using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> AddAccountAsync(Account account);
        Task DeleteAccountAsync(Guid accountId);
        Task<Account> GetAccountByPhoneNumberAsync(string phoneNumber);
        Task<Account> UpdateAccountAsync(Account account);
    }
}
