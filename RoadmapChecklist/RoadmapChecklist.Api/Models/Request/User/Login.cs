using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoadmapChecklist.Api.Models.Request.User
{
    public class Login
    {
        [Required(ErrorMessage = "Email field is required !"),
         DataType(DataType.EmailAddress),
         EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required !")]
        public string Password { get; set; }
    }
}
