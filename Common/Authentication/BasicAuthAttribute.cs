using Gdot.Care.Common.Interface;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Gdot.Care.Common.Db;
using Gdot.Care.Common.Model;

namespace Gdot.Care.Common.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    [ExcludeFromCodeCoverage]
    public class BasicAuthAttribute : AuthorizationFilterAttribute
    {
        public BasicAuthAttribute()
        {
            CmdClientAuthenticationValidation = new ClientAuthenticationValidation();
            CmdGetClientAuthenticationSalt = new GetClientAuthenticationSalt();
        }

        public ISqlCommand<int, ClientAuthenticationValidationInput> CmdClientAuthenticationValidation { get; set; }
        public ISqlCommand<string, GetClientAuthenticationSaltInput> CmdGetClientAuthenticationSalt { get; set; }
        public int ClientAuthenticationPartnerKey { get; set; }


        public override async Task OnAuthorizationAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (ClientAuthenticationPartnerKey == 0)
            {
                throw new Exception("ClientAuthenticationPartnerKey is required");
            }

            if (actionContext.Request.Headers.Contains("Authorization"))
            {
                var authHeaderVal =
                    AuthenticationHeaderValue.Parse(
                        actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault());
                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                    StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    var authenticated = await AuthenticateUser(authHeaderVal.Parameter);
                    if (!authenticated)
                    {
                        Challenge(actionContext);
                    }
                }


            }
            else
            {
                Challenge(actionContext);
            }

            await base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        private static void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
        }

        private async Task<bool> AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                return await CheckPassword(name, password);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Call database and check password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<bool> CheckPassword(string username, string password)
        {
            var saltValue =
                await
                    CmdGetClientAuthenticationSalt.ExecuteAsync(new GetClientAuthenticationSaltInput
                    {
                        UserName = username,
                        ClientAuthenticationPartnerKey = this.ClientAuthenticationPartnerKey
                    });
            if (saltValue == string.Empty) //record not found
            {
                return false;
            }
            var hashPassword =
                Convert.ToBase64String(HashUltility.GenerateSaltedHash(HashUltility.ToByteArray(password),
                    Convert.FromBase64String(saltValue)));
            var clientAuthenticationKey =
                await
                    CmdClientAuthenticationValidation.ExecuteAsync(new ClientAuthenticationValidationInput()
                    {
                        ClientAuthenticationPartnerKey = this.ClientAuthenticationPartnerKey,
                        UserName = username,
                        HashPassword = hashPassword
                    });
            return clientAuthenticationKey > 0;
        }


    }
}
