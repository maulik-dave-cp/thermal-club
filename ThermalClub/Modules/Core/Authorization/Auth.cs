using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ThermalClub.Modules.Core.Authorization
{
    public class Auth
    {
	    private readonly HttpContext _httpContext;

        public Auth(IHttpContextAccessor httpContext)
        {
	        _httpContext = httpContext.HttpContext;
        }

        public AuthUser LoggedInUser()
        {
            try
            {
	            var authorization = _httpContext.Request.Headers["Authorization"].FirstOrDefault();
                var token = authorization?.Substring("Bearer ".Length);

                return AuthHelper.VerifyAndGetLoggedInUser(token);
            }
            catch
            {
                return null;
            }

        }
    }

    public class AuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public static class AuthHelper
    {
        private const string Secret = "iHXSFfZOZjS5ojQkG0pQqxD29h7KRuhp";

        public static AuthUser VerifyAndGetLoggedInUser(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, Secret, verify: false);

                var user = new AuthUser();

                var jObject = JObject.Parse(json);
                user.Name = (string)jObject["name"];
                user.Email = (string)jObject["email"];
                user.Id = (int)jObject["id"];

                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
