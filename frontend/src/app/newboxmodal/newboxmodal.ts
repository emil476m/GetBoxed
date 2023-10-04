import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-newbox-modal',
  template:
    `
      <ion-header>
        <ion-toolbar>
          <ion-title>New Box</ion-title>
          <ion-buttons slot="end">
            <ion-button (click)="dismissModal()">Close</ion-button>
          </ion-buttons>
        </ion-toolbar>
      </ion-header>
      <ion-content>
        <ion-item>
            <ion-input label="Box Name" labelPlacement="floating"  [formControl]="BName"></ion-input>
        </ion-item>
        <ion-item>
            <ion-input label="Size" labelPlacement="floating" [formControl]="BSize"></ion-input>
        </ion-item>
          <ion-item>
              <ion-input label="Price" labelPlacement="floating" [formControl]="BPrice"></ion-input>
          </ion-item>
          <ion-item>
              <ion-input label="Image" labelPlacement="floating" [formControl]="BImage"></ion-input>
              <div><img style="max-height: 100px; width: auto;" [src]="this.BImage.value" ></div>
          </ion-item>
          <ion-item>
              <ion-input label="Description" labelPlacement="floating" [formControl]="BDesc"></ion-input>
          </ion-item>


          <ion-button expand="full" type="submit" [disabled]="BoxGroup.invalid">Create Box</ion-button>
      </ion-content>
    `,
})
export class NewBoxModal
{
    BName = new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(100)]);
    BSize = new FormControl("",[Validators.required,Validators.minLength(6),Validators.maxLength(50)]);
    BPrice = new FormControl("",[Validators.required]);
    BImage = new FormControl("", [Validators.required]);
    BDesc = new FormControl("",[Validators.required]);


    BoxGroup = new FormGroup(
      {
          name: this.BName,
          size: this.BSize,
          price: this.BPrice,
          boxImgUrl: this.BImage,
          description: this.BDesc
      });


  constructor(private modalController: ModalController, private http: HttpClient) {
  }

  dismissModal() {
    this.modalController.dismiss();
  }
}
