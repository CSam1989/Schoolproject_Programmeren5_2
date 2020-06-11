import { OrderByIdResolver } from "./../../Shared/resolvers/orderById.resolver";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "src/app/Shared/guards/auth.guard";
import { OrderConfirmationComponent } from "../components/orderConfirmation/orderConfirmation.component";
import { OrderGuard } from "./../../Shared/guards/order.guard";
import { CustomerDetailsResolver } from "./../../Shared/resolvers/customerDetails.resolver";
import { OrderCreateComponent } from "./../components/orderCreate/orderCreate.component";
import { OrderPreviousComponent } from "./../components/orderPrevious/orderPrevious.component";
import { ShoppingCartComponent } from "./../components/shoppingCart/shoppingCart.component";

const routes: Routes = [
  { path: "shoppingcart", component: ShoppingCartComponent },
  {
    path: "orders",
    canActivate: [AuthGuard],
    children: [
      {
        path: "create",
        component: OrderCreateComponent,
        resolve: { customer: CustomerDetailsResolver }
      },
      { path: "previous", component: OrderPreviousComponent },
      {
        path: "confirmation/:id",
        component: OrderConfirmationComponent,
        resolve: { order: OrderByIdResolver },
        canActivate: [OrderGuard]
      },
    ]
  }
];

export const CustomerRoutes = RouterModule.forChild(routes);
export const CustomerComponents = [
  OrderConfirmationComponent,
  OrderCreateComponent,
  OrderPreviousComponent,
  ShoppingCartComponent
];
