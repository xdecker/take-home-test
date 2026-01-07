import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, TextOnlySnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private currentSnack: MatSnackBarRef<TextOnlySnackBar> | null = null;

  constructor(private snackBar: MatSnackBar) {}

  private open(message: string, panelClass: string[], duration: number) {
    // Cierra el toast previo si existe
    this.currentSnack?.dismiss();

    this.currentSnack = this.snackBar.open(message, '×', {
      duration,
      panelClass,
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });

    return this.currentSnack;
  }

  showSuccess(message: string, duration = 3000) {
    this.open(message, ['toast-success'], duration);
  }

  showError(message: string, duration = 5000) {
    this.open(message, ['toast-error'], duration);
  }

  showInfo(message: string, duration = 3000) {
    this.open(message, ['toast-info'], duration);
  }

  showLoading(message: string) {
    this.open(message, ['toast-loading'], 0);
  }

  dismiss() {
    this.currentSnack?.dismiss();
    this.currentSnack = null;
  }
}
