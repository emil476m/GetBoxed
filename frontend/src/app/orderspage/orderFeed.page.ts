import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {NewBoxModal} from "../newboxmodal/newboxmodal";
import {Boxfeed, OrderFeed} from "../boxInterface";
import {firstValueFrom} from "rxjs";
import {globalState} from "../../service/states/global.state";

@Component({
  selector: 'app-orderfeed',
  template:
    `
      <ion-header [translucent]="true">
        <ion-toolbar>
          <ion-title align="center">Get Boxed LLC</ion-title>
        </ion-toolbar>
      </ion-header>
      <ion-content class="ion-padding" [fullscreen]="true">
        <ion-card *ngFor="let order of state.orderfeed">
          <ion-toolbar>
            <ion-buttons slot="end">
              <ion-button >
                <ion-icon name="duplicate-outline"></ion-icon>
              </ion-button>
            </ion-buttons>
          </ion-toolbar>
          <ion-grid>
            <ion-row>
              <ion-col >
                <ion-title>OrderId:{{order.orderId}}</ion-title>
              </ion-col>
              <ion-col>
                <ion-text style="color: #2dd36f">Sum$ {{order.price}}</ion-text>
              </ion-col>
            </ion-row>
          </ion-grid>
        </ion-card>

      </ion-content>
    `,
})
export class OrderFeedPage
{

  constructor(private modalController: ModalController, private http: HttpClient, public router: Router, public state: globalState) {}


  ngOnInit(): void {
    this.getOrderFeed();
  }


  private async getOrderFeed() {
    try {
      const call = this.http.get<OrderFeed[]>("http://localhost:5000/Order");
      const result = await firstValueFrom<OrderFeed[]>(call);
      this.state.orderfeed = result
    }
    catch (error)
    {
      console.log(error)
    }

  }
}
