import { Routes } from "@angular/router";
import { AdminPageComponent } from "./admin-page/admin-page.component";
import { UsersListComponent } from "./feature/users-list/users-list.component";
import { UserDetailsComponent } from "./feature/user-details/user-details.component";
import { BooksListComponent } from "./feature/books-list/books-list.component";
import { EditBookComponent } from "./feature/edit-book/edit-book.component";
import { AuthorsComponent } from "./feature/authors/authors.component";
import { GenresComponent } from "./feature/genres/genres.component";
import { AddBookComponent } from "./feature/add-book/add-book.component";

export const adminRoutes: Routes = [
  {
    path: '',
    component: AdminPageComponent,
    children: [
      { path: 'users', component: UsersListComponent },
      { path: 'users/:userId', component: UserDetailsComponent },
      { path: 'books', component: BooksListComponent },
      { path: 'books/edit/:bookId', component: EditBookComponent },
      { path: 'authors', component: AuthorsComponent },
      { path: 'genres', component: GenresComponent },
      { path: 'add-book', component: AddBookComponent },
      { path: '', redirectTo: 'books', pathMatch: 'full' }
    ]
  }
];