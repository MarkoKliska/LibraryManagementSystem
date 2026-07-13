export interface AddBookResponse {
  id: string;
  title: string;
  authorId: string;
  authorName: string;
  genreId: string;
  genreName: string;
  isbn13: string;
  numberOfCopies: number;
}