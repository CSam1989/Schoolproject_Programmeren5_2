import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { AuthGuard } from "./../../Shared/guards/auth.guard";
import { AboutComponent } from "./../components/about/about.component";
import { AccessDeniedComponent } from "./../components/accessDenied/accessDenied.component";
import { LoginComponent } from "./../components/auth/login/login.component";
import { ManageAccountComponent } from "./../components/auth/manageAccount/manageAccount.component";
import { RegisterComponent } from "./../components/auth/register/register.component";
import { NotFoundComponent } from "./../components/notFound/notFound.component";
import { PrivacyComponent } from "./../components/privacy/privacy.component";
import { UserDetailsResolver } from "./../resolvers/userDetails.resolver";


const routes: Routes = [
  { path: "home", component: HomeComponent },
  {
    path: "auth",
    children: [
      { path: "login", component: LoginComponent },
      { path: "register", component: RegisterComponent },
    ]
  },
  {
    path: "edit",
    canActivate: [AuthGuard],
    children: [
      { path: "account", component: ManageAccountComponent, resolve: { userDetails: UserDetailsResolver } }
    ]
  },
  { path: "about", component: AboutComponent },
  { path: "privacy", component: PrivacyComponent },
  { path: "access-denied", component: AccessDeniedComponent },
  { path: "admin", loadChildren: () => import("../../Admin/Admin.module").then(m => m.AdminModule) },
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "**", component: NotFoundComponent },
];

export const CoreRoutes = RouterModule.forRoot(routes);
export const CoreComponents = [
  HomeComponent,
  LoginComponent,
  RegisterComponent,
  ManageAccountComponent,
  AboutComponent,
  PrivacyComponent,
  AccessDeniedComponent,
  NotFoundComponent,
];
