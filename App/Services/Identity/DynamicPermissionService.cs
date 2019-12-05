using System.Security.Claims;

namespace App.Services.Identity
{
    public class DynamicPermissionService : IDynamicPermissionService
    {
        public bool CanAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var key = $"{area}:{controller}:{action}";

            if (user.IsInRole("sa"))
                return true;

            return user.HasClaim(ConstantPolicies.DynamicPermission, key);
        }
    }
}