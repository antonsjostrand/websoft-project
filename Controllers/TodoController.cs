using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using websoftProject.Models;
using websoftProject.Services;
using websoftProject.Pages;

namespace websoftProject.Controllers
{

    [ApiController]
    [Route("todo")]
    public class TodoController : Controller 
    {

        public DatabaseService DatabaseService {get;}

        public TodoController(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        [Route("list/{id:int}")]
        [HttpGet]
        public List<TodoTask> getAllTodoTaskByListId(int id)
        {
            List<TodoTask> list = DatabaseService.getAllTodoTaskByListId(id);
            return list;
        }

    }


}