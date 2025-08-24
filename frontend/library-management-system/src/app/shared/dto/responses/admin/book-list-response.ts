export interface BookListResponse {
  id: string;
  title: string;
  authorName: string;
  genreName: string;
  isbn13: string;
  totalCopies: number;
  availableCopies: number;
  rentedByUserId?: string;
  rentedByUserEmail?: string;
}