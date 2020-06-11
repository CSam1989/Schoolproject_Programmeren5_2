import { Order } from "src/app/Shared/models/order";
import { OrderService } from "./../../../Shared/services/order.service";
import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { MatTableDataSource, MatPaginator, MatSort } from "@angular/material";
import { FilterService } from "src/app/Shared/services/filter.service";
import { trigger, state, style, transition, animate } from "@angular/animations";

@Component({
  selector: "app-orderPrevious",
  templateUrl: "./orderPrevious.component.html",
  styleUrls: ["./orderPrevious.component.css"],
  animations: [
    trigger("detailExpand",
      [
        state("collapsed", style({ height: "0px", minHeight: "0" })),
        state("expanded", style({ height: "*" })),
        transition("expanded <=> collapsed", animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")),
      ]),
  ],
})
export class OrderPreviousComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  dataSource: MatTableDataSource<Order>;

  searchValue = "";

  columnsToDisplay = ["customer", "orderDate", "orderId", "totalPrice"];
  expandedElement: Order | null;
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

    this.applySorting();
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getOrders() {
    this.subscription = this.orderService.getPrevious()
      .subscribe(order => {
          console.log(order);
          this.dataSource.data = order["list"];
        },
        error => console.log(error));
  }

  applyFilter(searchValue: string): void {
    this.filterService.applyFilter(searchValue, this.dataSource);
  }

  applySorting() {
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
      case "orderId":
        return item.id;
      case "totalPrice":
        return item.orderSummary.totalInclVat;
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
