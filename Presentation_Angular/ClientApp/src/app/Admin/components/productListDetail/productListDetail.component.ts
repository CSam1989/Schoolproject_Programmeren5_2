import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { AlertifyService } from "src/app/Shared/services/alertify.service";
import { Product } from "./../../../Shared/models/product";

@Component({
  selector: "app-productListDetail",
  templateUrl: "./productListDetail.component.html",
  styleUrls: ["./productListDetail.component.css"]
})
export class ProductListDetailComponent implements OnInit {
  @Input()
  Product: Product;
  @Output()
  deleteById = new EventEmitter<Product>();

  constructor(
    private alertify: AlertifyService
  ) {
  }

  ngOnInit() {
  }

  delete(product: Product): void {
    const title = `Deleting ${product.name}`;
    const message = "Are your sure?";
    this.alertify.confirm(
      title,
      message,
      () => this.deleteById.emit(product), // Pressed OK
      () => this.alertify.error("Product not deleted") // Pressed Cancel
    );
  }
}
