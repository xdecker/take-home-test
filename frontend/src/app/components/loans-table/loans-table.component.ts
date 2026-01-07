import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { LoanStatusTextPipe } from '../../pipes/loan-status-text.pipe';
import { Loan } from '../../models';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-loans-table',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    LoanStatusTextPipe,
  ],
  standalone: true,
  templateUrl: './loans-table.component.html',
  styleUrl: './loans-table.component.scss',
})
export class LoansTableComponent {
  @Input() set loans(value: Loan[]) {
    this.dataSource.data = value;
  }
  @Output() onAddPayment = new EventEmitter<Loan>();
  @Output() onDelete = new EventEmitter<Loan>();

  displayedColumns: string[] = [
    'amount',
    'currentBalance',
    'applicantName',
    'status',
    'actions',
  ];

  dataSource = new MatTableDataSource<Loan>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}
