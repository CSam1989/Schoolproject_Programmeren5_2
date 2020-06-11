import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "summary"
})
export class SummaryPipe implements PipeTransform {

  transform(value: string, limit?: number): any {
    if (!value) {
      return null;
    }

    const actualLimit = limit ? limit : 50;

    if ((value.length - 3) > actualLimit) {
      return value.substr(0, actualLimit) + "...";
    }

    return value;
  }
}
