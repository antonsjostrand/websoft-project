using System;
using Microsoft.AspNetCore.Mvc;
using websoftProject.Models;
using websoftProject.Services;

namespace websoftProject.Controllers 
{

    [ApiController]
    [Route("user")]
    public class UserController 
    {

        public DatabaseService DatabaseService {get;}

        public UserController(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        [HttpPut]
        public void login([FromForm] LoginForm loginForm)
        {
            
        }

        [HttpPost]
        public void signUp([FromForm] SignUpForm signUpForm)
        {
            DatabaseService.createUser(signUpForm.username, signUpForm.email, signUpForm.password);
        }

    }

}