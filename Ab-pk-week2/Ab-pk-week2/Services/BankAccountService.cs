using Ab_pk_week2.DBOperations;
using Ab_pk_week2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ab_pk_week2.Services
{
    public interface IBankAccountService
    {
        BankAccount GetAccountById(int accountId);
        
    }
    public class BankAccountService1: IBankAccountService
    {
        private readonly BankDbContext _context;

        public BankAccountService1(BankDbContext context)
        {
            _context = context;
        }

        public BankAccount GetAccountById(int accountId)
        {
            try
            {
                var account = _context.BankAccounts.SingleOrDefault(x => x.accountId == accountId);

                if (account == null)
                {
                    throw new KeyNotFoundException("Account not found"); 
                }
                account.accountHolder += " Service1'i kullanmıştır. İyi  günler";
                return account; 
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Account not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

    }

    public class BankAccountService2 : IBankAccountService
    {
        private readonly BankDbContext _context;

        public BankAccountService2(BankDbContext context)
        {
            _context = context;
        }
        public BankAccount GetAccountById(int accountId)
        {
            try
            {
                var account = _context.BankAccounts.SingleOrDefault(x => x.accountId == accountId);

                if (account == null)
                {
                    throw new KeyNotFoundException("Account not found");
                }
                account.accountHolder += " Service2'i kullanmıştır. İyi  tatiller"; 
                return account;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Account not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

    }

    // Bağımlılığı enjekte edeceğimiz sınıf
    public class Service
    {
        private readonly IBankAccountService _service;

        // Bağımlılığı constructor üzerinden enjekte ediyoruz
        public Service(IBankAccountService service)
        {
            _service = service;
        }

        public BankAccount GetAccountById(int id)
        {
            return _service.GetAccountById(id);
        }
    }
}
