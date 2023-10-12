import {Component, Input, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {AlertController, ModalController, ToastController} from "@ionic/angular";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Box, Boxfeed} from "../boxInterface";
import {globalState} from "../../service/states/global.state";
import {firstValueFrom} from "rxjs";


@Component({
  selector: 'app-editbox-modal',
  template:
    `
      <ion-header>
        <ion-toolbar>
          <ion-buttons>
            <ion-button color="danger" (click)="deleteAlert()" data-testid="deletebtn_">Delete box</ion-button>
          </ion-buttons>
          <ion-title>Edit Box</ion-title>
          <ion-buttons slot="end">
            <ion-button (click)="dismissModal()">Close</ion-button>
          </ion-buttons>
        </ion-toolbar>
      </ion-header>
      <ion-content>
        <ion-item>
            <ion-input label="Box Name" labelPlacement="floating"  [formControl]="BName" data-testid="boxName_"></ion-input>
            <div *ngIf="BName.invalid && BName.touched" class="error">
              Box name is required
            </div>
        </ion-item>
        <ion-item>
            <ion-input label="Size" labelPlacement="floating" [formControl]="BSize" data-testid="boxSize_"></ion-input>
            <div *ngIf="BSize.invalid && BSize.touched" class="error">
              Box size is required
            </div>
        </ion-item>
          <ion-item>
              <ion-input label="Price" labelPlacement="floating" [formControl]="BPrice" data-testid="boxPrice_"></ion-input>
                <div *ngIf="BPrice.invalid && BPrice.touched" class="error">
                  Price is required
                </div>
          </ion-item>
        <ion-item>
        <ion-input label="Description" labelPlacement="floating" [formControl]="BDesc" data-testid="boxDesc_"></ion-input>
          <div *ngIf="BDesc.invalid && BDesc.touched" class="error">
            Description is required
          </div>
      </ion-item>
          <ion-item>
              <ion-input label="Image" labelPlacement="floating" [formControl]="BImage" data-testid="boxImg_"></ion-input>
            <div *ngIf="BImage.invalid && BImage.touched" class="error">
              Image url is required
            </div>
          </ion-item>
            <ion-item *ngIf="BImage.value">
              <ion-label>Image preview</ion-label>
              <div><img style="max-height: 100px; width: auto;" [src]="this.BImage.value" ></div>
            </ion-item>



          <ion-button expand="full" type="submit" (click)="updateBox()" [disabled]="BoxGroup.invalid" data-testid="updateboxbtn_">Update Box</ion-button>
      </ion-content>
    `,
})
export class editBoxModal implements OnInit
{
 // @Input() copyOfBox!: Box;
  copyOfBox: Box;

    BName = new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(100)]);
    BSize = new FormControl("",[Validators.required,Validators.minLength(6),Validators.maxLength(50)]);
    BPrice = new FormControl(0,[Validators.required]);
    BImage = new FormControl("", [Validators.required]);
    BDesc = new FormControl("",[Validators.required]);
    BId = new FormControl (0,[ Validators.required])

   boxFeedgroup = new FormGroup(
       {
           boxId: this.BId,
           name: this.BName,
           size: this.BSize,
           price: this.BPrice,
           boxImgUrl: this.BImage
       }
   )

    BoxGroup = new FormGroup(
      {
          boxId: this.BId,
          name: this.BName,
          size: this.BSize,
          description: this.BDesc,
          price: this.BPrice,
          boxImgUrl: this.BImage,

      });


  constructor(private modalController: ModalController, private http: HttpClient, private alertcontroller: AlertController, public state: globalState, public router: Router, public toastcontrol: ToastController) {


 this.copyOfBox = this.state.currentBox; }

  dismissModal() {
    this.modalController.dismiss();
  }

  ngOnInit(): void {
    this.BoxGroup.patchValue({
      boxId: this.copyOfBox.boxId,
      name: this.copyOfBox.name,
      size: this.copyOfBox.size,
      description: this.copyOfBox.description,
      price: this.copyOfBox.price,
      boxImgUrl: this.copyOfBox.boxImgUrl
    })
    this.boxFeedgroup.patchValue({
      boxId: this.copyOfBox.boxId
    })
  }

  deleteAlert() {
    this.alertcontroller.create({
      message: "do you want to delete " + this.copyOfBox.name + "?",
      buttons: [
        {
          role: "cancel",
          text: "no"
        },
        {
          role: "confirm",
          text: "yes",
          handler: async () => {
            const call = this.http.delete("http://localhost:5000/box/" + this.state.currentBox.boxId);
            const result = await firstValueFrom(call);
            this.state.boxfeed = this.state.boxfeed.filter(e => e.boxId != this.copyOfBox.boxId);
            this.state.currentBox = {}
            this.router.navigate(["tabs/tabs/boxfeed"]);
            this.toastcontrol.create({
              color: "success",
              message: this.copyOfBox.name + ' successfully deleted.',
              duration: 2000,
            }).then(res =>
            {
              res.present();
            })
            this.dismissModal();
          }
        }
      ]
    }).then(res =>
    {
      res.present();
    })
  }

 async updateBox() {
     const call = this.http.put("http://localhost:5000/box/" + this.copyOfBox.boxId, this.BoxGroup.value);
     const result = await firstValueFrom(call);
     let index = this.state.boxfeed.findIndex(b => b.boxId == this.copyOfBox.boxId)

     this.state.boxfeed[index] = this.boxFeedgroup.getRawValue() as Boxfeed;
     this.state.currentBox = this.BoxGroup.getRawValue() as Box;

     this.router.navigate(["tabs/tabs/boxfeed"]);
     this.toastcontrol.create({
         color: "success",
         message: this.copyOfBox.name + ' successfully updated.',
         duration: 2000,
     }).then(res => {
         res.present();
     })
     this.dismissModal();
 }

}
