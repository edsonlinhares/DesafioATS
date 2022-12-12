using System;

namespace DesafioATS.WebAPI.Identidade.Models.Login
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public Guid UserId { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }
}
