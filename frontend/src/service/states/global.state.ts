import {Injectable} from "@angular/core";
import {Box, Boxfeed, Order, OrderFeed} from "../../app/boxInterface";

@Injectable({
  providedIn: 'root',
  })

export class globalState
{
  boxfeed: Boxfeed[] = [];
  currentBox: Box|any = {};
  search: Boxfeed[] = [];
  isSearch: boolean = false;
  orderfeed: OrderFeed[] = [];
  currentOrder: Order|any = [];
}
