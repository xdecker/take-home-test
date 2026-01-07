import { LoanStatus } from "./loan-status.enum";

export interface Loan {
    id: string;
    applicantName: string;
    amount: number;
    currentBalance: number;
    status: LoanStatus;
  }