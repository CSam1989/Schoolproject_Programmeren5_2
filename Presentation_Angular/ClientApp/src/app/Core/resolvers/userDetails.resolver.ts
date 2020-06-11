import { AuthService } from "src/app/Shared/services/auth.service";
import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";

import { ChangeUserDetails } from "../models/changeUserDetails";
import { environment } from "./../../../environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class UserDetailsResolver implements Resolve<Observable<ChangeUserDetails>> {
  baseUrl = environment.baseUrl;

  constructor(
    private authService: AuthService
  ) {
  }

  resolve() {
    return this.authService.getUserDetails();
  }
}
