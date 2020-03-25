using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using webapp.Models;
using webapp.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace webapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        public AccountsController(JsonFileAccountService accountService)
        {
            AccountService = accountService;
        }

        public JsonFileAccountService AccountService { get; }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return AccountService.GetAccounts();
        }

        [HttpGet("/account/{id}")]
        public string GetAccount(int id){
            var accounts = AccountService.GetAccounts().ToList();

            foreach(var acc in accounts){
                if(id == acc.Number){
                    var accountList = new List<Account>();
                    accountList.Add(acc);
                    var json = JsonSerializer.Serialize<IEnumerable<Account>>(accountList);
                    return json;
                }
            }
            return "Error: " + id + " does not exist";
        }
    }
}
