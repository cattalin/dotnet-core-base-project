using System;

namespace Core.Models
{
    public class AuthStatusDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
