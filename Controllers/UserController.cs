using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using websoftProject.Models;
using websoftProject.Services;

namespace websoftProject.Controllers 
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {

        public DatabaseService DatabaseService {get;}

        public UserController(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromForm] LoginForm loginForm)
        {
            if(loginForm.username == null || loginForm.password == null){
                return Redirect("http://localhost:5000/Information?type=error&content=no-values");
            }

            bool success = DatabaseService.login(loginForm.username, loginForm.password);

            if(success){
                HttpContext.Session.SetString("username", loginForm.username);
                if(DatabaseService.isAdmin(loginForm.username)){
                    HttpContext.Session.SetString("admin", "true");
                }
                
                return RedirectToPage("/Todo");
            }else {
                return Redirect("http://localhost:5000/Information?type=error&content=no-user");
            }

            
        }

        [HttpPost]
        public IActionResult signUp([FromForm] SignUpForm signUpForm)
        {

            if(signUpForm.username == null || signUpForm.email == null || signUpForm.password == null){
                return Redirect("http://localhost:5000/Information?type=error&content=no-values");
            }

            bool success = DatabaseService.createUser(signUpForm.username, signUpForm.email, signUpForm.password);
            
            if(success){
                return Redirect("http://localhost:5000/Success?type=user");
            }else {
                return Redirect("http://localhost:5000/Information?type=error&content=user-exists");
            }

            

        }

        [HttpGet]
        [Route("all")]
        public List<User> getAllUsers()
        {
            return DatabaseService.getAllUsers();
        }

        [HttpGet]
        [Route("{id:int}")]
        public User GetUser(int id){

            return DatabaseService.getUser(id);

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult editUser([FromForm] EditUserForm editUserForm)
        {
            DatabaseService.editUser(editUserForm.id, editUserForm.email, editUserForm.username, editUserForm.password, editUserForm.privilege);

            return Redirect("/admin");

        }
        
        [HttpDelete]
        [Route("delete/{id:int}")]
        public IActionResult deleteUser(int id){
            
            List<int> listIds = DatabaseService.getAllListIdForUser(id);
            
            foreach(int listId in listIds)
            {
                List<int> taskIds = DatabaseService.getAllTaskIdByListId(listId);

                foreach(int taskId in taskIds)
                {
                    DatabaseService.deleteTask(taskId, "");
                }

                DatabaseService.deleteList(listId);
            }

            DatabaseService.deleteUser(id);

            return Ok();
        }

    }

}