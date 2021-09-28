using System;
using ThermalClub.Modules.CurrentProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ThermalClub.Modules.Core.Api
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string[] permissions)
        {
            Permissions = permissions;
        }
        public string[] Permissions { get; set; }
    }

    public class AuthorizeApiAdminUserAttribute : TypeFilterAttribute
    {
        public AuthorizeApiAdminUserAttribute(params string[] permissions)
            : base(typeof(AdminPermissionFilter))
        {
            Arguments = new object[] { new PermissionRequirement(permissions) };
            Order = int.MinValue;
        }
    }

    public class AdminPermissionFilter : Attribute, IAuthorizationFilter
    {
        private readonly ThermalConfiguration _config;
        private readonly PermissionRequirement _requirement;

        public AdminPermissionFilter(
            ThermalConfiguration config,
            PermissionRequirement requirement)
        {
            _config = config;
            _requirement = requirement;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // bool ok = await _authService.AuthorizeAsync(context.HttpContext.User, null, _requirement);
            // bool ok = false;

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            var authorizationHeader = Convert.ToString(request.Headers["Authorization"]);


            //if (!string.IsNullOrEmpty(authorizationHeader) &&
            //    authorizationHeader.StartsWith("Bearer "))
            //{
            //    var token = authorizationHeader.Substring("Bearer ".Length);

            //    try
            //    {
            //        IJsonSerializer serializer = new JsonNetSerializer();
            //        IDateTimeProvider provider = new UtcDateTimeProvider();
            //        IJwtValidator validator = new JwtValidator(serializer, provider);
            //        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            //        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            //        var json = decoder.Decode(token, _config.Backend.JwtSecret, verify: true);

            //        //AuthUser

            //        //var jObject = JObject.Parse(json);
            //        //name = (string) jUser["name"];
            //        //teamname = (string) jUser["teamname"];
            //        //email = (string) jUser["email"];
            //        //players = jUser["players"].ToArray();

            //        // Write logic for verify permissions
            //    }
            //    catch (TokenExpiredException)
            //    {
            //        //Console.WriteLine("Token has expired");
            //        response.WriteAsync(JsonConvert.SerializeObject("Token has expired."));
            //        response.StatusCode = 419;
            //        response.ContentType = "application/json";
            //    }
            //    catch (SignatureVerificationException)
            //    {
            //        response.WriteAsync(JsonConvert.SerializeObject("Token has invalid signature"));
            //        response.StatusCode = 400;
            //        response.ContentType = "application/json";
            //    }
            //    catch
            //    {
            //        new UnauthorizedResult();
            //    }
            //}


            //context.HttpContext.Response.ContentType = "application/json";
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            //var obj = new
            //{
            //    name = "AUTHENTICATION_FAILURE",
            //    message = "Authentication failed due to invalid authentication credentials or a missing Authorization header.",
            //};

            //context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(obj));
            //new UnauthorizedResult();
        }
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //public class CheckAccessAttribute : AuthorizeAttribute, IAuthorizationFilter
    //{
    //    private string[] _permission;
    //    public CheckAccessAttribute(params string[] permission)
    //    {
    //        _permission = permission;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        context.HttpContext.Response.ContentType = "application/json";
    //        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

    //        var obj = new
    //        {
    //            name = "AUTHENTICATION_FAILURE",
    //            message = "Authentication failed due to invalid authentication credentials or a missing Authorization header."
    //        };

    //        context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(obj));
    //    }
    //}

}
