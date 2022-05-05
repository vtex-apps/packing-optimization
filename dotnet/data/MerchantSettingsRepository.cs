namespace PackingOptimization.Data
{
    using Microsoft.AspNetCore.Http;
    using PackingOptimization.Models;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using PackingOptimization.Services;
    using Newtonsoft.Json;

    public class MerchantSettingsRepository : IMerchantSettingsRepository
    {
        private const string SETTINGS_NAME = "merchantSettings";
        private const string BUCKET = "packing_optimization";
        public const string HEADER_VTEX_CREDENTIAL = "X-Vtex-Credential";
        public const string HEADER_VTEX_ACCOUNT = "X-Vtex-Account";
        public const string HEADER_VTEX_WORKSPACE = "X-Vtex-Workspace";
        public const string APPLICATION_JSON = "application/json";
        private readonly IVtexEnvironmentVariableProvider _environmentVariableProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _applicationName;
        private string AUTHORIZATION_HEADER_NAME;


        public MerchantSettingsRepository(IVtexEnvironmentVariableProvider environmentVariableProvider, IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory)
        {
            this._environmentVariableProvider = environmentVariableProvider ??
                                                throw new ArgumentNullException(nameof(environmentVariableProvider));

            this._httpContextAccessor = httpContextAccessor ??
                                        throw new ArgumentNullException(nameof(httpContextAccessor));

            this._clientFactory = clientFactory ??
                                  throw new ArgumentNullException(nameof(clientFactory));

            this._applicationName =
                $"{this._environmentVariableProvider.ApplicationVendor}.{this._environmentVariableProvider.ApplicationName}";

            AUTHORIZATION_HEADER_NAME = "Authorization";
        }

        public async Task<MerchantSettings> GetMerchantSettings()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://vbase.{this._environmentVariableProvider.Region}.vtex.io/{this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_ACCOUNT]}/{this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_WORKSPACE]}/buckets/{this._applicationName}/{BUCKET}/files/{SETTINGS_NAME}"),
            };

            string authToken = this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_CREDENTIAL];
            if (authToken != null)
            {
                request.Headers.Add(AUTHORIZATION_HEADER_NAME, authToken);
            }

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            MerchantSettings merchantSettings = JsonConvert.DeserializeObject<MerchantSettings>(responseContent);
            return merchantSettings;
        }

        public async Task<bool> SetMerchantSettings(MerchantSettings merchantSettings)
        {
            if (merchantSettings == null)
            {
                merchantSettings = new MerchantSettings();
            }

            var jsonSerializedMerchantSettings = JsonConvert.SerializeObject(merchantSettings);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://vbase.{this._environmentVariableProvider.Region}.vtex.io/{this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_ACCOUNT]}/{this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_WORKSPACE]}/buckets/{this._applicationName}/{BUCKET}/files/{SETTINGS_NAME}"),
                Content = new StringContent(jsonSerializedMerchantSettings, Encoding.UTF8, APPLICATION_JSON)
            };

            string authToken = this._httpContextAccessor.HttpContext.Request.Headers[HEADER_VTEX_CREDENTIAL];
            if (authToken != null)
            {
                request.Headers.Add(AUTHORIZATION_HEADER_NAME, authToken);
            }

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
