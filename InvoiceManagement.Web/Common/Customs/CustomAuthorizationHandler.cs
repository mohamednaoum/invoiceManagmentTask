namespace InvoiceManagement.Web.Common.Customs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public CustomAuthorizationHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var token = httpContext.Request.Cookies["AuthToken"];
        if (token == null)
        {
            var username = httpContext.Request.Form["username"];
            var password = httpContext.Request.Form["password"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync($"{_configuration["ApiUrl"]}/api/auth/login", new { username, password });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    token = result["Token"];
                    httpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions { HttpOnly = true });

                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    var claims = jwtToken.Claims;

                    var identity = new ClaimsIdentity(claims, "jwt");
                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    httpContext.User = claimsPrincipal;

                    // Manually add the claims to the AuthorizationHandlerContext
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
        else
        {
            // If the token is present in cookies, validate and set the user context
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            var identity = new ClaimsIdentity(claims, "jwt");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            httpContext.User = claimsPrincipal;
            context.Succeed(requirement);
        }
    }
}
