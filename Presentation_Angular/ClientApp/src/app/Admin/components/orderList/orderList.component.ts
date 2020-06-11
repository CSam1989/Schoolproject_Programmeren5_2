import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, MatSort, MatTableDataSource } from "@angular/material";
import { Subscription } from "rxjs";
import { Order } from "src/app/Shared/models/order";
import { FilterService } from "src/app/Shared/services/filter.service";

import { OrderService } from "./../../../Shared/services/order.service";

@Component({
  selector: "app-orderList",
  templateUrl: "./orderList.component.html",
  styleUrls: ["./orderList.component.css"]
})
export class OrderListComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  dataSource: MatTableDataSource<Order>;

  searchValue = "";

  columnsToDisplay = ["customer", "orderId", "orderDate", "totalCount", "totalPrice", "actions"];
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  constructor(
    private orderService: OrderService,
    private filterService: FilterService
  ) {
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Order>();

    this.getOrders();

    this.filterService.filters(this.dataSource);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.applySorting();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getOrders() {
    this.subscription = this.orderService.get()
      .subscribe(order => this.dataSource.data = order["list"],
        error => console.log(error));
  }

  updatePaid(order: Order): void {
    this.subscription = this.orderService.updatePaid(order.id,
        {
          id: order.id,
          isPayed: true
        })
      .subscribe(() => {
        const index = this.dataSource.data.indexOf(order);
        if (index !== -1) {
          this.dataSource.data[index].isPayed = true;
          this.dataSource.paginator = this.paginator;
        }
      });
  }

  delete(order: Order): void {
    this.subscription = this.orderService.delete(order.id)
      .subscribe(() => {
        const index = this.dataSource.data.indexOf(order);
        if (index !== -1) {
          this.dataSource.data.splice(index, 1);
          this.dataSource.paginator = this.paginator;
        }
      });
  }

  applyFilter(searchValue: string): void {
    this.filterService.applyFilter(searchValue, this.dataSource);
  }

  applySorting() {
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
      case "customer":
        return item.customer.familyName;
      case "orderId":
        return item.id;
      case "totalCount":
        return item.orderSummary.totalCount;
      case "totalPrice":
        return item.orderSummary.totalInclVat;
      case "actions":
        return item.isPayed;

      default:
        return item[property];
      }
    };
  }

  clearSearchField() {
    this.searchValue = "";
    this.applyFilter(this.searchValue);
  }

}
