import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Box, Boxfeed} from "../boxInterface";
import {cartItem} from "../orderInterface";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {editBoxModal} from "../EditBoxModal/editboxmodal";
import {globalState} from "../../service/states/global.state";

@Component({
    selector: 'app-boxdetailed',
    template:
        `
          <ion-content>
            <ion-card>
                <ion-toolbar>
                    <ion-buttons>
                        <ion-button (click)="goBack()">
                            <ion-icon name="chevron-back"></ion-icon>
                        </ion-button>
                    </ion-buttons>
                    <ion-title>{{state.currentBox.name}}</ion-title>
                    <ion-buttons slot="end">
                    <ion-button (click)="openEdit()" data-testid="editbtn_">
                        <ion-icon name="cog"></ion-icon>
                    </ion-button>
                </ion-buttons>
                </ion-toolbar>
                <img [src]="state.currentBox.boxImgUrl">

                <ion-item lines="none">
                    <i>{{state.currentBox.description}}</i>
                </ion-item>
                <ion-item>
                    <i>{{state.currentBox.size}}</i>
                </ion-item>
                <ion-item>
                    <i style="color: #2dd36f">$ {{state.currentBox.price}}</i>
                    <ion-button slot="end" (click)="addtoCart()">Add to cart
                    <ion-icon name="cart">
                    </ion-icon></ion-button>
                </ion-item>
            </ion-card>
          </ion-content>
        `,
})
export class boxDetailPage implements OnInit{

    constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, private modalcontroller: ModalController, public state: globalState) {
    }



    goBack() {
        if(this.state.isSearch === false && this.state.isOrder === false)
        {
        this.router.navigate(["tabs/tabs/boxfeed"]);
        }
        else if (this.state.isOrder === true){
          this.router.navigate(["tabs/tabs/orders"]);
        }
        else
        {
          this.router.navigate(["tabs/tabs/search"])
        }
    }

    ngOnInit(): void {
        this.getBox()
    }

    private async getBox() {
        const id = (await firstValueFrom(this.route.paramMap)).get('id');
        const call = this.http.get<Box>("http://localhost:5000/box/"+id);
        const result = await firstValueFrom<Box>(call);
        this.state.currentBox = result
    }

    openEdit() {

      this.modalcontroller.create({
        component: editBoxModal,
        componentProps: {
         // copyOfBox: {...this.state.currentBox}
        }
      }).then(res => {
        res.present();
      })
    }

  addtoCart() {
    let item: cartItem|any=
      {
        name: this.state.currentBox.name,
        boxId: this.state.currentBox.boxId,
        price: this.state.currentBox.price,
        amount: 1,
        boximgurl: this.state.currentBox.boximgurl
      }

      const checkifexist = this.state.cart.some(value => item.boxId == value.boxId)
      if(checkifexist)
      {
        let index = this.state.cart.findIndex(value => item.boxId == value.boxId)
        this.state.cart[index].amount++
      }
      else
      {
      this.state.cart.push(item);
      }
  }
}
