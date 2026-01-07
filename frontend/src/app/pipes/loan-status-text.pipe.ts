import { Pipe, PipeTransform } from '@angular/core';
import { LoanStatus } from '../models/loan-status.enum';

@Pipe({ name: 'loanStatusText', standalone: true })
export class LoanStatusTextPipe implements PipeTransform {
  transform(value: LoanStatus): string {
    return value === LoanStatus.Active ? 'Active' : 'Paid';
  }
}
