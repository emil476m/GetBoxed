import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Box, Boxfeed} from "../boxInterface";
import {firstValueFrom} from "rxjs";
import {globalState} from "../../service/states/global.state";

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
          <div *ngIf="BName.invalid && BName.touched" class="error">
            Box name is required
          </div>
        </ion-item>
        <ion-item>
            <ion-input label="Size" labelPlacement="floating" [formControl]="BSize"></ion-input>
          <div *ngIf="BSize.invalid && BSize.touched" class="error">
            Box size is required
          </div>
        </ion-item>
          <ion-item>
              <ion-input label="Price" labelPlacement="floating" [formControl]="BPrice"></ion-input>
            <div *ngIf="BPrice.invalid && BPrice.touched" class="error">
              Price is required
            </div>
          </ion-item>
        <ion-item>
        <ion-input label="Description" labelPlacement="floating" [formControl]="BDesc"></ion-input>
          <div *ngIf="BDesc.invalid && BDesc.touched" class="error">
            Description is required
          </div>
      </ion-item>
          <ion-item>
              <ion-input label="Image" labelPlacement="floating" [formControl]="BImage"></ion-input>
            <div *ngIf="BImage.invalid && BImage.touched" class="error">
              Image url is required
            </div>
          </ion-item>
        <ion-item *ngIf="BImage.value">
          <ion-label>Image preview</ion-label>
        <div><img style="max-height: 100px; width: auto;" [src]="this.BImage.value" ></div>
        </ion-item>
          <ion-button expand="full" type="submit" [disabled]="BoxGroup.invalid" (click)="createbox()">Create Box</ion-button>
      </ion-content>
    `,
})
export class NewBoxModal
{
    BName = new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(100)]);
    BSize = new FormControl("",[Validators.required,Validators.minLength(6),Validators.maxLength(50)]);
    BPrice = new FormControl(0,[Validators.required]);
    BImage = new FormControl("", [Validators.required]);
    BDesc = new FormControl("",[Validators.required]);


    BoxGroup = new FormGroup(
      {
          name: this.BName,
          size: this.BSize,
          description: this.BDesc,
          price: this.BPrice,
          boxImgUrl: this.BImage,
      });


  constructor(private modalController: ModalController, private http: HttpClient, private toastControl: ToastController, public state: globalState) {
  }

  dismissModal() {
    this.modalController.dismiss();
  }

  async createbox() {
    try{
      const call = this.http.post<Box>('http://localhost:5000/box', this.BoxGroup.value)
      const reault = await firstValueFrom(call);
      this.toastControl.create(
        {
          color: "success",
          duration: 2000,
          message: "Success"
        }
      ).then(res =>{
        res.present();

      })
      this.state.boxfeed.push(reault);
    }
    catch (error)
    {
      this.toastControl.create(
        {
          color: "success",
          duration: 2000,
          message: "Success"
        }
      ).then(res =>{
        res.present();

      })

    }
    this.dismissModal();
  }
}
