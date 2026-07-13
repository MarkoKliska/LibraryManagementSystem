export interface EditBookRequest {
  bookId: string;
  title: string;
  authorId: string;
  genreId: string;
  isbn13: string;
  numberOfCopies: number;
}