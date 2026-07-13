export interface RentalDetails {
  rentalId: string;
  bookId: string;
  bookTitle: string;
  rentalDate: string;
  dueDate: string;
  returnDate?: string;
}