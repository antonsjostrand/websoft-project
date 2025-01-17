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
    [Route("task")]
    public class TaskController : Controller 
    {

        public DatabaseService DatabaseService {get;}

        public TaskController(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        [Route("{id:int}")]
        [HttpGet]
        public List<TodoTask> getAllTaskById(int id)
        {
            List<TodoTask> list = DatabaseService.getAllTodoTaskByListId(id);
            return list;
        }

        [HttpPost]
        public IActionResult createNewTask([FromForm] CreateTaskForm createTaskForm)
        {
            DatabaseService.createTask(createTaskForm.title, createTaskForm.description, createTaskForm.week, createTaskForm.weekDay, HttpContext.Session.GetString("username"));

            return RedirectToPage("/todo");
        }

        [Route("edit")]
        [HttpPost]
        public IActionResult editTask([FromForm] EditTaskForm editTaskForm)
        {
            DatabaseService.editTask(editTaskForm.id, editTaskForm.title, editTaskForm.description, editTaskForm.week, editTaskForm.weekDay, HttpContext.Session.GetString("username"));

            return RedirectToPage("/todo");
        }

        [Route("delete")]
        [HttpPost]
        public IActionResult deleteTask([FromForm] DeleteTaskForm deleteTaskForm)
        {
            DatabaseService.deleteTask(deleteTaskForm.id, deleteTaskForm.title);

            return RedirectToPage("/todo");
        }

    }


}