using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

//This is a Custom Filter which provide the basic authentication on the Web API controller
//This filter first checks whether the Authorization in Header is null or not.
//If it is null then it will show unauthorizes
//If not then it will check matching record of username and password in the database

namespace CustomerWebAPIService.Models
{
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                //Here authenticationToken will be Base64 encoded
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                //To decode Base64 encoded username and password
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if (CustomerAuthenticate.LoginUser(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }


            }
        }
    }
}