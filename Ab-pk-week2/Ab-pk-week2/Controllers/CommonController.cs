using Ab_pk_week2.DBOperations;
using Ab_pk_week2.Models;
using Ab_pk_week2.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Ab_pk_week2.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class CommonController : ControllerBase
    {
        private readonly BankDbContext dbcontext;

        public CommonController(BankDbContext bankDbContext)
        {
            this.dbcontext = bankDbContext;

        }


        

        // GET: get BankAccaunt from id
        [HttpGet("/GetBankAccountByIdFromS1/{id}")]
        public ActionResult<BankAccount> Get_BA_ByIdFromService1([FromRoute] int id)
        {

            try
            {
                BankAccountService1 servis = new BankAccountService1(dbcontext);
                Service _bankAccountService  = new Service(servis);

                var account = _bankAccountService.GetAccountById(id);
                if (account == null)
                {
                    return NotFound();
                }

                return Ok(account); //200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: get BankAccaunt from id
        [HttpGet("/GetBankAccountByIdFromS2/{id}")]
        public ActionResult<BankAccount> Get_BA_ByIdFromService2([FromRoute] int id)
        {

            try
            {
                BankAccountService2 servis = new BankAccountService2(dbcontext);
                Service _bankAccountService = new Service(servis);

                var account = _bankAccountService.GetAccountById(id);
                if (account == null)
                {
                    return NotFound();
                }

                return Ok(account); //200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: get BankAccaunt from id
        [HttpGet("/GetBankAccountBalanceById/{id}")]
        public ActionResult<string> GetBankAccountBalanceById([FromRoute] int id)
        {
            try
            {
                var account = dbcontext.BankAccounts.Where(x => x.accountId == id).SingleOrDefault();
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account.GetFormattedBalance()); //200 // Extention burada kullanılmıştır
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
