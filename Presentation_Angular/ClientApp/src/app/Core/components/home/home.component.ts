import { Product } from "src/app/Shared/models/product";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs/internal/Subscription";
import { ProductService } from "src/app/Shared/services/product.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  products: Product[];

  constructor(
    private productService: ProductService,
  ) {
  }

  ngOnInit() {
    this.getProducten();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getProducten() {
    this.subscription = this.productService.get()
      .subscribe(product => this.products = product["list"],
        error => console.log(error));
  }
}
