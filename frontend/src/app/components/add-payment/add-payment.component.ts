import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormsModule,
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';
import { LoanService } from '../../core/services/loan/loan.service';
import { ToastService } from '../../core/services/toast/toast.service';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-payment',
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
  templateUrl: './add-payment.component.html',
})
export class AddPaymentComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private loanService: LoanService,
    private toast: ToastService,
    private dialogRef: MatDialogRef<AddPaymentComponent>,
    @Inject(MAT_DIALOG_DATA) public loan: any
  ) {
    this.form = this.fb.group({
      amount: [0, [Validators.required, Validators.min(0.01)]],
    });
  }

  submit() {
    if (this.form.invalid) return;

    const dto = { amount: this.form.value.amount };

    this.toast.showLoading('Adding payment...');
    this.loanService.addPayment(this.loan.id, dto).subscribe({
      next: () => {
        this.toast.dismiss();
        this.toast.showSuccess('Payment added successfully');
        this.dialogRef.close(true);
      },
      error: (err) => {
        this.toast.dismiss();
        console.error(err);
        this.toast.showError('Failed to add payment');
      },
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
