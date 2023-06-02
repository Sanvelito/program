using program.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace program.Helpers
{
    class AuthHeaderHandler : DelegatingHandler
    {

        private readonly IApiAuthService _apiAuthService;
        public AuthHeaderHandler(IApiAuthService apiService)
        {
            _apiAuthService = apiService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await SecureStorage.GetAsync("access_token");
            var refreshToken = await SecureStorage.GetAsync("refresh_token");

            //potentially refresh token here if it has expired etc.
            DateTime expirationDate = GetTokenExpirationDate(accessToken);
            if (expirationDate <= DateTime.Now)
            {
                accessToken = await _apiAuthService.RefreshToken(refreshToken);
                await SecureStorage.SetAsync("access_token", accessToken);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        private DateTime GetTokenExpirationDate(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);

            // Получаем значение из claim с именем "exp" (срок действия токена в формате Unix Epoch).
            var expirationClaim = token.Claims.FirstOrDefault(claim => claim.Type == "exp");

            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long expirationSeconds))
            {
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationSeconds).UtcDateTime;
                return expirationDateTime;
            }
            return DateTime.MinValue;
        }
    }
}
