using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string Bank { get; set; }
        public string TypeAccount { get; set; }
        public string NumberAccount { get; set; }
    }
}
