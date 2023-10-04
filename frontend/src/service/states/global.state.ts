import {Injectable} from "@angular/core";
import {Box, Boxfeed} from "../../app/boxInterface";

@Injectable({
  providedIn: 'root',
  })

export class globalState
{
  Boxfeed: Boxfeed[] = [];
  currentBox: Box|any = {};
}
