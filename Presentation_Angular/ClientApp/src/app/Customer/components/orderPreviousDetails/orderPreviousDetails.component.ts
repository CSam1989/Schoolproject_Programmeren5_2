import { Component, Input, OnInit } from "@angular/core";
import { Order } from "src/app/Shared/models/order";

@Component({
  selector: "app-orderPreviousDetails",
  templateUrl: "./orderPreviousDetails.component.html",
  styleUrls: ["./orderPreviousDetails.component.css"]
})
export class OrderPreviousDetailsComponent implements OnInit {
  @Input()
  Order: Order;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
