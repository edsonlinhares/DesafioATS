using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using DesafioATS.WebAPI.Identidade;
using DesafioATS.WebAPI.Identidade.Models.Login;
using DesafioATS.Domain.Interfaces;
using MediatR;
using DesafioATS.Domain.Core.Notifications;

namespace DesafioATS.WebAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator, AuthenticationService authenticationService, IAspNetUser user,
            IConfiguration configuration) : base(notifications, mediator)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarViewModel obj)
        {
            var user = await _authenticationService.UserManager.FindByEmailAsync(obj.Email);

            if (user != null)
            {
                NotificarErro("Usuário já registrado.");
                return CustomResponse();
            }

            user = new Identidade.Models.ApplicationUser();
            user.Id = Guid.NewGuid().ToString();
            user.UserName = obj.Email;
            user.NomeCompleto = obj.Nome;
            user.Email = obj.Email;
            user.EmailConfirmed = true;
            user.PhoneNumber = obj.Celular;
            user.PhoneNumberConfirmed = true;

            var result = await _authenticationService.UserManager.CreateAsync(user, obj.Senha);

            if (result.Succeeded)
            {
                await _authenticationService.SignInManager.SignInAsync(user, false);

                result = await _authenticationService.UserManager.AddToRoleAsync(user, obj.TipoLogin.ToString());

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        NotificarErro(item.Description);
                    }
                    return CustomResponse();
                }

                var _token = await _authenticationService.GerarJwt(obj.Email);

                return CustomResponse(_token);
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginUser)
        {
            string mensagem = "Usuário ou Senha incorretos.";

            var user = await _authenticationService.UserManager.FindByNameAsync(loginUser.UserName);

            if (user != null)
            {
                var result = await _authenticationService.SignInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);

                if (result.Succeeded)
                {
                    var _token = await _authenticationService.GerarJwt(loginUser.UserName);

                    return CustomResponse(_token);
                }

                if (result.IsLockedOut)
                {
                    NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas.");
                    return CustomResponse();
                }
            }

            NotificarErro(mensagem);

            return CustomResponse();
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                NotificarErro("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.ObterRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                NotificarErro("Refresh Token expirado");
                return CustomResponse();
            }

            var user = await _authenticationService.UserManager.FindByNameAsync(token.Username);

            return CustomResponse(await _authenticationService.GerarJwt(token.Username));
        }

        [Authorize]
        [HttpPut("alterar-senha")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordViewModel updatePassword)
        {
            string mensagem = "Usuário ou Senha incorretos.";

            var user = await _authenticationService.UserManager.FindByNameAsync(updatePassword.UserName);

            if (user == null)
            {
                NotificarErro(mensagem);
                return CustomResponse();
            }

            if (await _authenticationService.UserManager.CheckPasswordAsync(user, updatePassword.Senha) == false)
            {
                NotificarErro(mensagem);
                return CustomResponse();
            }

            var result = await _authenticationService.UserManager.ChangePasswordAsync(user, updatePassword.Senha, updatePassword.NovaSenha);

            if (result.Succeeded)
            {
                return CustomResponse("Sua senha foi alterada com sucesso!");
            }

            foreach (var item in result.Errors)
            {
                NotificarErro(item.Description);
            }

            return CustomResponse();
        }

        [HttpPost("esqueci-senha")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel obj)
        {
            var mensagem = "Um e-mail foi enviado para: " + obj.Email + " verifique sua caixa de entrada ou spam.";

            var user = await _authenticationService.UserManager.FindByEmailAsync(obj.Email);

            if (user == null)
            {
                NotificarErro("Usuário não localizado.");
                return CustomResponse();
            }

            var confirmationToken = await _authenticationService.UserManager.GeneratePasswordResetTokenAsync(user);
            confirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            var callbackUrl = $"{_configuration.GetSection("UrlFront").Value}{_configuration.GetSection("RouteResetPassword").Value}?code={confirmationToken}";

            //await _smtpService.SendEmailAsync(new string[] { user.Email }, "2bTech -> Reset de senha", callbackUrl);

            return CustomResponse(mensagem);
        }

        [HttpPost("reset-senha")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordConfirmationViewModel resetPassword)
        {
            string _code;

            var user = await _authenticationService.UserManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                NotificarErro("Usuário não localizado.");
                return CustomResponse();
            }

            _code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Code));

            var result = await _authenticationService.UserManager.ResetPasswordAsync(user, _code, resetPassword.Senha);

            if (result.Succeeded)
            {
                return CustomResponse("Sua senha foi alterada com sucesso!");
            }

            foreach (var item in result.Errors)
            {
                NotificarErro(item.Description);
            }

            return CustomResponse();
        }
    }
}