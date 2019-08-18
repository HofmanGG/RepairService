namespace HelloSocNetw_PL.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public int CountryId { get; set; }

        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
    }
}