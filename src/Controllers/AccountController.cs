using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase {
        private readonly IAccountServices _accountService;
        public AccountController(IAccountServices accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("/Accounts")]
        public async Task<ActionResult<ServiceResponse<List<GetAccountDto>>>> getAccounts() {
            var response = await _accountService.getAccounts();

            if(!response.Success) {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> getAccount(string id) {
            var response = await _accountService.getAccount(id);

            if(!response.Success) {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpPost("add")] 
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> addStudent(AddAccountDto newAccount) {
            var response = await _accountService.addAccount(newAccount);

            if(!response.Success) {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("set")]
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> setAccount(UpdateAccountDto updatedAccount) {
            var response = await _accountService.updateAccount(updatedAccount);

            if(!response.Success){
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> deleteAccount(string id) {
            var response = await _accountService.deleteAccount(id);

            if(!response.Success){
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}