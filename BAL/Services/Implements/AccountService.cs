using AutoMapper;
using BAL.Requests;
using BAL.Response;
using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountResponse> RegisterAccountAsync(RegisterAccountRequest request)
        {
            var existingAccount = await _accountRepository.GetAllAsync();
            if (existingAccount.Any(a => a.Email == request.Email || a.PhoneNumber == request.PhoneNumber))
            {
                throw new Exception(ResponseMessages.AccountAlreadyExists);
            }

            var account = _mapper.Map<Account>(request);
            account.CreatedDate = DateTime.UtcNow;

            var createdAccount = await _accountRepository.AddAccountAsync(account);
            return _mapper.Map<AccountResponse>(createdAccount);
        }

        public async Task<AccountResponse> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetAccountByPhoneNumberAsync(string phoneNumber)
        {
            var account = await _accountRepository.GetAccountByPhoneNumberAsync(phoneNumber);
            if (account == null)
            {
                throw new ValidationException(ResponseMessages.AccountNotFound);
            }
            return _mapper.Map<AccountResponse>(account);
        }
    }
}
