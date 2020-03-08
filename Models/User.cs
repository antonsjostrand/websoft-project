using System;


namespace websoftProject.Models {
    public class User
    {
        public int userId { get; private set;}

        public string email {get; set;}

        public string username {get; set;}

        public string password {get; set;}

        public string token {get;set;}

        public int privilege {get; set;}

    }
}