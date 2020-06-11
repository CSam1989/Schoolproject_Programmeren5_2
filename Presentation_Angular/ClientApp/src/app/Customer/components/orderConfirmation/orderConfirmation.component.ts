import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs";
import { Order } from "src/app/Shared/models/order";

@Component({
  selector: "app-orderConfirmation",
  templateUrl: "./orderConfirmation.component.html",
  styleUrls: ["./orderConfirmation.component.css"]
})
export class OrderConfirmationComponent implements OnInit {
  subscription: Subscription;
  order: Order;

  constructor(
    private route: ActivatedRoute,
  ) {
  }

  ngOnInit() {
    this.subscription = this.route.data
      .subscribe((data: { order: Order }) => {
          this.order = data.order["order"];
          this.order.orderSummary = data.order["orderSummary"];
        },
        error => console.log(error));
  }

}
