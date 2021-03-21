using System.ComponentModel.DataAnnotations;

namespace RoadmapChecklist.Api.Models.Request.User
{
    public class Register
    {
        [Required(ErrorMessage = "UserName field is required !"),
         StringLength(50, ErrorMessage = "UserName length should be a maximum of 50 characters.") ]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Email field is required !"),
         DataType(DataType.EmailAddress),
         EmailAddress ]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password field is required !"),
         StringLength(20, ErrorMessage = "Password length should be a maximum of 20 characters.") ]
        public string Password { get; set; }
    }
}