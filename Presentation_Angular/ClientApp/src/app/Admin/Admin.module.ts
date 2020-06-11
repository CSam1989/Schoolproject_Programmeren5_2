import { ProductListDetailComponent } from "./components/productListDetail/productListDetail.component";
import { SharedModule } from "./../Shared/Shared.module";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { AdminComponents, AdminRoutes } from "./routes/admin.routing";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutes
  ],
  exports: [
    RouterModule
  ],
  declarations: [
    AdminComponents,
    ProductListDetailComponent
  ]
})
export class AdminModule {
}
