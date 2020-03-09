using System;
using System.Web;
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

        public bool login(string username, string password)
        {

            string retrievedPassword = "";

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlInsert = "SELECT idUser, password FROM user where username = @mcUsername";
                cmd.CommandText = sqlInsert;
                cmd.Parameters.AddWithValue("@mcUsername", username);
                cmd.Parameters.AddWithValue("@mcPassword", password);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        retrievedPassword = reader["password"].ToString();
                    }
                }

            }

            if(retrievedPassword.Equals(password)){
                
                return true;
            }else {
                return false;
            }
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
        public List<TodoTask> getAllTasks()
        {
            List<TodoTask> taskList = new List<TodoTask>();

            //using dispatches the object when it is no longer in scope.
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from task", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        taskList.Add(new TodoTask()
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
        public List<TodoList> getAllListsByUsername(string username)
        {

            int userId = getUserIdByUsername(username);
            List<TodoList> todoLists = new List<TodoList>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT * FROM list WHERE User_idUser = @mcUserId";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcUserId", userId);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        todoLists.Add(new TodoList()
                        {
                            listId = Convert.ToInt32(reader["idList"]),
                            week = Convert.ToInt32(reader["week"]),
                            userId = Convert.ToInt32(reader["User_idUser"])

                        });
                    }
                }
            }

            return todoLists;
        }
        public int getUserIdByUsername(string username)
        {

            int userId = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT idUser FROM user WHERE username = @mcUsername";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcUsername", username);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["idUser"]);
                    }
                }
            }

            return userId;
        }
        public List<TodoTask> getAllTodoTaskByListId(int id)
        {
            List<TodoTask> todoLists = new List<TodoTask>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT * FROM task WHERE list_idList = @mcListId";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcListId", id);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        todoLists.Add(new TodoTask()
                        {
                            listId = Convert.ToInt32(reader["list_idList"]),
                            status = Convert.ToInt32(reader["status"]),
                            description = reader["description"].ToString(),
                            title = reader["title"].ToString(),
                            taskId = Convert.ToInt32(reader["idTask"]),
                            weekDay = reader["weekDay"].ToString()

                        });
                    }
                }
            }

            return todoLists;
        }
    }
}