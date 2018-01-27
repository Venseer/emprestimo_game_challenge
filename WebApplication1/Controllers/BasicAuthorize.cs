using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

public class BasicAuthenticationAttribute : ActionFilterAttribute
{
    public string BasicRealm { get; set; }
    protected string Username { get; set; }
    protected string Password { get; set; }

    public BasicAuthenticationAttribute(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var req = filterContext.HttpContext.Request;
        String auth = req.Headers["Authorization"];
        if (!String.IsNullOrEmpty(auth))
        {
            var cred = Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
            var user = new { Name = cred[0], Pass = cred[1] };
            if (user.Name == Username && user.Pass == Password) return;
        }
        filterContext.HttpContext.Response.Headers.Add("WWW-Authenticate", System.String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
        filterContext.Result = new UnauthorizedResult();
    }
}