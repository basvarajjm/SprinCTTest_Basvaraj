using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace SprinCTTest_Basvaraj.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string auth = httpContext.Request.Headers.Authorization;
            if (!string.IsNullOrEmpty(auth) && auth.ToLower().StartsWith("basic"))
            {
                string encodedUsrNameAndPasswd = auth.Substring(6);
                var byteData = Convert.FromBase64String(encodedUsrNameAndPasswd);
                Encoding encoding = Encoding.UTF8;
                string usrNameAndPasswd = encoding.GetString(byteData);
                int colonIndex = usrNameAndPasswd.IndexOf(':');
                string userName = usrNameAndPasswd.Substring(0, colonIndex);
                string password = usrNameAndPasswd.Substring(colonIndex + 1);
                if (userName == "test" && password == "test123")
                {
                    await _next(httpContext);
                    return;
                }
            }
            httpContext.Response.StatusCode = 401;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
