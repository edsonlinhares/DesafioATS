using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DesafioATS.WebAPI.Identidade.Models;
using DesafioATS.WebAPI.Identidade.Models.Login;
using DesafioATS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DesafioATS.WebAPI.Identidade
{
    public class AuthenticationService
    {
        public readonly SignInManager<ApplicationUser> SignInManager;
        public readonly UserManager<ApplicationUser> UserManager;
        public readonly RoleManager<IdentityRole> RoleManager;
        private readonly IdentityATSContext _context;

        private readonly IHttpContextAccessor _httpContext;

        private readonly IJsonWebKeySetService _jwksService;
        private readonly IAspNetUser _aspNetUser;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IdentityATSContext context, IConfiguration configuration, IJsonWebKeySetService jwksService,
            IAspNetUser aspNetUser, IHttpContextAccessor httpContext)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            RoleManager = roleManager;
            _context = context;
            _jwksService = jwksService;
            _aspNetUser = aspNetUser;
            _httpContext = httpContext;
        }

        public async Task<LoginResponseViewModel> GerarJwt(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            var claims = await ObterClaimsUsuario(user);
            var encodedToken = CodificarToken(claims.Claims);

            var refreshToken = await GerarRefreshToken(user);

            return ObterRespostaToken(encodedToken, user, refreshToken);
        }

        private async Task<ClaimResultModel> ObterClaimsUsuario(ApplicationUser user)
        {
            var claims = await UserManager.GetClaimsAsync(user);
            var userRoles = await UserManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("Nome", $"{user.NomeCompleto}"));
            claims.Add(new Claim("Id", user.Id));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            return new ClaimResultModel { Claims = claims };
        }

        private string CodificarToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var currentIssuer = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}";

            var key = _jwksService.GetCurrentSigningCredentials();

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = currentIssuer,
                Subject = identityClaims,
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = key
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponseViewModel ObterRespostaToken(string encodedToken, ApplicationUser user,
            RefreshToken refreshToken)
        {
            return new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                UserId = Guid.Parse(user.Id),
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Nome = user.NomeCompleto,
                    Email = user.Email,
                    Telefone = user.PhoneNumber
                }
            };
        }

        private async Task<RefreshToken> GerarRefreshToken(ApplicationUser user)
        {
            var refreshToken = new RefreshToken
            {
                Username = user.UserName,
                ExpirationDate = DateTime.Now.AddHours(72)
            };

            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(u => u.Username == user.UserName));
            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken> ObterRefreshToken(Guid refreshToken)
        {
            var token = await _context.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Token == refreshToken);

            return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now ? token : null;
        }

        public async Task CreateRoles()
        {
            string[] rolesNames = {
                "Recrutador",
                "Candiato"
            };

            IdentityResult result;

            foreach (var namesRole in rolesNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }

        public async Task<string> GetRoles(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user is null) return null;
            var roles = await UserManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

    }
}
