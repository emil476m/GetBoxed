import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Order, Orders} from "../orderInterface";
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
                    <ion-title>{{state.currentOrder.orderId}}</ion-title>
                    <ion-buttons slot="end">
                </ion-buttons>
                </ion-toolbar>

                <ion-item lines="none">
                    <i>{{state.currentOrder.customerId}}</i>
                </ion-item>
                <ion-item>
                    <i>{{state.currentOrder.totalPrice}}</i>
                </ion-item>


              <ion-card *ngFor="let order of state.currentOrder.Orders">
                <ion-grid>
                  <ion-row>
                    <ion-col >
                      <ion-title>BoxId:{{order.boxId}} Amount:{{order.amount}}</ion-title>
                    </ion-col>

                  </ion-row>
                </ion-grid>
              </ion-card>

            </ion-card>
          </ion-content>
        `,
})
export class orderDetailPage implements OnInit{

  constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, public state: globalState) {

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
