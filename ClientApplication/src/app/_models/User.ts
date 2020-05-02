export interface User {
   id: number;

   appIdentityUserId: string;

   firstName: string;
   lastName: string;

   dayOfBirth: number;
   monthOfBirth: number;
   yearOfBirth: number;

   roles: string[];

   gender: string;

   countryId: number;
   countryName: string;

   token: string;
}
