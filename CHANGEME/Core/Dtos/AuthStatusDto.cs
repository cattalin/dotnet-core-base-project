using System;

namespace Core.Dtos
{
    public class AuthStatusDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
