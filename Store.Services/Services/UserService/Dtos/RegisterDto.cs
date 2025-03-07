﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.UserService.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression ("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^amp;*()-+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            
     ErrorMessage ="Password must have 1 Uppercase, 1 Lowercase,1number,1non alphan "       )]
        public string Password { get; set; }
    }
}
