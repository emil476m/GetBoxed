import {Component, OnInit} from "@angular/core";
import {ModalController, ToastController} from "@ionic/angular";
import {HttpClient} from "@angular/common/http";
import {NavigationStart, Router} from "@angular/router";
import {globalState} from "../../service/states/global.state";
import {NewBoxModal} from "../newboxmodal/newboxmodal";
import {Boxfeed} from "../boxInterface";
import {catchError, firstValueFrom, of} from "rxjs";
import {cartItem, customer, Order, OrderFeed, Orders} from "../orderInterface";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-boxfeed',
  template:
    `
      <ion-header [translucent]="true">
          <ion-toolbar>
            <ion-title align="center">Cart</ion-title>
          </ion-toolbar>
        </ion-header>
        <ion-content class="ion-padding" [fullscreen]="true">

          <div *ngFor="let cartitem of state.cart">
            <ion-card [attr.data-testid]="'card_ ' + cartitem.name">
                <ion-toolbar  >
                    <ion-title>{{cartitem.name}}</ion-title>
                    <ion-buttons slot="end">
                      <ion-button (click)="deleteitemfromcart(cartitem)">
                        <ion-icon name="trash"></ion-icon>
                      </ion-button>
                    </ion-buttons>
                </ion-toolbar>
                <ion-grid>
                    <ion-row>
                      <ion-col>
                        <img [src]="cartitem.boximgurl" style="height: 100px; width: 100px;">
                      </ion-col>
                        <ion-col>
                            <ion-text style="color: #2dd36f" (change)="amountofbox;" [textContent]="'$ ' + cartitem.price*cartitem.amount"></ion-text>
                            <ion-input type="number" (ionChange)="changeAmount(cartitem)" value="{{cartitem.amount}}" [min]="1" [formControl]="amountofbox"/>
                        </ion-col>
                    </ion-row>
                </ion-grid>
            </ion-card></div>
            <div>
              <ion-item  slot="end">
                <ion-text>Total: <ion-text style="color: #2dd36f" (change)="ischange" [textContent]="'$ '+this.total" [formControl]="totalprice"></ion-text>
                </ion-text>
              </ion-item>
              <ion-item>
                <ion-input labelPlacement="floating" label="Full name" [formControl]="name" (ionInput)="calculatetotal()"></ion-input>
              </ion-item>
              <ion-item>
                <ion-input labelPlacement="floating" label="Phone number" [formControl]="phonenumber"></ion-input>
              </ion-item>
              <ion-item>
                <ion-input label="Email" labelPlacement="floating" [formControl]="email"></ion-input>
              </ion-item>
              <ion-item>
                <ion-input label="Address" labelPlacement="floating" [formControl]="address">
              </ion-input>
              </ion-item>
              <ion-item slot="end">
                <ion-button (click)="placeorder()">Buy now</ion-button>
              </ion-item>
            </div>
        </ion-content>
    `,
})
export class CartPage{
  amountofbox = new FormControl(1,[Validators.required,Validators.min(1)]);
  ischange: boolean = false
  total: number = 0;
  totalprice = new FormControl(0);
  name = new FormControl("", [Validators.required]);
  phonenumber = new FormControl("",[Validators.required]);
  email = new FormControl("",[Validators.required]);
  address = new FormControl("", [Validators.required])

  costumerFG = new FormGroup({
    name: this.name,
    tlf: this.phonenumber,
    mail: this.email,
    address: this.address,
  })

  constructor(private modalController: ModalController, private http: HttpClient, public router: Router, public state: globalState, public  toastcontrol: ToastController) {
    this.router.events.subscribe(event =>    {
      if(event instanceof NavigationStart) {
        this.calculatetotal()
      }
    })
  }

  changeAmount(cartitem: cartItem) {
    const amount: number = 0;
    if (this.amountofbox.value != null) {
      cartitem.amount = this.amountofbox.value;
      this.calculatetotal()
      this.ischange = true
    }
    this.ischange = false;
  }

  calculatetotal()
  {
    let price: number = 0;
    this.state.cart.forEach(value =>{
      price = price + (value.price*value.amount)
    });
    this.total = price;
  }

  deleteitemfromcart(cartitem: cartItem)
  {
    let index = this.state.cart.findIndex(c => c.boxId == cartitem.boxId)
    delete this.state.cart[index];
    this.state.cart = this.state.cart.filter(e => e.boxId != cartitem.boxId);
    this.total = this.total - (cartitem.price*cartitem.amount)
  }



  async placeorder() {
    try {

    const orderlist: Orders[] = []
    this.state.cart.forEach(value => {
      const item: Orders =
        {
          boxId: value.boxId,
          amount: value.amount
        }
        orderlist.push(item)
    })
    const customerCall = this.http.post<customer>('http://localhost:5000/customer', this.costumerFG.value);
    const cresult = await firstValueFrom<customer>(customerCall);

    const order: Order =
      {
        customerId: cresult.customerId,
        totalPrice: this.total.valueOf(),
        boxOrder: orderlist
      }

      const orderCall = this.http.post<Order>('http://localhost:5000/order', order)
      const result = await firstValueFrom<Order>(orderCall)
      this.toastcontrol.create(
        {
          color: "success",
          duration: 2000,
          message: "Order placed successfully"
        }
      ).then(res => {
        res.present
      })
      let orderFeedItem: OrderFeed = {
      orderId: result.orderId!,
        price: result.totalPrice!,
        customerId: result.customerId!
      }
      this.state.orderfeed.push(orderFeedItem)
      this.state.cart = []
      this.costumerFG.reset();
      this.router.navigate(['tabs/tabs/boxfeed']);
    }
  catch (error)
  {
    console.log(error)
  }
  }

}
