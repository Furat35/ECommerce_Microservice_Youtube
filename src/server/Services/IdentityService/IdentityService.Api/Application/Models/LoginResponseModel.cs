﻿namespace IdentityService.Api.Application.Models
{
    public class LoginResponseModel
    {
        public string UserName { get; set; }
        public string UserToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
