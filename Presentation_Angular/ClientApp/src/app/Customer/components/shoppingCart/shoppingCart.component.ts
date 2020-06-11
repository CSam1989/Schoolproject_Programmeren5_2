import { Component, OnInit } from "@angular/core";
import { Product } from "./../../../Shared/models/product";
import { ShoppingCartService } from "./../../../Shared/services/shoppingCart.service";

@Component({
  selector: "app-shoppingCart",
  templateUrl: "./shoppingCart.component.html",
  styleUrls: ["./shoppingCart.component.css"]
})
export class ShoppingCartComponent implements OnInit {

  constructor(
    public shoppingCart: ShoppingCartService,
  ) {
  }

  ngOnInit() {
  }

  clearAll(): void {
    this.shoppingCart.clearAll();
  }

  clearFromCart(product: Product) {
    this.shoppingCart.removeFromCart(product);
  }

}
