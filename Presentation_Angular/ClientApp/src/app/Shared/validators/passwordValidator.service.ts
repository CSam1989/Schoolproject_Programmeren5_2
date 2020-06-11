import { Injectable } from "@angular/core";
import { FormGroup, ValidatorFn, FormControl } from "@angular/forms";

@Injectable({
  providedIn: "root"
})
// Found on https://stackoverflow.com/a/34582914
export class PasswordValidatorService {
  CorrectFormat(control: FormControl): { [key: string]: any } {
    const passwordRegex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/;
    if (control.value && !passwordRegex.test(control.value)) {
      return { invalidPassword: true };
    }
  }

  Match(password: string, confirmPassword: string): ValidatorFn {
    return (group: FormGroup): { [key: string]: any } => {
      const pass = group.controls[password];
      const confirm = group.controls[confirmPassword];
      if (pass.value !== confirm.value) {
        return { passwordMismatch: true };
      }
    };
  }
}
