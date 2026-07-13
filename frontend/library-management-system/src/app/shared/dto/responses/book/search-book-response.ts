export interface SearchBooksResponse {
  id: string;
  title: string;
  authorName: string;
  genreName: string;
  isbn13: string;
  totalCopies: number;
  availableCopies: number;
}