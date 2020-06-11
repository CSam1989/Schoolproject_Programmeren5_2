import { Product } from "./../../../Shared/models/product";
import { Component, OnInit, Inject } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material";

@Component({
  selector: "app-productDetailDialog",
  templateUrl: "./productDetailDialog.component.html",
  styleUrls: ["./productDetailDialog.component.css"]
})
export class ProductDetailDialogComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Product
  ) {
  }

  ngOnInit() {
  }
}
