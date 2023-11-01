using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/bank")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountId:int}", Name = "GetAccount")]
        public async Task<ActionResult<Account>> GetAccount(int accountId)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] CreateAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            var account = await _accountService.CreateAccountAsync(request.Name, request.Balance);

            return CreatedAtAction("GetAccount", new { accountId = account.Id }, account);
        }

        [HttpPut("{accountId}")]
        public async Task<ActionResult<Account>> UpdateAccount(int accountId, [FromBody] UpdateAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            var account = await _accountService.UpdateAccountAsync(accountId, request.Name, request.Balance);
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }

        [HttpDelete("{accountId}")]
        public async Task<ActionResult> DeleteAccount(int accountId)
        {
            await _accountService.DeleteAccountAsync(accountId);
            return NoContent();
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<ActionResult<Account>> Deposit(int accountId, [FromBody] DepositRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            var account = await _accountService.DepositAsync(accountId, request.Amount);
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<ActionResult<Account>> Withdraw(int accountId, [FromBody] WithdrawRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            var account = await _accountService.WithdrawAsync(accountId, request.Amount);
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }
    }
}
