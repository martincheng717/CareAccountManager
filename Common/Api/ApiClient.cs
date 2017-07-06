using System;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Formatting;
using AttributeCaching;

namespace Gdot.Care.Common.Api
{
    [ExcludeFromCodeCoverage]
    public class ApiClient: IApiClient
    {
        private HttpClient _client;
        private HttpContext _context;
        private bool _wasSetup;
        private readonly dynamic _config = ConfigManager.Instance.Configuration;
        private static readonly ILogger Log = Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public int? PartnerAuthenticationKey { private get; set; }
        public string KeyStore { private get; set; }
        public ISqlCommand<PartnerAuthenticationCredentialInfo, int, string> PartnerAuthenticationCredentials { get; set;}
        public HttpClient Client
        {
            get { return _client ?? (_client = new HttpClient()); }
            set { _client = value; }
        }

        public ApiClient(){}
        /// <summary>
        /// If Api has authentication, pass in partnerAuthenticationKey
        /// </summary>
        /// <param name="partnerAuthenticationKey"></param>
        public ApiClient(int partnerAuthenticationKey, string keyStore)
        {
            PartnerAuthenticationKey = partnerAuthenticationKey;
            KeyStore = keyStore;
        }
        public ApiClient(int partnerAuthenticationKey, ISqlCommand<PartnerAuthenticationCredentialInfo, int, string> partnerAuthenticationCredentials)
        {
            PartnerAuthenticationKey = partnerAuthenticationKey;
            PartnerAuthenticationCredentials = partnerAuthenticationCredentials;
        }

        private async Task<HttpClient> SetupClient()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
            //if HttpClient is shared, Timeout only allowed to be assigned once, need to perform this check to avoid error
            if (!_wasSetup)
            {
                var timeout = _config.ApiConnectionTimeoutInSec.ToString();
                if (!string.IsNullOrEmpty(timeout))
                {
                    _client.Timeout = new TimeSpan(0, 0, 0, Convert.ToInt32(timeout));
                }
                _wasSetup = true;
            }
            //transfer all "x-" from previous api to the current api
            if (_context == null)
            {
                _context = HttpContext.Current;
            }
            if (_context != null)
            {
                var headers = _context.Request.Headers;
                foreach (var header in headers)
                {
                    var name = header.ToString();
                    if (name.ToLower(CultureInfo.CurrentCulture).StartsWith("x-") &&
                        !_client.DefaultRequestHeaders.Contains(name))
                    {
                        _client.DefaultRequestHeaders.Add(name, headers.GetValues(name));
                    }
                }
            }
            //add authentication 
            if (PartnerAuthenticationKey.HasValue)
            {
                if (!_client.DefaultRequestHeaders.Contains("authorization"))
                {
                    _client.DefaultRequestHeaders.Add("authorization", "Basic " + await GetEncodedAuthentication(PartnerAuthenticationKey.Value, KeyStore));
                }
                if (!_client.DefaultRequestHeaders.Contains("endusersecurityid"))
                {
                    _client.DefaultRequestHeaders.Add("endusersecurityid", _config.EndUserSecurityId.ToString());
                }
                if (!_client.DefaultRequestHeaders.Contains("enduserip"))
                {
                    _client.DefaultRequestHeaders.Add("enduserip", Utility.GetIpAddress);
                }
                if (!_client.DefaultRequestHeaders.Contains("apikey"))
                {
                    _client.DefaultRequestHeaders.Add("apikey", _config.ApiKey.ToString());
                }
            }

            return _client;
        }

        private static void EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Log.Error(new LogObject(CommonEventType.ApiClient.ToString(), $"An Api exception has occurred - HttpStatusCode={response.StatusCode}"), ex);
                throw new ApiRequestException(response.StatusCode, $"An Api exception has occurred - HttpStatusCode={response.StatusCode}", ex);
            }
        }
        [Cacheable(Days = 1)]
        private async Task<string> GetEncodedAuthentication(int partnerAuthenticationKey, string keyStore)
        {
            var encodedAuthentication = string.Empty;
            if (partnerAuthenticationKey > 0)
            {
                var auth = await PartnerAuthenticationCredentials.ExecuteAsync(partnerAuthenticationKey, keyStore);
                if (!string.IsNullOrEmpty(auth?.Login) && !string.IsNullOrEmpty(auth.EncryptedPassword))
                {
                    // Get plain text
                    string hashedPassword;
                    using (var sha1 = new SHA1CryptoServiceProvider())
                    {
                        hashedPassword = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(auth.PlainTextPassword)));
                    }
                    encodedAuthentication = Convert.ToBase64String((Encoding.ASCII.GetBytes(auth.Login + ":" + hashedPassword)));
                }
            }
            return encodedAuthentication;
        }

        #region Post
        public async Task<TResponse> PostAsync<TResponse, TRequest>(Uri uri, TRequest data, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null)
        {
            return await PostAsync<TResponse, TRequest>(uri, data, new JsonMediaTypeFormatter(), logOption,logMessage);
        }
        public async Task<TResponse> PostAsync<TResponse, TRequest>(Uri uri, TRequest data, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage=null)
        {
            logMessage = logMessage ?? new Dictionary<string, object>();
            logMessage.Add("RemoteRoute",uri.AbsoluteUri);
            logMessage.Add("Verb", "POST");
            var metric = new MetricWatcher(Constants.MetricClient, new MetricWatcherOption { ManualStartStop = true,LogMessage = logMessage });
            var isException = false;
            var client = await SetupClient();
            TResponse responseData;
            try
            {
                metric.Start();
                var content = new ObjectContent<TRequest>(data, mediaTypeFormatter);
                var response = await client.PostAsync(uri, content);
                EnsureSuccessStatusCode(response);
                responseData = await response.Content.ReadAsAsync<TResponse>(new[] { mediaTypeFormatter });
            }
            catch (Exception)
            {
                isException = true;
                throw;
            }
            finally
            {
                metric.Stop(isException);
            }
            return responseData;
        }
        #endregion

        #region Get

        public async Task<TResponse> GetAsync<TResponse>(Uri uri, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null)
        {
            return await GetAsync<TResponse> (uri, new JsonMediaTypeFormatter(), logOption,logMessage);
        }

        public async Task<TResponse> GetAsync<TResponse>(Uri uri, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string,object> logMessage=null)
        {
            logMessage = logMessage ?? new Dictionary<string, object>();
            logMessage.Add("RemoteRoute", uri.AbsoluteUri);
            logMessage.Add("Verb", "GET");
            var metric = new MetricWatcher(Constants.MetricClient, new MetricWatcherOption { ManualStartStop = true, LogMessage = logMessage });
            var isException = false;
            var client = await SetupClient();
            TResponse responseData;
            try
            {
                metric.Start();
                var response = await client.GetAsync(uri);
                EnsureSuccessStatusCode(response);
                responseData = await response.Content.ReadAsAsync<TResponse>(new[] { mediaTypeFormatter });
            }
            catch (Exception)
            {
                isException = true;
                throw;
            }
            finally
            {
                metric.Stop(isException);
            }
            return responseData;
        }
        #endregion

        #region Put

        public async Task<TResponse> PutAsync<TResponse, TRequest>(Uri uri, TRequest data, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null)
        {
            return await PutAsync<TResponse, TRequest>(uri, data, new JsonMediaTypeFormatter(), logOption,logMessage);
        }

        public async Task<TResponse> PutAsync<TResponse, TRequest>(Uri uri, TRequest data, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string,object> logMessage=null)
        {
            logMessage = logMessage ?? new Dictionary<string, object>();
            logMessage.Add("RemoteRoute", uri.AbsoluteUri);
            logMessage.Add("Verb", "PUT");
            var metric = new MetricWatcher(Constants.MetricClient, new MetricWatcherOption { ManualStartStop = true, LogMessage = logMessage });
            var isException = false;
            var client = await SetupClient();
            TResponse responseData;
            try
            {
                metric.Start();
                var content = new ObjectContent<TRequest>(data, mediaTypeFormatter);
                var response = await client.PutAsync(uri, content);
                EnsureSuccessStatusCode(response);
                responseData = await response.Content.ReadAsAsync<TResponse>(new[] { mediaTypeFormatter });

            }
            catch (Exception)
            {
                isException = true;
                throw;
            }
            finally
            {
                metric.Stop(isException);
            }
            return responseData;

        }
        #endregion
    }
}
