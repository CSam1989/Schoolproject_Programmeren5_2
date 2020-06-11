import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SharedModule } from "./../Shared/Shared.module";
import { ChangePasswordComponent } from "./components/auth/changePassword/changePassword.component";
import { ChangeUserDetailsComponent } from "./components/auth/changeUserDetails/changeUserDetails.component";
import { DeleteUserComponent } from "./components/auth/deleteUser/deleteUser.component";
import { FooterComponent } from "./components/footer/footer.component";
import { MenuComponent } from "./components/menu/menu.component";
import { CoreComponents, CoreRoutes } from "./routes/core.routing";
import { CustomerModule } from "../Customer/Customer.module";


@NgModule({
  imports: [
    CommonModule,
    CustomerModule,
    SharedModule,
    CoreRoutes,
  ],
  exports: [
    RouterModule,
    MenuComponent,
    FooterComponent
  ],
  declarations: [
    CoreComponents,
    ChangePasswordComponent,
    ChangeUserDetailsComponent,
    DeleteUserComponent,
    MenuComponent,
    FooterComponent,
  ]
})
export class CoreModule {
}
