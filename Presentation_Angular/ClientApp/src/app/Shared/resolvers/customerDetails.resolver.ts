import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { CustomerService } from "src/app/Shared/services/customer.service";
import { Customer } from "../models/customer";

@Injectable({
  providedIn: "root"
})
export class CustomerDetailsResolver implements Resolve<Customer> {

  constructor(
    private customerService: CustomerService
  ) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Customer> {
    return this.customerService.getCurrentCustomer();
  }
}
