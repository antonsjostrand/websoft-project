using System;


namespace websoftProject.Models {
    public class TodoTask
    {
        public int taskId { get; set;}

        public string title {get; set;}

        public string description {get; set;}

        public int listId {get; set;}

        public string weekDay {get; set;}
    }
}