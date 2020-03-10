using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using websoftProject.Services;
using websoftProject.Models;

namespace websoftProject.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;

        public DatabaseService DatabaseService;

        public string sessionUser {get; set;}

        public string sessionAdmin {get;set;}

        public AdminModel(ILogger<AdminModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {
            sessionUser = HttpContext.Session.GetString("username");
            sessionAdmin = HttpContext.Session.GetString("admin");
        }
    }
}
