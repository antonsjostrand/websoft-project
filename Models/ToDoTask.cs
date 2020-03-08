using System;


namespace websoftProject.Models {
    public class ToDoTask
    {
        public int taskId { get; set;}

        public string title {get; set;}

        public string description {get; set;}

        public int status {get; set;}

        public int listId {get; set;}
    }
}