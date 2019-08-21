export class User 
{
constructor(
  public userInfoId: number,

  public firstName: string,
  public lastName: string,

  public dayOfBirth: number,
  public monthOfBirth: number,
  public yearOfBirth: number,

  public gender: string,

  public countryId: number,
  public countryName: string,
  
  public token: string) { }
}