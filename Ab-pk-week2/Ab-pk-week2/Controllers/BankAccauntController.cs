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
    public class BankAccauntController : ControllerBase
    {
        private readonly BankDbContext dbcontext;

        public BankAccauntController(BankDbContext bankDbContext)
        {
            this.dbcontext = bankDbContext;

        }


        // GET: get BankAccaunts
        [HttpGet]
        public ActionResult<List<BankAccount>> GetBankAccounts()
        {
            try
            {
                var _list = dbcontext.BankAccounts.OrderBy(x => x.accountId).ToList();
                if (_list == null)
                {
                    return NotFound(); // 404 nofound error
                }
                return Ok(_list); // 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500
            }
        }

        // GET: get BankAccaunts
        [HttpGet("/AccountsByHolder/")]
        public ActionResult<List<BankAccount>> GetBankAccountsByHolder([FromQuery] string holder)
        {
            try
            {
                var _list = dbcontext.BankAccounts.Where(x=>x.accountHolder== holder).OrderBy(x => x.accountId).ToList();
                if (_list == null)
                {
                    return NotFound(); // 404 nofound error
                }
                return Ok(_list); // 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500
            }
        }

        // GET: get BankAccaunt from id
        [HttpGet("{id}")]
        public ActionResult<BankAccount> GetBankAccountById([FromRoute]int id)
        {
            try
            {
                var account = dbcontext.BankAccounts.Where(x => x.accountId == id).SingleOrDefault();
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
        [HttpGet("/GetBankAccountByIdFromService1/{id}")]
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
        [HttpGet("/GetBankAccountByIdFromService2/{id}")]
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
                return Ok(account.GetFormattedBalance()); //200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Post: create a BankAccaunt
        [HttpPost]
        public IActionResult AddBankAccount([FromBody]BankAccount newAccount)
        {
            try
            {
                if(newAccount == null)
                {
                    return BadRequest();
                }

                dbcontext.Add(newAccount);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: update a BankAccaunt
        [HttpPut("{id}")]
        public IActionResult UpdateBankAccount(int id, [FromBody]BankAccount updateAccount)
        {
            try
            {
                if (id != updateAccount.accountId)
                {
                    return BadRequest();
                }

                var account = dbcontext.BankAccounts.Where(x => x.accountId == id).SingleOrDefault();

                if (account == null)
                {
                    return NotFound(); // 404 nofound error
                }

                dbcontext.Update(account);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: delete a BankAccaunt
        [HttpDelete("{id}")]
        public IActionResult DeleteBankAccount(int id)
        {
            try
            {
                var account = dbcontext.BankAccounts.Where(x => x.accountId == id).SingleOrDefault();

                if (account == null)
                    return NotFound();

                dbcontext.BankAccounts.Remove(account);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // PATCH: patch a BankAccaunt
        [HttpPatch("{id}")]
        public IActionResult PatchBankAccount(int id, [FromBody] JsonPatchDocument<BankAccount> updateAccount)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var account = dbcontext.BankAccounts.Where(x => x.accountId == id).SingleOrDefault();

                if (account == null) 
                { 
                    return NotFound();
                }

                dbcontext.Update(account);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


            
        }

    }
}
