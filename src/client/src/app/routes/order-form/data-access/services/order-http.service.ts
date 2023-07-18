import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Order } from "../models";

@Injectable({
  providedIn: 'root'
})
export class OrderHttpService {

  constructor(
    private readonly http: HttpClient
  ) { }

  public loadOrders(): Observable<Order[]> {
    return this.http.get<Order[]>('https://localhost:44330/order');
  }

  public createOrder(name: string, tempId: number): Observable<void> {
    return this.http.post<void>('https://localhost:44330/order', {
      assemblyName: name,
      tempId: tempId
    });
  }

  public updateOrder(orderId: number, assemblyName: string): Observable<void> {
    return this.http.put<void>('https://localhost:44330/order', {
      orderId,
      assemblyName
    });
  }

  public deleteOrder(orderId: number): Observable<void> {
    return this.http.delete<void>(`https://localhost:44330/order/${orderId}`);
  }
}
