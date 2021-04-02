using ApiModel.ExternalContracts.Contracts;
using ApiModel.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiModel.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&accesstoken=EAAGWh2Gn5MsBAA2gZA3TZC4oliCZBvkNkhKoGY30ZBXHVtTPmhZCtDY2hU73ZAWbexognIN1iySsSJmVn7FGCEdVv7fqyVRSog4XojCJi6xUjKXqPCjCI4qHndKJKLv4UBEkavPBLGoD5mi8nJJCsndXjWCevNoGaYya2Hz37dUNZAUBklktngQg6aAAOe5K62fsBBFows07SLFb2RPuLZAcZA1hLYEsOkGsdhJd9vmXZBgQZDZD";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&accesstoken=EAAGWh2Gn5MsBAA2gZA3TZC4oliCZBvkNkhKoGY30ZBXHVtTPmhZCtDY2hU73ZAWbexognIN1iySsSJmVn7FGCEdVv7fqyVRSog4XojCJi6xUjKXqPCjCI4qHndKJKLv4UBEkavPBLGoD5mi8nJJCsndXjWCevNoGaYya2Hz37dUNZAUBklktngQg6aAAOe5K62fsBBFows07SLFb2RPuLZAcZA1hLYEsOkGsdhJd9vmXZBgQZDZD";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;


        public FacebookAuthService(FacebookAuthSettings facebookAuthSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthSettings = facebookAuthSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(TokenValidationUrl, accessToken, _facebookAuthSettings.AppId,
                _facebookAuthSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);

        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken, _facebookAuthSettings.AppId,
               _facebookAuthSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }

        Task<FacebookUserInfoResult> IFacebookAuthService.GetUserInfoAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

    }
}
