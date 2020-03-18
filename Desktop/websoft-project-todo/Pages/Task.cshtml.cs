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

        public List<TodoTask> task {get; set;}

        public int week {get; set;}

        public string sessionUser {get; set;}

        public TaskModel(ILogger<TaskModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {
            sessionUser = HttpContext.Session.GetString("username");
            int id = Convert.ToInt32(Request.Query["edit"]);
            task = DatabaseService.getTodoTaskById(id);
            week = DatabaseService.getWeekByListId(task[0].listId);
        }

    }
}
