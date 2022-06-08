using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchPortalFunction.Entities;
using WatchPortalFunction.Wrappers;

namespace WatchPortalFunction.Auth
{
    internal class AuthenticationService 
    {

        private readonly TokenIssuer _tokenIssuer;
        // Service class for performing authentication
        public AuthenticationService(TokenIssuer tokenIssuer)
        {
            _tokenIssuer = tokenIssuer;
        }

        [FunctionName("Authenticate")]
        public async Task<IActionResult> Authenticate(
        // https://stackoverflow.com/a/52748884/116051
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth")]
        Credentials credentials,
        ILogger log)
        {
            // Hardcoded admin user {user: "rost", password: "1234"}
            bool authlogin = credentials?.User.Equals("rost", StringComparison.InvariantCultureIgnoreCase) ?? false;
            bool authpassw = credentials?.Password.Equals("1234",StringComparison.InvariantCulture)?? false;
            bool authenticated = authlogin && authpassw;

            if (!authenticated)
            {
                return new UnauthorizedResult();
            }
            //var token = _tokenIssuer.IssueTokenForUser(credentials);
            return new OkObjectResult(_tokenIssuer.IssueTokenForUser(credentials));
        }
    }
}
