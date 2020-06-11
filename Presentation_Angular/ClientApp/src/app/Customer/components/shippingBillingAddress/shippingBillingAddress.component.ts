import { Order } from "./../../../Shared/models/order";
import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-shippingBillingAddress",
  templateUrl: "./shippingBillingAddress.component.html",
  styleUrls: ["./shippingBillingAddress.component.css"]
})
export class ShippingBillingAddressComponent implements OnInit {
  @Input()
  Order: Order;

  constructor() {}

  ngOnInit() {
  }

}
