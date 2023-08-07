using Bank.Client.UI;
using Bank.Client.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

//builder.Services.AddOidcAuthentication(options =>
//{
//    builder.Configuration.Bind("AzureAuth", options.ProviderOptions);
//    options.ProviderOptions.ResponseType = "code";
//    options.ProviderOptions.AdditionalProviderParameters.Add("audience",
//        "https://localhost:7000");
//});

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
});

builder.Services.AddApiAuthorization();

var accountsBaseAddress = "https://localhost:7000";
//builder.HostEnvironment.IsDevelopment()
//    ? "http://"
//        + builder.Configuration.GetSection("Services").GetSection("Accounts").GetValue(typeof(string), "Host")
//        + ":"
//        + builder.Configuration.GetSection("Services").GetSection("Accounts").GetValue(typeof(string), "Port")
//    : Environment.GetEnvironmentVariable("ACCOUNTS_API_ADDRESS");
//test

builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri(accountsBaseAddress);
}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


await builder.Build().RunAsync();
