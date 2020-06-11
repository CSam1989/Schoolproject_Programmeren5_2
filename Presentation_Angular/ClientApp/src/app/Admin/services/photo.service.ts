import { environment } from "./../../../environments/environment";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Photo } from "../models/photo";

@Injectable({
  providedIn: "root"
})
export class PhotoService {
  baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) {
  }

  addPhoto(photo: FormData): Observable<Photo> {
    return this.http.post<Photo>(this.baseUrl + "photo", photo);
  }

  removePhoto(photoId: string): Observable<{}> {
    return this.http.delete(this.baseUrl + "photo/" + photoId);
  }
}
