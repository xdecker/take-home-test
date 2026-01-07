import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormsModule,
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { LoanService } from '../../core/services/loan/loan.service';
import { ToastService } from '../../core/services/toast/toast.service';
import { LoanCreateDto } from '../../models';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-create-loan',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './create-loan.component.html',
})
export class CreateLoanComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private loanService: LoanService,
    private toast: ToastService,
    private dialogRef: MatDialogRef<CreateLoanComponent>
  ) {
    this.form = this.fb.group({
      applicantName: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(0.01)]],
    });
  }

  submit() {
    if (this.form.invalid) return;

    const dto: LoanCreateDto = this.form.value;
    this.toast.showLoading('Creating loan...');
    this.loanService.createLoan(dto).subscribe({
      next: (loan) => {
        this.toast.dismiss();
        this.toast.showSuccess('Loan created successfully');
        this.dialogRef.close(true);
      },
      error: (err) => {
        this.toast.dismiss();
        console.error(err);
        this.toast.showError('Failed to create loan');
      },
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
