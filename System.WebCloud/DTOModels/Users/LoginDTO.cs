﻿

using System.ComponentModel.DataAnnotations;

namespace System.WebCloud.DTOModels.Users
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        
    }
}
