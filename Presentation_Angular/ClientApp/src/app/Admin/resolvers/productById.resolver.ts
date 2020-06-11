import { ProductService } from "./../../Shared/services/product.service";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { Product } from "src/app/Shared/models/product";

@Injectable({
  providedIn: "root"
})
export class ProductByIdResolver implements Resolve<Product> {

  constructor(
    private productService: ProductService
  ) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Product> {
    return this.productService.getById(route.params.id);
  }
}
