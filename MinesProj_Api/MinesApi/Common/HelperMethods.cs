using Entities.Models.ViewModels;
using System.Security.Claims;

namespace MinesApi.Common
{
    public class HelperMethods
    {
        //HttpContext.User.Identity as ClaimsIdentity
        public static UserClaims? GetCurrentUserClaims(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                var useClaims = identity.Claims;

                return new UserClaims
                {
                    Id = int.Parse(useClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                    Username = useClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value,
                    GivenName = useClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value,
                    Surname = useClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname).Value,
                    Role = useClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role).Value
                };
            }
            return null;
        }
    }
}
