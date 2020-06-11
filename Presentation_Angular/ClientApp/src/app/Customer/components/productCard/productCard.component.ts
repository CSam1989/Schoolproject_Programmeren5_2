import { Product } from "./../../../Shared/models/product";
import { ShoppingCartService } from "./../../../Shared/services/shoppingCart.service";
import { ProductDetailDialogComponent } from "./../productDetailDialog/productDetailDialog.component";
import { Component, OnInit, Input } from "@angular/core";
import { MatDialog } from "@angular/material";

@Component({
  selector: "app-productCard",
  templateUrl: "./productCard.component.html",
  styleUrls: ["./productCard.component.css"]
})
export class ProductCardComponent implements OnInit {
  @Input()
  Product: Product;

  constructor(
    private dialog: MatDialog,
    private shoppingCart: ShoppingCartService
  ) {
  }

  ngOnInit() {
  }

  openProductDetail(product: Product) {
    this.dialog.open(ProductDetailDialogComponent,
      {
        data: product,
        panelClass: "custom-dialog-container"
      });
  }

  get cartItem() {
    return this.shoppingCart.getItem(this.Product);
  }

  addToCart() {
    this.shoppingCart.addToCart(this.Product);
  }

  removeFromCart() {
    this.shoppingCart.removeFromCart(this.Product);
  }
}
