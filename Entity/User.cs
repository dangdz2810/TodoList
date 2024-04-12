﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "user name is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        [RegularExpression(
            "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])((?=.*\\W)|(?=.*_))^[^ ]+$",
            ErrorMessage = "Password must contains at least one lower case char, upper case char, digit and symbol"
        )]
        public string? Password { get; set; }
        [Required(ErrorMessage = "email is required")]
        [RegularExpression("^[\\w\\.-]+@[\\w\\.-]+\\.[a-zA-Z]{2,}$")]
        public string? Email { get; set; }

        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
