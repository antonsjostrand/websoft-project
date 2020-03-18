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
    public class TodoModel : PageModel
    {
        private readonly ILogger<TodoModel> _logger;

        public DatabaseService DatabaseService;

        public List<TodoList> todoLists{get; private set;}

        public List<TodoTask> todoTaskList {get; set;}

        public string sessionUser {get; set;}

        public string sessionAdmin {get; set;}

        public TodoModel(ILogger<TodoModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {
            sessionUser = HttpContext.Session.GetString("username");
            sessionAdmin = HttpContext.Session.GetString("admin");
            todoLists = DatabaseService.getAllListsByUsername(sessionUser);
        }

    }
}
