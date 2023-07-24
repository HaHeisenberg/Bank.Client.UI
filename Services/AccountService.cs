using BankProject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bank.Client.UI.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAccountsByUserAsync();
    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _accessTokenProvider;
        public AccountService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _accessTokenProvider = tokenProvider;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsByUserAsync()
        {
            //_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            //var response = await _httpClient.GetStreamAsync($"api/accounts/user");
            //var a = await JsonSerializer.DeserializeAsync<IEnumerable<AccountDto>>(response);

            var request = new HttpRequestMessage(HttpMethod.Get, "api/accounts/user");

            var tokenResult = await _accessTokenProvider.RequestAccessToken();
            tokenResult.TryGetToken(out var token);
            //Console.WriteLine("token");
            //Console.WriteLine(token.Value);
            _httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {token.Value}");


            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Console.WriteLine("response");
            Console.WriteLine(response.ToString());

            Console.WriteLine("rmessage");
            Console.WriteLine(response.RequestMessage);

            Console.WriteLine("content");
            Console.WriteLine(response.Content.ToString());
            response.EnsureSuccessStatusCode();

            var str = await response.Content.ReadAsStreamAsync();

            var a = await JsonSerializer.DeserializeAsync<IEnumerable<AccountDto>>(str, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            foreach (var acc in a)
            {
                Console.WriteLine("Konto:");
                Console.WriteLine(acc.Type);
            }
            
            //var response = await _httpClient.GetFromJsonAsync<List<AccountDto>>("api/accounts/user");

            return a;
        }
    }
}
