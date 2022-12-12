using System.Collections.Generic;
using System.Security.Claims;

namespace DesafioATS.WebAPI.Identidade.Models.Login
{
    public class ClaimResultModel
    {
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<ClaimViewModel> ClaimsModel { get; set; }
    }
}
