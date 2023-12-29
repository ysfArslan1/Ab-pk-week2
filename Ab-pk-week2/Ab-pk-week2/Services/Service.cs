using Ab_pk_week2.Models;

namespace Ab_pk_week2.Services
{
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

