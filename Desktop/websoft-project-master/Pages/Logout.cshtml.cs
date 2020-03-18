using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using websoftProject.Services;
using websoftProject.Models;

namespace websoftProject.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;

        public DatabaseService DatabaseService;
        
        public LogoutModel(ILogger<LogoutModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {

            HttpContext.Session.Clear();
        }

    }
}