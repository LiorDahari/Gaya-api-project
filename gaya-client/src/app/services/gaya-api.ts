import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class GayaApi {
  constructor(private http: HttpClient) {}

  ServerSideGET(): Observable<any> {
    return this.http.get<any>('https://localhost:7044/api/Calculator/operations');
  }
  Calculate(request: any): Observable<any> {
    return this.http.post<any>('https://localhost:7044/api/Calculator/calculate', request);
  }
  GetLastHistory(): Observable<any> {
    return this.http.get<any>('https://localhost:7044/api/Calculator/last-history');
  }
}