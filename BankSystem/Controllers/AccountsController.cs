using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountsController> _logger;


        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Account()
        {
            try
            {
                var accounts = await _accountService.GetAllAccountsAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while getting accounts.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAccount/{accountId:int}")]
        public async Task<ActionResult<Account>> Account(int accountId)
        {
            try
            {
                var account = await _accountService.GetAccountAsync(accountId);
                if (account == null)
                {
                    return NotFound();
                }

                return account;
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while getting the account.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Account([FromBody] CreateAccountRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid request data.");
                }
                var account = await _accountService.CreateAccountAsync(request.Name, request.Balance);

                return CreatedAtAction("GetAccount", new { accountId = account.Id }, account);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while creating the account.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{accountId}")]
        public async Task<ActionResult<Account>> Account(int accountId, [FromBody] UpdateAccountRequest request)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while updating the account.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{accountId}")]
        public async Task<ActionResult> Delete(int accountId)
        {
            try
            {
                await _accountService.DeleteAccountAsync(accountId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while deleting the account.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<ActionResult<Account>> Deposit(int accountId, [FromBody] DepositRequest request)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while Depositing in the account.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<ActionResult<Account>> Withdraw(int accountId, [FromBody] WithdrawRequest request)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while Withdrawing from the account.");
                return BadRequest(ex.Message);
            }
        }
    }
}
