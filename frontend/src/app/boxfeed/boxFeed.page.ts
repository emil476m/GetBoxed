import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {NewBoxModal} from "../newboxmodal/newboxmodal";
import {Boxfeed} from "../boxInterface";
import {firstValueFrom} from "rxjs";
import {globalState} from "../../service/states/global.state";

@Component({
  selector: 'app-boxfeed',
  template:
    `
      <ion-header [translucent]="true">
          <ion-toolbar>
            <ion-title align="center">Get Boxed LLC</ion-title>
          </ion-toolbar>
        </ion-header>
        <ion-content class="ion-padding" [fullscreen]="true">

            <ion-card *ngFor="let box of state.boxfeed">
                <ion-toolbar>
                    <ion-title>{{box.name}}</ion-title>
                    <ion-buttons slot="end">
                        <ion-button (click)="goToBox(box.boxId)">
                            <ion-icon name="open-outline"></ion-icon>
                        </ion-button>
                    </ion-buttons>
                </ion-toolbar>
                <ion-grid>
                    <ion-row>
                        <ion-col size="3"><img style="max-height: 100px; width: auto;" [src]="box.boxImgUrl">
                        </ion-col>
                        <ion-col>
                            <ion-text style="color: #2dd36f">$ {{box.price}}</ion-text>
                        </ion-col>
                    </ion-row>
                </ion-grid>
            </ion-card>

          <ion-fab vertical="bottom" horizontal="end" slot="fixed">
          <ion-fab-button (click)="opennewBoxComponent()">
            <ion-icon name="add"></ion-icon>
          </ion-fab-button>
          </ion-fab>
        </ion-content>
    `,
})
export class BoxFeedPage implements OnInit{


  constructor(private modalController: ModalController, private http: HttpClient, public router: Router, public state: globalState) {}


  ngOnInit(): void {
    this.getBoxFeed();
  }


  async opennewBoxComponent()
  {
    this.modalController.create({
      component: NewBoxModal
    }).then(function (modal) {
      return modal.present();
    });
  }

    goToBox(boxId: number) {
      this.state.isSearch = false;
      this.state.isOrder = false;
      this.router.navigate(['tabs/tabs/detail/'+ boxId])
    }

    private async getBoxFeed() {
        try {
          const call = this.http.get<Boxfeed[]>("http://localhost:5000/box");
          const result = await firstValueFrom<Boxfeed[]>(call);
          this.state.boxfeed = result
        }
        catch (error)
        {
          console.log(error)
        }

    }
}
