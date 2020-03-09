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
    public class TaskModel : PageModel
    {
        private readonly ILogger<TaskModel> _logger;

        public DatabaseService DatabaseService;

        public List<TodoList> todoLists{get; private set;}

        public List<TodoTask> todoTaskList {get; set;}

        public string sessionUser {get; set;}

        public TaskModel(ILogger<TaskModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {
            sessionUser = HttpContext.Session.GetString("username");
            todoLists = DatabaseService.getAllListsByUsername(sessionUser);
        }

    }
}