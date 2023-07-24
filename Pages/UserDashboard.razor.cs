using Bank.Client.UI.Services;
using BankProject.Shared;
using Microsoft.AspNetCore.Components;

namespace Bank.Client.UI.Pages
{
    public partial class UserDashboard
    {
        [Inject]
        public IAccountService? AccountService { get; set; }
        public List<AccountDto> AccountList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AccountList = (await AccountService.GetAccountsByUserAsync()).ToList();
        }
    }
}
