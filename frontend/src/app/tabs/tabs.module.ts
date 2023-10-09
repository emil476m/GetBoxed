import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { TabsPage } from './tabs.page';

import { HomePageRoutingModule } from './tabs-routing.module';
import {BoxFeedPage} from "../boxfeed/boxFeed.page";
import {HttpClientModule} from "@angular/common/http";
import {NewBoxModal} from "../newboxmodal/newboxmodal";
import {SearchPage} from "../searchpage/search.page";
import {boxDetailPage} from "../detailbox/boxdetail.page";
import {editBoxModal} from "../EditBoxModal/editboxmodal";
import {OrderFeedPage} from "../orderspage/orderFeed.page";
import {orderDetailPage} from "../detailorder/orderdetail.page";



@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        HomePageRoutingModule,
        HttpClientModule,
        ReactiveFormsModule
    ],
  declarations: [TabsPage,BoxFeedPage,NewBoxModal,SearchPage,boxDetailPage,editBoxModal, OrderFeedPage, orderDetailPage]
})
export class HomePageModule {}
