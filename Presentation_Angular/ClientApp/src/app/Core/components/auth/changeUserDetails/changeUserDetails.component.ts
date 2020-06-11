import { AlertifyService } from "./../../../../Shared/services/alertify.service";
import { Subscription } from "rxjs";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthService } from "src/app/Shared/services/auth.service";
import { ChangeUserDetails } from "src/app/Core/models/changeUserDetails";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-changeUserDetails",
  templateUrl: "./changeUserDetails.component.html",
  styleUrls: ["./changeUserDetails.component.css"]
})
export class ChangeUserDetailsComponent implements OnInit, OnDestroy {
  userDetailForm: FormGroup;
  subscription: Subscription;
  user: ChangeUserDetails;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.user = this.route.snapshot.data.userDetails;
    this.createForm();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get form() { return this.userDetailForm.controls; }

  changeUserDetails(user: ChangeUserDetails) {
    this.subscription = this.authService.changeUserDetails(user)
      .subscribe(() => this.alertify.success("Your details has been changed"),
        error => this.alertify.error(error));
  }

  private createForm() {
    this.userDetailForm = this.fb.group({
      firstName: [this.user.firstName, [Validators.required, Validators.maxLength(50)]],
      familyName: [this.user.familyName, [Validators.required, Validators.maxLength(50)]],
      username: [this.user.username, [Validators.required, Validators.maxLength(20)]],
      street: [this.user.street, [Validators.required, Validators.maxLength(100)]],
      houseNr: [this.user.houseNr, [Validators.required, Validators.maxLength(5)]],
      houseBus: [this.user.houseBus, Validators.maxLength(4)],
      postalCode: [this.user.postalCode, [Validators.required, Validators.maxLength(6)]],
      city: [this.user.city, [Validators.required, Validators.maxLength(100)]],
      streetBilling: [this.user.streetBilling, Validators.maxLength(100)],
      houseNrBilling: [this.user.houseNrBilling, Validators.maxLength(5)],
      houseBusBilling: [this.user.houseBusBilling, Validators.maxLength(4)],
      postalCodeBilling: [this.user.postalCodeBilling, Validators.maxLength(6)],
      cityBilling: [this.user.cityBilling, Validators.maxLength(100)],
    });
  }
}
