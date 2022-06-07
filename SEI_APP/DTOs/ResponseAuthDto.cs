using System;

namespace SEI_APP.DTOs
{
    public class ResponseAuthDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string IdUser { get; set; }
        public bool IsAdmin { get; set; }
        public int Refesh { get; set; }

    }
}
