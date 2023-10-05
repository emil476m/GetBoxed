import {Injectable} from "@angular/core";
import {Box, Boxfeed} from "../../app/boxInterface";

@Injectable({
  providedIn: 'root',
  })

export class globalState
{
  boxfeed: Boxfeed[] = [];
  currentBox: Box|any = {};
  search: Boxfeed[] = [];
  isSearch: boolean = false;
}
