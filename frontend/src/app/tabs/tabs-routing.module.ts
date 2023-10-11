import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';
import { TabsPage } from './tabs.page';
import {BoxFeedPage} from "../boxfeed/boxFeed.page";
import {SearchPage} from "../searchpage/search.page";
import {boxDetailPage} from "../detailbox/boxdetail.page";
import {OrderFeedPage} from "../orderspage/orderFeed.page";
import {orderDetailPage} from "../detailorder/orderdetail.page";
import {graphModels} from "../GraphModel/graphModels";

const routes: Route[] = [
  {
    path: '',
    children: [
      {
        path: 'tabs',
        component: TabsPage,
        children: [
          {
            path: 'boxfeed',
            component: BoxFeedPage
          },
            {
              path: `search`,
                component: SearchPage
            },
          {
            path: `orders`,
            component: OrderFeedPage
          },
          {
            path: `order-detail/:id`,
            component: orderDetailPage,
          },
          {
            path: `detail/:id`,
            component: boxDetailPage,
          },
          {
            path: 'graphs',
            component: graphModels,
          },
          {
            path: '',
            redirectTo: 'tabs/boxfeed',
            pathMatch: 'full',
          },
        ],
      },
      {
        path: '',
        redirectTo: 'tabs/boxfeed',
        pathMatch: 'full',
      }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomePageRoutingModule {}
