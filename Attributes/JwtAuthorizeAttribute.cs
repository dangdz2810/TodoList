using Authentication.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Attributes
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {

        public JwtAuthorizeAttribute(
            params string[] roles
            )
            : base(typeof(JwtAuthorizeFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
