import { FilterService } from "./../../../Shared/services/filter.service";
import { ProductService } from "./../../../Shared/services/product.service";
import { Product } from "./../../../Shared/models/product";
import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { trigger, state, style, transition, animate } from "@angular/animations";
import { Subscription } from "rxjs";
import { MatTableDataSource, MatPaginator, MatSort } from "@angular/material";

@Component({
  selector: "app-productList",
  templateUrl: "./productList.component.html",
  styleUrls: ["./productList.component.css"],
  animations: [
    trigger("detailExpand",
      [
        state("collapsed", style({ height: "0px", minHeight: "0" })),
        state("expanded", style({ height: "*" })),
        transition("expanded <=> collapsed", animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")),
      ]),
  ],
})
export class ProductListComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  dataSource: MatTableDataSource<Product>;

  searchValue = "";

  columnsToDisplay = ["name", "category", "price"];
  expandedElement: Product | null;
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  constructor(
    private productService: ProductService,
    private filterService: FilterService
  ) {
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Product>();

    this.getProducten();

    this.filterService.filters(this.dataSource);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getProducten() {
    this.subscription = this.productService.get()
      .subscribe(product => this.dataSource.data = product["list"],
        error => console.log(error));
  }

  delete(product: Product): void {
    this.subscription = this.productService.delete(product.id)
      .subscribe(() => {
        const index = this.dataSource.data.indexOf(product);
        if (index !== -1) {
          this.dataSource.data.splice(index, 1);
          this.dataSource.paginator = this.paginator;
        }
      });
  }

  applyFilter(searchValue: string): void {
    this.filterService.applyFilter(searchValue, this.dataSource);
  }

  clearSearchField() {
    this.searchValue = "";
    this.applyFilter(this.searchValue);
  }
}
