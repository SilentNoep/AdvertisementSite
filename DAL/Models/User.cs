using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
       
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your first name!"), DisplayName("First Name:"), MaxLength(25), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name!"), DisplayName("Last Name:"), MaxLength(25), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }
        [DataType(DataType.Date), Required(ErrorMessage = "Please enter your Birth Day address!"), DisplayName("Day of Birth:")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Please enter your Email address!"), DisplayName("Email Address:"), StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your User Name!"), DisplayName("User Name:"), RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed."), StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please enter your Password!"), DisplayName("Password:"), RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed."), StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    



 
    }
}
