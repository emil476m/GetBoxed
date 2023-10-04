import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";

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
        </ion-content>
       <ion-item lines="none">
           <ion-searchbar animated="true" (ionInput)="getBoxes()" [(ngModel)]="searchTerm" placeholder="Search for boxes"/>
       </ion-item>
    `,
})
export class SearchPage
{
    searchTerm: string = "";

    getBoxes() {

    }
}
