namespace TokenApp.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public OrderService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<string> GetOrdersAsync()
        {
            var token = await _tokenService.GetTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/orders");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
