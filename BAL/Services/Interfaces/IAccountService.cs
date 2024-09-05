using BAL.Requests;
using BAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> GetAccountByIdAsync(Guid accountId);
        Task<IEnumerable<AccountResponse>> GetAllAccountsAsync();
        Task<AccountResponse> RegisterAccountAsync(RegisterAccountRequest request);
        Task<AccountResponse> GetAccountByPhoneNumberAsync(string phoneNumber);
    }
}
