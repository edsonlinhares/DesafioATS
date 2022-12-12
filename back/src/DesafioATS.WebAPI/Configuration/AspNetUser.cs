using DesafioATS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DesafioATS.WebAPI.Configuration
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string ObterId()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.ObterId() : "";
        }

        public string ObterRefreshToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.ObterRefreshToken() : "";
        }

        public string ObterToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.ObterToken() : "";
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string ObterNomeCompleto()
        {
            return _accessor.HttpContext.User.ObterNomeCompleto();
        }

        public string ObterPerfil()
        {
            return _accessor.HttpContext.User.ObterPerfil();
        }
    }
}
