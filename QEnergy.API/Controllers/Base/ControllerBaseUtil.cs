using QEnergy.API.Utils;
using System.Security.Claims;

namespace QEnergy.API.Controllers.Base
{
    public static class ControllerBaseUtil
    {
        public static Guid GetUserId(IEnumerable<ClaimsIdentity> identities)
        {
            var result = default(Guid);
            var userIdentity = identities?.FirstOrDefault(x => x.Claims.Any(y => y.Type == JwtTokenUtil.UserIdClaim));
            if (userIdentity != null)
            {
                var userIdClaimValue = userIdentity.Claims.FirstOrDefault(x => x.Type == JwtTokenUtil.UserIdClaim)?.Value;
                if (!string.IsNullOrEmpty(userIdClaimValue))
                    Guid.TryParse(userIdClaimValue, out result);
            }
            return result;
        }

        public static string GetUsername(IEnumerable<ClaimsIdentity> identities)
        {
            string result = null;
            var userIdentity = identities?.FirstOrDefault(x => x.Claims.Any(y => y.Type == JwtTokenUtil.UserIdClaim));
            if (userIdentity != null)
                result = userIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            return result;
        }

        public static int GetCompanyId(IEnumerable<ClaimsIdentity> identities)
        {
            var result = default(int);
            var userIdentity = identities?.FirstOrDefault(x => x.Claims.Any(y => y.Type == JwtTokenUtil.UserIdClaim));
            if (userIdentity != null)
            {
                var companyIdClaimValue = userIdentity.Claims.FirstOrDefault(x => x.Type == JwtTokenUtil.CompanyIdClaim)?.Value;
                if (!string.IsNullOrEmpty(companyIdClaimValue))
                    _ = int.TryParse(companyIdClaimValue, out result);
            }
            return result;
        }
    }
}
