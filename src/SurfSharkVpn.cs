using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace SurfSharkVpnApi
{
    public class SurfSharkVpn
    {
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://api.surfshark.com";
        private string token;
        public SurfSharkVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "SurfsharkAndroid/2.8.7.4 (com.surfshark.vpnclient.android; release; playStore; 208070400; device/mobile)");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetServerClusters()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/v4/server/clusters/all");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetServerUser()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/v1/server/user");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Register(string email, string password)
        {
            var data = JsonContent.Create(new { email = email, password = password });
            var response = await httpClient.PostAsync($"{apiUrl}/v1/account/users", data);
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        public async Task<string> Login(string email, string password)
        {
            var data = JsonContent.Create(new { username = email, password = password });
            try
            {
                var response = await httpClient.PostAsync($"{apiUrl}/v1/auth/login", data);
                var responseContent = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseContent);
                if (doc.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    token = tokenElement.GetString();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return responseContent;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public async Task<string> GetAccountInfo()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/v1/account/users/me");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountNotifications()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/v2/notification/me");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNotificationsAuth()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/v1/notification/authorization");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
