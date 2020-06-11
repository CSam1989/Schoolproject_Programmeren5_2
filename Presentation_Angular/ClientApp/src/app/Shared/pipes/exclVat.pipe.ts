import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "exclVat"
})
export class ExclVatPipe implements PipeTransform {

  transform(value: any, vat?: any): any {
    if (!value) {
      return null;
    }

    const actualVat = vat ? vat : 21;

    return value * (1 - (actualVat / 100));
  }

}
