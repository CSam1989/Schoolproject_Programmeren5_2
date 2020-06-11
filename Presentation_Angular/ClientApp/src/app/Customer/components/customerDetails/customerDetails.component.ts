import { Component, OnInit, Input } from "@angular/core";
import { Customer } from "src/app/Shared/models/customer";

@Component({
  selector: "app-customerDetails",
  templateUrl: "./customerDetails.component.html",
  styleUrls: ["./customerDetails.component.css"]
})
export class CustomerDetailsComponent implements OnInit {
  @Input()
  Customer: Customer;

  constructor() {}

  ngOnInit() {
  }

}
