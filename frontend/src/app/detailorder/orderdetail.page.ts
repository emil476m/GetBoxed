import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Box, Boxfeed} from "../boxInterface";
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
                    <ion-title>{{state.currentOrder}}</ion-title>
                    <ion-buttons slot="end">
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
                </ion-item>
            </ion-card>
          </ion-content>
        `,
})
export class orderDetailPage implements OnInit{

  constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, private modalcontroller: ModalController, public state: globalState) {

  }



  goBack() {
    this.router.navigate(["tabs/tabs/order"]);
  }

  ngOnInit(): void {
    this.getBox()
  }

  private async getBox() {
    const id = (await firstValueFrom(this.route.paramMap)).get('id');
    const call = this.http.get<Box>("http://localhost:5000/order/"+id);
    const result = await firstValueFrom<Box>(call);
    this.state.currentBox = result
  }

  openEdit() {
    this.modalcontroller.create({
      component: editBoxModal,
      componentProps: {
        copyOfBox: {...this.state.currentBox}
      }
    }).then(res => {
      res.present();
    })
  }
}
