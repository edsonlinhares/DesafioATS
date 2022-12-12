using System;
using System.Security.Claims;

namespace DesafioATS.WebAPI.Configuration
{
    public static class ClaimsPrincipalExtensions
    {
        public static string ObterId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("Id");
            return claim?.Value;
        }

        public static string ObterToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }

        public static string ObterRefreshToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("RefreshToken");
            return claim?.Value;
        }

        public static string ObterNomeCompleto(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("Nome");
            return claim?.Value;
        }

        public static string ObterPerfil(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Role);
            return claim?.Value;
        }
    }
}
