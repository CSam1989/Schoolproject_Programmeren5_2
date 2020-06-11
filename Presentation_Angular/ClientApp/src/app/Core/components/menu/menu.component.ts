import { AlertifyService } from "./../../../Shared/services/alertify.service";
import { ShoppingCartService } from "./../../../Shared/services/shoppingCart.service";
import { AuthService } from "./../../../Shared/services/auth.service";
import { Component, OnInit } from "@angular/core";
import { User } from "src/app/Shared/models/user";
import { Router } from "@angular/router";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.css"]
})
export class MenuComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    public shoppingCart: ShoppingCartService,
    private router: Router,
  ) {
  }

  ngOnInit() {
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  get User(): User {
    return this.authService.getUser();
  }

  get Username(): String {
    return this.User.username;
  }

  get Role(): String {
    return this.User.role;
  }

  loginPage(): void {
    this.router.navigate(["auth/login"], { queryParams: { returnUrl: this.router.url } });
  }

  logout(): void {
    this.authService.logout();
    this.alertify.success("Logged out");
    this.router.navigate(["/home"]);
  }
}
