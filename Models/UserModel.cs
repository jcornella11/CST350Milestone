using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CST350Milestone.Models
{
    public class UserModel
    {
        [Required, DisplayName("Users first name")]
        [StringLength(10, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, DisplayName("Users last name")]
        [StringLength(10, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required, DisplayName("Users sex")]
        public string Sex { get; set; }

        [Required, DisplayName("Users age")]
        [Range(1, 119)]
        public int Age { get; set; }

        [Required, DisplayName("Users home state")]
        public string State { get; set; }

        [Required, DisplayName("Users email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DisplayName("Username")]
        [StringLength(10, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, DisplayName("Password")]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        public UserModel(string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
        }

        public UserModel()
        {

        }
    }
}
