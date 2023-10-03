import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Box, Boxfeed} from "../boxInterface";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Component({
    selector: 'app-boxdetailed',
    template:
        `
            <ion-card>
                <ion-toolbar>
                    <ion-buttons>
                        <ion-button (click)="goBack()">
                            <ion-icon name="chevron-back"></ion-icon>
                        </ion-button>
                    </ion-buttons>
                    <ion-title>{{currentBox.name}}</ion-title>
                    <ion-buttons slot="end">
                    <ion-button (click)="openEdit()">
                        <ion-icon name="cog"></ion-icon>
                    </ion-button>
                </ion-buttons>
                </ion-toolbar>
                <img [src]="currentBox.boxImgUrl">

                <ion-item lines="none">
                    <i>{{currentBox.description}}</i>
                </ion-item>
                <ion-item>
                    <i>{{currentBox.size}}</i>
                </ion-item>
                <ion-item>
                    <i>{{currentBox.price}}</i>
                </ion-item>
            </ion-card>
        `,
})
export class boxDetailPage implements OnInit{
    currentBox: Box | any = {};

    constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient) {

    }



    goBack() {
        this.router.navigate(["tabs/tabs/boxfeed"]);
    }

    ngOnInit(): void {
        this.getBox()
    }

    private async getBox() {
        const id = (await firstValueFrom(this.route.paramMap)).get('id');
        const call = this.http.get<Box>("http://localhost:5000/box/"+id);
        const result = await firstValueFrom<Box>(call);
        this.currentBox = result
    }

    openEdit() {

    }
}
