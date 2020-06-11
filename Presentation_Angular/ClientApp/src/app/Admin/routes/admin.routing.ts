import { ProductByIdResolver } from "./../resolvers/productById.resolver";
import { AdminAuthGuard } from "./../guards/admin-auth.guard";
import { OrderListComponent } from "./../components/orderList/orderList.component";
import { ProductUpsertComponent } from "./../components/productUpsert/productUpsert.component";
import { ProductListComponent } from "./../components/productList/productList.component";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "src/app/Shared/guards/auth.guard";

const routes: Routes = [
  {
    path: "",
    canActivate: [AuthGuard, AdminAuthGuard],
    children: [
      {
        path: "products",
        children: [
          { path: "", component: ProductListComponent },
          { path: "create", component: ProductUpsertComponent },
          {
            path: "update/:id",
            component: ProductUpsertComponent,
            resolve: { product: ProductByIdResolver }
          },
        ]
      },
      {
        path: "orders",
        children: [
          { path: "", component: OrderListComponent }
        ]
      }
    ]
  },
];

export const AdminRoutes = RouterModule.forChild(routes);
export const AdminComponents = [
  ProductListComponent,
  ProductUpsertComponent,
  OrderListComponent
];
