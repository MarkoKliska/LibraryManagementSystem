export interface RentedBookResponse {
  rentalId: string;
  bookId: string;
  title: string;
  authorName: string;
  genreName: string;
  isbn13: string;
  rentalDate: string;
  dueDate: string;
  bookCopyId: string;
}