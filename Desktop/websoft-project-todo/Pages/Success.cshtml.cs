using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using websoftProject.Services;

namespace websoftProject.Pages
{

    public class SuccessModel : PageModel
    {
        private readonly ILogger<SuccessModel> _logger;

        public DatabaseService DatabaseService;

        public string Type {get; set;}

        public SuccessModel(ILogger<SuccessModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public int test {get; set;}

        public void OnGet()
        {
            test = DatabaseService.getAvailableUserId();

            Type = Request.Query["type"];
            
        }
       
    }

}