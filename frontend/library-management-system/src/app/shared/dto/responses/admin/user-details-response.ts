import { RentalDetails } from './rental-details-response';

export interface UserDetailsResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  rentals: RentalDetails[];
}