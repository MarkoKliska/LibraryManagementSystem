export interface AddBookRequest {
  title: string;
  authorId: string;
  genreId: string;
  isbn13: string;
  numberOfCopies: number;
}