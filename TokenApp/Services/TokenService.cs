using System.Text.Json;
using TokenApp.Models;

namespace TokenApp.Services
{
    public class TokenService
    {
        private static TokenResponse _cachedToken;
        private static DateTime _tokenExpiration;
        private static int _requestCount = 0;

        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync()
        {
            // Eğer geçerli token varsa onu döndürelim
            if (_cachedToken != null && DateTime.Now < _tokenExpiration)
                return _cachedToken.access_token;

            // Sınır kontrolümüz
            if (_requestCount >= 5)
                throw new Exception("Hourly Token Request Limit Exceeded.");

            // Yeni token alalım
            var response = await _httpClient.PostAsync("https://api.example.com/token", null);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _cachedToken = JsonSerializer.Deserialize<TokenResponse>(content);
            _tokenExpiration = DateTime.Now.AddSeconds(_cachedToken.expires_in - 60); // 1dk erken bitecek şekilde ayarladık
            _requestCount++;

            return _cachedToken.access_token;
        }
    }

}
