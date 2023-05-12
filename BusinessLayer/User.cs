using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "FirstName must be up to 20 letters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "LastName must be up to 20 letters")]
        public string LastName { get; set; }

        [Range(18, 81, ErrorMessage = "Age must be between 18 and 81")]
        public int Age { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "UserName must be up to 20 symbols")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Email must be up to 20 symbols")]
        public string Email { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Password must be up to 70 symbols")]
        public string Password { get; set; }
        public List<User> Friends { get; set; }
        public List<Interest> Interests { get; set; }

        private User()
        {
            Friends = new List<User>();
            Interests = new List<Interest>();
        }
        public User(string firstName, string lastName, int age, string userName, string password, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UserName = userName;
            Password = password;
            Email = email;
            Friends = new List<User>();
            Interests = new List<Interest>();
        }
    }
}