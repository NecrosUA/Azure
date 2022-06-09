using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WatchPortalFunction.Auth
{
    internal class ChangePassword
    {
        [FunctionName("ChangePassword")]
        public async Task<IActionResult> ChangePwd(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "changepassword")]
        HttpRequest req, // Note: we need the underlying request to get the header
        ILogger log)
        {
            // Check if we have authentication info.
            AuthenticationInfo auth = new AuthenticationInfo(req);

            if (!auth.IsValid)
            {
                return new UnauthorizedResult(); // No authentication info.
            }

            string newPassword = await req.ReadAsStringAsync();

            return new OkObjectResult($"{auth.Username} changed password to {newPassword}");
        }
    }
}
