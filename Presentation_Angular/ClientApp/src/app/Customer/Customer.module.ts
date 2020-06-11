import { ShippingBillingAddressComponent } from "./components/shippingBillingAddress/shippingBillingAddress.component";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SharedModule } from "./../Shared/Shared.module";
import { AddToCartComponent } from "./components/addToCart/addToCart.component";
import { CustomerDetailsComponent } from "./components/customerDetails/customerDetails.component";
import { OrderPreviousDetailsComponent } from "./components/orderPreviousDetails/orderPreviousDetails.component";
import { ProductCardComponent } from "./components/productCard/productCard.component";
import { ProductDetailDialogComponent } from "./components/productDetailDialog/productDetailDialog.component";
import { SummaryComponent } from "./components/summary/summary.component";
import { CustomerComponents, CustomerRoutes } from "./routes/customer.routing";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CustomerRoutes
  ],
  exports: [
    RouterModule,
    ProductCardComponent,
  ],
  declarations: [
    CustomerComponents,
    ProductCardComponent,
    ProductDetailDialogComponent,
    AddToCartComponent,
    SummaryComponent,
    CustomerDetailsComponent,
    OrderPreviousDetailsComponent,
    ShippingBillingAddressComponent
  ],
  entryComponents: [
    ProductDetailDialogComponent,
  ],
})
export class CustomerModule {
}
