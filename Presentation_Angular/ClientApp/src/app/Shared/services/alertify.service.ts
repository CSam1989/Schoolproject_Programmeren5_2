import { Injectable } from "@angular/core";
import * as alertify from "alertifyjs";

@Injectable({
  providedIn: "root"
})
export class AlertifyService {

  constructor() {}

  confirm(title: string, message: string, okCallback: () => any, cancelCallback: () => any) {
    alertify.confirm(title,
        message,
        (e: any) => {
          if (e) {
            okCallback();
          }
        },
        (c: any) => {
          if (c) {
            cancelCallback();
          }
        })
      .set("closable", false);
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  message(message: string) {
    alertify.message(message);
  }
}
