using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using websoftProject.Models;

namespace websoftProject.Services 
{

    public class DatabaseService
    {

        public string connectionString {get; set;}

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MySqlConnection GetConnection(){
            return new MySqlConnection(connectionString);
        }

        public List<ToDoTask> getAllTasks()
        {
            List<ToDoTask> taskList = new List<ToDoTask>();

            //using dispatches the object when it is no longer in scope.
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from task", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        taskList.Add(new ToDoTask()
                        {
                            taskId = Convert.ToInt32(reader["idTask"]),
                            title = reader["title"].ToString(),
                            description = reader["description"].ToString(),
                            status = Convert.ToInt32(reader["status"]),
                            listId = Convert.ToInt32(reader["list_idList"])
                        });
                    }
                }

            }

            return taskList;

        }


    }


}