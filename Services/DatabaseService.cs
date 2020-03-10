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

                for(int i = 0; i < 52; i++)
                {
                    int id = getAvailableListId();

                    MySqlCommand cmdWeek = new MySqlCommand();
                    string insertWeek = "INSERT INTO list (idList, week, User_idUser) VALUES (@mcListId, @mcWeek, @mcUserId)";
                    cmdWeek.CommandText = insertWeek;
                    cmdWeek.Parameters.AddWithValue("@mcUserId", availableId);
                    cmdWeek.Parameters.AddWithValue("@mcListId", id);
                    cmdWeek.Parameters.AddWithValue("@mcWeek", (i+1));
                    cmdWeek.Connection = conn;
                    cmdWeek.ExecuteNonQuery();
                }

            }
        }

        public void createTask(string title, string description, int listId, string weekDay)
        {
            int id = getAvailableTaskId();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlInsert = "INSERT INTO task (idTask, title, description, weekDay, list_idList, status) VALUES (@mcIdTask, @mcTitle, @mcDescription, @mcWeekday, @mcListId, @mcStatus)";
                cmd.CommandText = sqlInsert;
                cmd.Parameters.AddWithValue("@mcIdTask", id);
                cmd.Parameters.AddWithValue("@mcTitle", title);
                cmd.Parameters.AddWithValue("@mcDescription", description);
                cmd.Parameters.AddWithValue("@mcWeekDay", weekDay);
                cmd.Parameters.AddWithValue("@mcListId", listId);
                cmd.Parameters.AddWithValue("@mcStatus", 1);
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
                        if(reader["max(idUser)"] != DBNull.Value){
                            availableId = Convert.ToInt32(reader["max(idUser)"]);
                        }else {
                            availableId = 0;
                        }
                    }
                }
            }

            return availableId + 1;

        }
        public int getAvailableTaskId()
        {
            int availableId = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select max(idTask) from task", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        if(reader["max(idTask)"] != DBNull.Value){
                        availableId = Convert.ToInt32(reader["max(idTask)"]);
                        }else{
                            availableId = 0;
                        }
                    }
                }
            }

            return availableId + 1;
        }      
        public int getAvailableListId()
        {
            int availableId = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select max(idList) from list", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        if(reader["max(idList)"] != DBNull.Value){
                            availableId = Convert.ToInt32(reader["max(idList)"]);
                        }else{
                            availableId = 0;
                        }
                    }
                }
            }

            return availableId + 1;
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
  
        public List<TodoTask> getTodoTaskById(int id)
        {
            List<TodoTask> todoLists = new List<TodoTask>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT * FROM task WHERE idTask = @mcTaskId";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcTaskId", id);
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
        
        public void editTask(int id, string title, string description, int listId, string weekDay)
        {

            Console.WriteLine("Updating parameters: " );
            Console.WriteLine("ID: " + id + " , title: " + title + ", description: " + description + ", listId: " + listId + ", weekday: " + weekDay);

            using (MySqlConnection conn = GetConnection())
            {
                Console.WriteLine("Edit");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlUpdate = "UPDATE task SET title = @mcTitle, description = @mcDescription, list_idList = @mcListId, weekDay = @mcWeekDay WHERE idTask = @mcIdTask";
                cmd.CommandText = sqlUpdate;
                cmd.Parameters.AddWithValue("@mcIdTask", id);
                cmd.Parameters.AddWithValue("@mcTitle", title);
                cmd.Parameters.AddWithValue("@mcDescription", description);
                cmd.Parameters.AddWithValue("@mcWeekDay", weekDay);
                cmd.Parameters.AddWithValue("@mcListId", listId);
                cmd.Connection = conn;
                int row = cmd.ExecuteNonQuery();
                Console.WriteLine("Edit done");
                Console.WriteLine(row);


            }

        }

        public void deleteTask(int id, string title)
        {
            using (MySqlConnection conn = GetConnection())
            {
                Console.WriteLine("Delete");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlUpdate = "DELETE FROM task WHERE idTask = @mcIdTask";
                cmd.CommandText = sqlUpdate;
                cmd.Parameters.AddWithValue("@mcIdTask", id);
                cmd.Connection = conn;
                int row = cmd.ExecuteNonQuery();
                Console.WriteLine("Delete done");
                Console.WriteLine(row);

            }

        }
    }
}