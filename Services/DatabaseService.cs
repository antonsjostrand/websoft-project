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

        public void login(string username, string password)
        {

        }

        public void createUser(string username, string email, string password)
        {
            int availableId = getAvailableUserId();

            availableId++;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlInsert = "INSERT INTO user (idUser, email, username, password) VALUES (@mcIdUser, @mcEmail, @mcUsername, @mcPassword)";
                cmd.CommandText = sqlInsert;
                cmd.Parameters.AddWithValue("@mcIdUser", availableId);
                cmd.Parameters.AddWithValue("@mcUsername", username);
                cmd.Parameters.AddWithValue("@mcEmail", email);
                cmd.Parameters.AddWithValue("@mcPassword", password);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

            }
        }

        public int getAvailableUserId()
        {
            int availableId = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select max(idUser) from user", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        availableId = Convert.ToInt32(reader["max(idUser)"]);
                    }
                }
            }

            return availableId;

        }

        public void getAvailableTaskId()
        {

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