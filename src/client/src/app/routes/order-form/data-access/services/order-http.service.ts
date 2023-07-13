import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrderHttpService {

  constructor(
    private readonly http: HttpClient
  ) { }

  public createOrder(name: string): Observable<void> {
    return this.http.post<void>('https://localhost:44330/order', {
      assemblyName: name
    });
  }
}
