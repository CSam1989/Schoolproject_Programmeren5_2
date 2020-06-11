import { environment } from "./../../../environments/environment";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Customer } from "../models/customer";

@Injectable({
  providedIn: "root"
})
export class CustomerService {
  private readonly baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) {
  }

  getCurrentCustomer(): Observable<Customer> {
    return this.http.get<Customer>(this.baseUrl + "customer");
  }
}
