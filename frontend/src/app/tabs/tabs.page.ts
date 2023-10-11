import { Component } from '@angular/core';

@Component({
  selector: 'app-tabs',
  template:
    `
          <ion-tabs>
            <ion-tab-bar slot="bottom">
              <ion-tab-button tab="home" routerLink="boxfeed">
                <ion-icon name="home"></ion-icon>
              </ion-tab-button>
              <ion-tab-button tab="search" routerLink="search">
                <ion-icon name="search"></ion-icon>
              </ion-tab-button>
              <ion-tab-button tab="cart">
                <ion-icon name="cart"></ion-icon>
              </ion-tab-button>
              <ion-tab-button tab="orders" routerLink="orders">
                <ion-icon name="reader-outline"></ion-icon>
              </ion-tab-button>
              <ion-tab-button tab="graph" routerLink="graphs">
                <ion-icon name="bar-chart-outline"></ion-icon>
              </ion-tab-button>
            </ion-tab-bar>
          </ion-tabs>

    `,
  styleUrls: ['tabs.page.scss'],
})
export class TabsPage {

  constructor() {}

}
