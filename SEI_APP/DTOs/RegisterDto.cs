using System;
using System.ComponentModel.DataAnnotations;

namespace  SEI_APP.DTOs
{
    public class RegisterDto
    {
       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Birthdate { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
