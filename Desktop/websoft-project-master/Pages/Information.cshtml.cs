using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using websoftProject.Services;
using websoftProject.Models;

namespace websoftProject.Pages
{
    public class InformationModel : PageModel
    {
        private readonly ILogger<InformationModel> _logger;

        public DatabaseService DatabaseService;

        public string Message {get; set;}

        public string Type {get; set;}
        public string RequestContent {get; set;}

        public InformationModel(ILogger<InformationModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DatabaseService = databaseService;
        }

        public void OnGet()
        {
            Type = Request.Query["type"];
            RequestContent = Request.Query["content"];

            if(Type.Equals("error")){
                if(RequestContent.Equals("no-user")){
                    Message = "The entered credentials were not present in the database.";
                }else if (RequestContent.Equals("user-exists")){
                     Message = "User not created, perhaps the username was already taken.";
                }else{
                    Message = "Make sure that you entered values in every textfield";
                }
            }
        }
    }
}
