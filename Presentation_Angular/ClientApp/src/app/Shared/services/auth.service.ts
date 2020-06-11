import { ChangeUserDetails } from "./../../Core/models/changeUserDetails";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { map } from "rxjs/operators";
import { DeleteUser } from "src/app/Core/models/deleteUser";

import { LoginUser } from "../../Core/models/loginUser";
import { environment } from "./../../../environments/environment";
import { RegisterUser } from "./../../Core/models/registerUser";
import { User } from "./../models/user";
import { Observable } from "rxjs";
import { ChangePassword } from "src/app/Core/models/changePassword";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private readonly baseUrl = environment.baseUrl;
  private jwtHelper = new JwtHelperService();

  constructor(
    private http: HttpClient
  ) {
  }

  // returns anonymous object with the token in it
  login(user: LoginUser): Observable<any> {
    return this.http.post(this.baseUrl + "auth/login", user)
      .pipe(
        map((response: any) => {
          const userResp = response;
          if (userResp) {
            localStorage.setItem("token", userResp.token);
          }
        })
      );
  }

  // returns anonymous object with the token in it
  register(user: RegisterUser): Observable<any> {
    return this.http.post(this.baseUrl + "auth/register", user)
      .pipe(
        map((response: any) => {
          const userResp = response;
          if (userResp) {
            localStorage.setItem("token", userResp.token);
          }
        })
      );
  }

  changePassword(user: ChangePassword): Observable<{}> {
    return this.http.put(this.baseUrl + "auth/password", user);
  }

  getUserDetails(): Observable<ChangeUserDetails> {
    return this.http.get<ChangeUserDetails>(this.baseUrl + "auth/user");
  }

  changeUserDetails(user: ChangeUserDetails): Observable<any> {
    return this.http.put(this.baseUrl + "auth/user", user)
      .pipe(
        map((response: any) => {
          const userResp = response;
          if (userResp) {
            localStorage.setItem("token", userResp.token);
          }
        })
      );
  }

  delete(user: DeleteUser): Observable<{}> {
    // send a body with http delete
    const options = {
      headers: new HttpHeaders({
        'Content-Type': "application/json",
      }),
      body: user
    };

    return this.http.delete(this.baseUrl + "auth/delete", options);
  }

  getUser(): User {
    const token = localStorage.getItem("token");
    if (token === null) {
      return null;
    }
    const decodedToken = this.jwtHelper.decodeToken(token);
    const user: User = {
      uid: decodedToken.nameid,
      username: decodedToken.unique_name,
      role: decodedToken.role
    };
    return user;
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem("token");
  }
}
