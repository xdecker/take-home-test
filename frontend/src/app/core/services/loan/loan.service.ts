import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Loan, LoanCreateDto, LoanPaymentDto } from '../../../models';

@Injectable({
  providedIn: 'root',
})
export class LoanService {
  private readonly baseUrl = environment.apiUrlDocker; // decidir que api probar

  constructor(private http: HttpClient) {}

  //getall

  getLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(`${this.baseUrl}/loan`);
  }

  //get one
  getLoanById(id: string): Observable<Loan> {
    return this.http.get<Loan>(`${this.baseUrl}/loan`);
  }

  //create a loan
  createLoan(dto: LoanCreateDto): Observable<Loan> {
    return this.http.post<Loan>(`${this.baseUrl}/loan`, dto);
  }

  //add a payment of amount
  addPayment(id: string, dto: LoanPaymentDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/loan/${id}/payment`, dto);
  }

  //delete
  deleteLoan(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/loan/${id}`);
  }
}
