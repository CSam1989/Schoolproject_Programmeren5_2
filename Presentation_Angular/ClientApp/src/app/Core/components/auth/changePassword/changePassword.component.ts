import { AlertifyService } from "./../../../../Shared/services/alertify.service";
import { PasswordValidatorService } from "./../../../../Shared/validators/passwordValidator.service";
import { Subscription } from "rxjs";
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from "@angular/forms";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthService } from "src/app/Shared/services/auth.service";
import { ChangePassword } from "src/app/Core/models/changePassword";

@Component({
  selector: "app-changePassword",
  templateUrl: "./changePassword.component.html",
  styleUrls: ["./changePassword.component.css"]
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  passwordForm: FormGroup;
  subscription: Subscription;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private passwordValidator: PasswordValidatorService
  ) {
  }

  ngOnInit() {
    this.createForm();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get form() { return this.passwordForm.controls; }

  // FormsDirective moet worden gereset, omdat mat-error hier ga controleren op validation
  // Found bug thanks to Tom Vanelven ;-)
  // And https://stackoverflow.com/a/48217303
  changePassword(user: ChangePassword, formDirective: FormGroupDirective) {
    this.authService.changePassword(user)
      .subscribe(() => {
          this.resetForm(formDirective);
          this.alertify.success("Your password has been changed");
        },
        error => this.alertify.error(error));
  }

  private createForm() {
    this.passwordForm = this.fb.group({
        oldPassword: ["", Validators.required],
        newPassword: ["", [Validators.required, Validators.maxLength(100), this.passwordValidator.CorrectFormat]],
        confirmPassword: ["", Validators.required]
      },
      { validator: this.passwordValidator.Match("newPassword", "confirmPassword") });
  }

  private resetForm(formDirective: FormGroupDirective) {
    formDirective.resetForm();
    this.passwordForm.reset();
  }
}
