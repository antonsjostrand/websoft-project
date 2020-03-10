using System;

namespace websoftProject.Models
{

    public class EditUserForm
    {
        public int id {get;set;}
        public string email {get; set;}
        public string username {get; set;}
        public string password {get; set;}
        public int privilege {get;set;}
        
    }

}