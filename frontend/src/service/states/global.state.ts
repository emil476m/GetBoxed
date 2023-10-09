import {Injectable} from "@angular/core";
import {Box, Boxfeed} from "../../app/boxInterface";
import {Order, OrderFeed} from "../../app/orderInterface";

@Injectable({
  providedIn: 'root',
  })

export class globalState
{
  boxfeed: Boxfeed[] = [];
  currentBox: Box|any = {};
  search: Boxfeed[] = [];
  isSearch: boolean = false;
  isOrder: boolean = false;
  orderfeed: OrderFeed[] = [];
  currentOrder: Order = {};
}
