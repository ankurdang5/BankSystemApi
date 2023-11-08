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

        /// <summary>
        /// Get all the accounts
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get the account details based on accountid
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("GetAccount/{accountId}")]
        public async Task<ActionResult<Account>> Account(string accountId)
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

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Account>> Account([FromBody] CreateAccountRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid request data.");
                }
                var account = await _accountService.CreateAccountAsync(request.Userid, request.UserName, request.PanCard, request.Balance);

                return CreatedAtAction("Account", new { accountId = account.Id }, account);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "An error occurred while creating the account.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete the account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpDelete("{accountId}")]
        public async Task<ActionResult> Delete(string accountId)
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

        /// <summary>
        /// Deposit the amount in the bank
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{accountId}/deposit")]
        public async Task<ActionResult<Account>> Deposit(string accountId, [FromBody] DepositRequest request)
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

        /// <summary>
        /// Withdraw the amount from the bank account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{accountId}/withdraw")]
        public async Task<ActionResult<Account>> Withdraw(string accountId, [FromBody] WithdrawRequest request)
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
