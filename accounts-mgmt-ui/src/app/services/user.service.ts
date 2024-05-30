import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7155/users';

  constructor(private http: HttpClient) {}

  signup(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signup`, data);
  }

  authenticate(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/authenticate`, data);
  }

  getBalance(token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/auth/balance`, { headers });
  }
}
