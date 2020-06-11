import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Order } from "src/app/Shared/models/order";
import { Customer } from "./../../../Shared/models/customer";
import { OrderService } from "./../../../Shared/services/order.service";
import { ShoppingCartService } from "./../../../Shared/services/shoppingCart.service";
import { Location } from "@angular/common";

@Component({
  selector: "app-orderCreate",
  templateUrl: "./orderCreate.component.html",
  styleUrls: ["./orderCreate.component.css"]
})
export class OrderCreateComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  customer: Customer;
  shippingForm: FormGroup;

  constructor(
    private orderService: OrderService,
    public shoppingCartService: ShoppingCartService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
  ) {
  }

  ngOnInit() {
    this.getCustomer();
    this.createForm();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get form() {
    return this.shippingForm.controls;
  }

  getCustomer(): void {
    this.subscription = this.route.data
      .subscribe((data: { customer: Customer }) =>
        this.customer = data.customer["customer"],
        error => console.log(error));
  }

  createForm(): void {
    this.shippingForm = this.fb.group({
      streetShipping: [null, Validators.maxLength(100)],
      houseNrShipping: [null, Validators.maxLength(5)],
      houseBusShipping: [null, Validators.maxLength(4)],
      postalcodeShipping: [null, Validators.maxLength(6)],
      cityShipping: [null, Validators.maxLength(100)]
    });
  }

  createOrder(): void {
    const order: Order = {
      streetShipping: this.form.streetShipping.value,
      houseNrShipping: this.form.houseNrShipping.value,
      houseBusShipping: this.form.houseBusShipping.value,
      postalcodeShipping: this.form.postalcodeShipping.value,
      cityShipping: this.form.cityShipping.value,
      customerId: this.customer.id,
      orderLines: this.shoppingCartService.shoppingCart
    };
    this.orderService.create(order)
      .subscribe(id => {
          this.router.navigate(["/orders/confirmation", id]);
          this.shoppingCartService.clearAll();
        },
        error => console.log(error));
  }

  cancel(): void {
    this.location.back();
  }
}
