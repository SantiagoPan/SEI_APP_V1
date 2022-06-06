using System;
using System.ComponentModel.DataAnnotations;

namespace  SEI_APP.DTOs
{
    public class RegisterDto
    {
        public string IdUser { get; set; }
        public string State { get; set; }
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
        public InfoBank BankInfo { get; set; }

        public class InfoBank
        {
            public int Id { get; set; }
            public string IdUser { get; set; }
            public string Bank { get; set; }
            public string TypeAccount { get; set; }
            public string NumberAccount { get; set; }
        }
        
        public class UpdateInfoUser
        {
            public userInfo user { get; set; }
            public class userInfo
            {
                public string IdUser { get; set; }
                public string State { get; set; }
                public string FirstName { get; set; }
                public string Lastname { get; set; }
                public string UserName { get; set; }
                public string DocumentType { get; set; }
                public string Document { get; set; }
                public string Phone { get; set; }
                public string Address { get; set; }
                public string Birthdate { get; set; }
                public string Email { get; set; }
            }

        }
        public class RecoverPass
        {
            public string TipoDocumento { get; set; }
            public string Documento { get; set; }
            public string Email { get; set; }
        }
    }
}
