import { Component, Input, OnInit } from "@angular/core";

import { Product } from "./../../../Shared/models/product";
import { ShoppingCartService } from "./../../../Shared/services/shoppingCart.service";

@Component({
  selector: "app-addToCart",
  templateUrl: "./addToCart.component.html",
  styleUrls: ["./addToCart.component.css"]
})
export class AddToCartComponent implements OnInit {
  @Input()
  Product: Product;
  @Input()
  extraMessage: string;

  constructor(
    private shoppingCart: ShoppingCartService,
  ) {
  }

  ngOnInit() {}

  get itemCount(): number {
    return this.shoppingCart.getItemCount(this.Product);
  }

  increment(): void {
    this.shoppingCart.increment(this.Product);
  }

  decrement(): void {
    this.shoppingCart.decrement(this.Product);
  }

  clearFromCart(): void {
    this.shoppingCart.removeFromCart(this.Product);
  }
}
