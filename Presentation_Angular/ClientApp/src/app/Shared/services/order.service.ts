import { Observable } from "rxjs";
import { Order } from "./../models/order";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { OrderPaid } from "../models/orderPaid";

@Injectable({
  providedIn: "root"
})
export class OrderService {
  private readonly baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) {
  }

  get(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + "order");
  }

  getPrevious(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + "order/previousorders");
  }

  getById(id: number): Observable<Order> {
    return this.http.get<Order>(this.baseUrl + "order/" + id);
  }

  create(order: Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl + "order", order);
  }

  updatePaid(id: number, order: OrderPaid): Observable<{}> {
    return this.http.put<OrderPaid>(this.baseUrl + "order/" + id, order);
  }

  delete(id: number): Observable<{}> {
    return this.http.delete<Order>(this.baseUrl + "order/" + id);
  }
}
