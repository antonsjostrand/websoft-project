using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using websoftProject.Services;

namespace websoftProject.Pages
{

    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public DatabaseService DatabaseService;

        public LoginModel(ILogger<LoginModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public int test {get; set;}

        public void OnGet()
        {
            test = DatabaseService.getAvailableUserId();
        }
       
    }

}