import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-notFound",
  templateUrl: "./notFound.component.html",
  styleUrls: ["./notFound.component.css"]
})
export class NotFoundComponent implements OnInit {
  // page template found on https://colorlib.com/etc/404/colorlib-error-404-1/
  // Aangepast naar mijn layout & ga deze ook gebruiken voor mijn 401 error
  constructor() {}

  ngOnInit() {
  }

}
