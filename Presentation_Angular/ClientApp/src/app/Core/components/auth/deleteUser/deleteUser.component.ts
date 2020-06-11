import { AlertifyService } from "./../../../../Shared/services/alertify.service";
import { AuthService } from "src/app/Shared/services/auth.service";
import { Subscription } from "rxjs";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { DeleteUser } from "src/app/Core/models/deleteUser";

@Component({
  selector: "app-deleteUser",
  templateUrl: "./deleteUser.component.html",
  styleUrls: ["./deleteUser.component.css"]
})
export class DeleteUserComponent implements OnInit, OnDestroy {
  deleteForm: FormGroup;
  subscription: Subscription;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private router: Router,
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

  get form() { return this.deleteForm.controls; }

  delete(user: DeleteUser): void {
    this.subscription = this.authService.delete(user)
      .subscribe(() => {
          this.alertify.success("Your account has been deleted successfully");
          this.authService.logout();
          this.router.navigate(["/home"]);
        },
        error => this.alertify.error(error));
  }

  private createForm() {
    this.deleteForm = this.fb.group({
      password: ["", Validators.required]
    });
  }
}
