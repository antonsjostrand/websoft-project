using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using websoftProject.Services;

namespace websoftProject.Pages
{

    public class SignupModel : PageModel
    {
        private readonly ILogger<SignupModel> _logger;

        public DatabaseService DatabaseService;

        public SignupModel(ILogger<SignupModel> logger, DatabaseService databaseService)
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