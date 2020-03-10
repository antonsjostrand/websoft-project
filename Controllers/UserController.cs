using System;
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

    }

}