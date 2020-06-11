import { AlertifyService } from "./../../../../Shared/services/alertify.service";
import { LoginUser } from "../../../models/loginUser";
import { AuthService } from "./../../../../Shared/services/auth.service";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  subscription: Subscription;
  returnUrl: string;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.authService.logout();
    // gaat returnUrl ophalen van authGuard, als deze niet bestaat => go to home
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/home";

    this.createForm();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get form() { return this.loginForm.controls; }

  login(user: LoginUser): void {
    this.subscription = this.authService.login(user)
      .subscribe(() => {
          this.alertify.success("Login success");
          this.router.navigateByUrl(this.returnUrl);
        },
        error => this.alertify.error(error));
  }

  private createForm() {
    this.loginForm = this.fb.group({
      username: ["", Validators.required],
      password: ["", Validators.required]
    });
  }
}
