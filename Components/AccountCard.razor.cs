using BankProject.Shared;
using Microsoft.AspNetCore.Components;

namespace Bank.Client.UI.Components
{
    public partial class AccountCard
    {
        [Parameter]
        public AccountDto Account { get; set; }
    }
}
