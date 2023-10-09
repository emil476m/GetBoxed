import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Order, Orders} from "../orderInterface";
import {Box} from "../boxInterface";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
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
                    <ion-title>OrderId: {{state.currentOrder.orderId}}</ion-title>
                </ion-toolbar>

                <ion-item lines="none">
                    <i>Customerid: {{state.currentOrder.customerId}}</i>
                </ion-item>
                <ion-item>
                  <i> OrderPrice: <i style="color: #2dd36f"> $ {{state.currentOrder.totalPrice}}</i></i>
                </ion-item>
              <ion-item>
                <i>OrderDate: {{state.currentOrder.orderDate}}</i>
              </ion-item>


              <ion-card *ngFor="let order of state.currentOrder.boxOrder">
                <ion-toolbar>


                      <ion-text>BoxId:{{order.boxId}} Amount:{{order.amount}}</ion-text>


                      <ion-button slot="end" (click)="goToBox(order.boxId)" >
                        <ion-icon name="open-outline"></ion-icon>
                      </ion-button>

                </ion-toolbar>
              </ion-card>

            </ion-card>
          </ion-content>
        `,
})
export class orderDetailPage implements OnInit{

  constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, public state: globalState) {

  }


  goToBox(boxId: number) {
    this.state.isSearch = false;
    this.state.isOrder = true;
    this.router.navigate(['tabs/tabs/detail/'+ boxId])
  }


  goBack() {
    this.router.navigate(["tabs/tabs/orders"]);
  }

  ngOnInit(): void {
    this.getOrder()
  }

  private async getOrder() {
    const id = (await firstValueFrom(this.route.paramMap)).get('id');
    const call = this.http.get<Order>("http://localhost:5000/Order/"+id);
    const result = await firstValueFrom<Order>(call);
    this.state.currentOrder = result
  }
}
