import { AlertifyService } from "./../../../../Shared/services/alertify.service";
import { PasswordValidatorService } from "./../../../../Shared/validators/passwordValidator.service";
import { RegisterUser } from "./../../../models/registerUser";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthService } from "src/app/Shared/services/auth.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm: FormGroup;
  subscription: Subscription;
  returnUrl: string;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private passwordValidator: PasswordValidatorService,
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

  get form() { return this.registerForm.controls; }

  register(user: RegisterUser): void {
    this.subscription = this.authService.register(user)
      .subscribe(() => {
          this.alertify.success("Register success");
          this.router.navigateByUrl(this.returnUrl);
        },
        error => this.alertify.error(error));
  }

  private createForm() {
    this.registerForm = this.fb.group({
        firstName: ["", [Validators.required, Validators.maxLength(50)]],
        familyName: ["", [Validators.required, Validators.maxLength(50)]],
        username: ["", [Validators.required, Validators.maxLength(20)]],
        email: ["", [Validators.required, Validators.email]],
        password: ["", [Validators.required, Validators.maxLength(100), this.passwordValidator.CorrectFormat]],
        confirmPassword: ["", Validators.required],
        street: ["", [Validators.required, Validators.maxLength(100)]],
        houseNr: ["", [Validators.required, Validators.maxLength(5)]],
        houseBus: [null, Validators.maxLength(4)],
        postalCode: ["", [Validators.required, Validators.maxLength(6)]],
        city: ["", [Validators.required, Validators.maxLength(100)]],
        streetBilling: [null, Validators.maxLength(100)],
        houseNrBilling: [null, Validators.maxLength(5)],
        houseBusBilling: [null, Validators.maxLength(4)],
        postalCodeBilling: [null, Validators.maxLength(6)],
        cityBilling: [null, Validators.maxLength(100)],
      },
      { validator: this.passwordValidator.Match("password", "confirmPassword") });
  }
}
