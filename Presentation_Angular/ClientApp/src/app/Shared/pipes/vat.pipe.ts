import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "vat"
})
export class VatPipe implements PipeTransform {

  transform(value: number, vat?: number): any {
    if (!value) {
      return null;
    }

    const actualVat = vat ? vat : 21;

    return value * (actualVat / 100);
  }

}
