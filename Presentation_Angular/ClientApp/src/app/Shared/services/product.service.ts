import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Product } from "../models/product";

@Injectable({
  providedIn: "root"
})
export class ProductService {
  private readonly baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) {
  }

  get(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "product");
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + "product/" + id);
  }

  create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.baseUrl + "product", product);
  }

  update(id: number, product: Product): Observable<Product> {
    return this.http.put<Product>(this.baseUrl + "product/" + id, product);
  }

  delete(id: number): Observable<{}> {
    return this.http.delete(this.baseUrl + "product/" + id);
  }
}
