using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DesafioATS.WebAPI.Identidade.Models.Login;

namespace DesafioATS.WebAPI.Filters
{
    public class ApiActionFilter : IActionFilter
    {
        private Stopwatch sw = new Stopwatch();
        private readonly ILogger<ApiActionFilter> _logger;
        private IDictionary<string, object> _ActionArguments;

        public ApiActionFilter(ILogger<ApiActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            sw.Start();

            _ActionArguments = context.ActionArguments;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            int statusCode = 200;
            string jsonString = string.Empty;

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            var result = context.Result as ObjectResult;

            if (result == null && (StatusCodeResult)context.Result != null)
                statusCode = ((StatusCodeResult)context.Result).StatusCode;

            var _path = context.HttpContext.Request.Path.ToString().ToLower();
            var _method = context.HttpContext.Request.Method.ToUpper();

            if (_path.Equals("/api/v1/auth/entrar"))
            {
                var model = (LoginViewModel)_ActionArguments.FirstOrDefault().Value;
                model.Password = "**********";
                jsonString = JsonConvert.SerializeObject(model);
            }
            else if (_path.Equals("/api/v1/auth/alterar-senha"))
            {
                var model = (UpdatePasswordViewModel)_ActionArguments.FirstOrDefault().Value;
                model.Senha = "**********";
                model.NovaSenha = "**********";
                model.ConfirmarSenha = "**********";
                jsonString = JsonConvert.SerializeObject(model);
            }
            else if (_path.Equals("/api/v1/auth/reset-password"))
            {
                var model = (ResetPasswordConfirmationViewModel)_ActionArguments.FirstOrDefault().Value;
                model.Senha = "**********";
                model.ConfirmarSenha = "**********";
                jsonString = JsonConvert.SerializeObject(model);
            }
            else if (_path.Equals("/api/v1/auth/registrar") && (_method.Equals("POST") || _method.Equals("PUT")))
            {
                var model = (RegistrarViewModel)_ActionArguments.FirstOrDefault().Value;
                model.Senha = "**********";
                model.ConfirmarSenha = "**********";
                jsonString = JsonConvert.SerializeObject(model);
            }

            _logger.LogInformation(
                new EventId(1000, "Api"),
                "{method} | {EndPoint} | {Request} | {Result} | {Status} | {Tempo}",
                _method,
                _path,
                string.IsNullOrEmpty(jsonString) ? JsonConvert.SerializeObject(_ActionArguments) : jsonString,
                result is null ? "{}" : JsonConvert.SerializeObject(result.Value),
                result is null ? statusCode : result.StatusCode,
                elapsedTime);
        }
    }
}
