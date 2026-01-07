import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { LoansTableComponent } from './components/loans-table/loans-table.component';
import { Loan } from './models';
import { LoanService } from './core/services/loan/loan.service';
import { ToastService } from './core/services/toast/toast.service';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { CreateLoanComponent } from './components/create-loan/create-loan.component';
import { AddPaymentComponent } from './components/add-payment/add-payment.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    LoansTableComponent,
    MatDialogModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  loans: Loan[] = [];

  constructor(
    private loanService: LoanService,
    private toast: ToastService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getLoans();
  }

  async getLoans() {
    this.loanService.getLoans().subscribe({
      next: (data) => {
        this.loans = data;
      },
      error: (err) => {
        this.toast.dismiss();
        console.error(err);
        this.toast.showError('Faild to get loans. Please try later');
      },
    });
  }

  openCreateLoan() {
    const dialogRef = this.dialog.open(CreateLoanComponent);

    dialogRef.afterClosed().subscribe((refresh: boolean) => {
      if (refresh) this.getLoans();
    });
  }

  openAddPayment(loan: Loan) {
    const dialogRef = this.dialog.open(AddPaymentComponent, { data: loan });
  
    dialogRef.afterClosed().subscribe((refresh: boolean) => {
      if (refresh) this.getLoans();
    });
  }
  
  deleteLoan(loan: Loan) {
    if (!confirm(`Are you sure you want to delete loan of ${loan.applicantName}?`)) return;
  
    this.toast.showLoading('Deleting loan...');
    this.loanService.deleteLoan(loan.id).subscribe({
      next: () => {
        this.toast.dismiss();
        this.toast.showSuccess('Loan deleted successfully');
        this.getLoans();
      },
      error: (err) => {
        this.toast.dismiss();
        console.error(err);
        this.toast.showError('Failed to delete loan');
      },
    });
  }
  
}
