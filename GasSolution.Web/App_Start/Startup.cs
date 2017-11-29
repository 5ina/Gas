using System;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(GasSolution.Web.App_Start.Startup))]

namespace GasSolution.Web.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            //app.UseAbp();

            //app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
                        

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieHttpOnly = true,
                CookieName = "__GAS_WEB"
            });

            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);


        }

        private static bool IsTrue(string appSettingName)
        {
            return string.Equals(
                ConfigurationManager.AppSettings[appSettingName],
                "true",
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
