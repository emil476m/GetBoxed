import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {globalState} from "../../service/states/global.state";
import {FormControl, Validators} from "@angular/forms";
import {Boxfeed} from "../boxInterface";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-boxfeed',
  template:
    `
      <ion-header [translucent]="true">
          <ion-toolbar>
            <ion-title align="center">Get Boxed LLC</ion-title>
            <div slot="end">
              <ion-input [formControl]="pageSize" type="number">Max number of results</ion-input>
            </div>
          </ion-toolbar>
        </ion-header>
        <ion-content class="ion-padding" [fullscreen]="true">
          <ion-card *ngFor="let box of state.search">
            <ion-toolbar>
              <ion-title>{{box.name}}</ion-title>
              <ion-buttons slot="end">
                <ion-button (click)="goToBox(box.boxId)">
                  <ion-icon name="open-outline"></ion-icon>
                </ion-button>
              </ion-buttons>
            </ion-toolbar>
            <ion-text style="color: #2dd36f">$ {{box.price}}</ion-text>
            <ion-img [src]="box.boxImgUrl" style="width: auto; height: 200px;"/>
          </ion-card>
        </ion-content>
       <ion-item lines="none">
           <ion-searchbar animated="true" (ionInput)="getBoxes()" [formControl]="searchTerm" placeholder="Search for boxes"/>
       </ion-item>
    `,
})
export class SearchPage
{
  searchTerm = new FormControl("",[Validators.minLength(3)])
  pageSize = new FormControl("5",[Validators.required]);

    constructor(public state: globalState, public http: HttpClient, public router: Router) {
    }

    async getBoxes() {
      const call = this.http.get<Boxfeed[]>("http://localhost:5000/box/Seartch", {
        params: {
          searchTerm: this.searchTerm.value!,
          amount: this.pageSize.value!
        }
      });
      const result = await firstValueFrom<Boxfeed[]>(call);
      this.state.search = result;
    }

  goToBox(boxId: number)
  {
      this.state.isSearch = true;
      this.router.navigate(['tabs/tabs/detail/'+ boxId])
  }
}
