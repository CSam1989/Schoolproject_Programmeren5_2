import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { Order } from "../models/order";
import { OrderService } from "./../services/order.service";

@Injectable({
  providedIn: "root"
})
export class OrderByIdResolver implements Resolve<Order> {

  constructor(
    private orderService: OrderService
  ) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Order> {
    return this.orderService.getById(+route.paramMap.get("id"));
  }
}
