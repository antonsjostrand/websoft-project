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

        public bool isAdmin(string username)
        {

            int retrievedPrivilege = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlSelect = "SELECT privilege FROM user WHERE username = @mcUsername";
                cmd.CommandText = sqlSelect;
                cmd.Parameters.AddWithValue("@mcUsername", username);
                cmd.Connection = conn;
                
                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        if(reader["privilege"] != DBNull.Value){
                            retrievedPrivilege = Convert.ToInt32(reader["privilege"]);
                        }
                    }
                }

            }

            if(retrievedPrivilege == 1){
                Console.WriteLine("Is admin");
                return true;
            }else {
                Console.WriteLine("Is not admin");
                return false;
            }
        }
        public bool createUser(string username, string email, string password)
        {
            int availableId = getAvailableUserId();

            if(!userExists(username)){
                return false;
            }

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

            return true;
        }

        public bool userExists(string username)
        {

            string retrievedUsername = "";

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlSelect = "SELECT username FROM user where username = @mcUsername";
                cmd.CommandText = sqlSelect;
                cmd.Parameters.AddWithValue("@mcUsername", username);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        if(reader["username"] != DBNull.Value){
                            retrievedUsername = reader["username"].ToString();
                        }
                    }
                }

            }

            if(retrievedUsername.Equals("")){
                return true;
            }else {
                return false;
            }
        }
        public void createTask(string title, string description, int week, string weekDay, string username)
        {
            int id = getAvailableTaskId();

            int listId = getListIdForUserAndWeek(week, username);

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlInsert = "INSERT INTO task (idTask, title, description, weekDay, list_idList) VALUES (@mcIdTask, @mcTitle, @mcDescription, @mcWeekDay, @mcListId)";
                cmd.CommandText = sqlInsert;
                cmd.Parameters.AddWithValue("@mcIdTask", id);
                cmd.Parameters.AddWithValue("@mcTitle", title);
                cmd.Parameters.AddWithValue("@mcDescription", description);
                cmd.Parameters.AddWithValue("@mcWeekDay", weekDay);
                cmd.Parameters.AddWithValue("@mcListId", listId);
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

        public User getUser(int id)
        {
            User user = new User();
            
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlSelect = "SELECT * FROM user WHERE idUser = @mcUserId";
                cmd.CommandText = sqlSelect;
                cmd.Parameters.AddWithValue("@mcUserId", id);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        
                        user.userId = Convert.ToInt32(reader["idUser"]);
                        user.email = reader["email"].ToString();
                        user.username = reader["username"].ToString();
                        user.privilege = Convert.ToInt32(reader["privilege"]);
                    }
                }
            }

        return user;

        }
        public List<User> getAllUsers()
        {

            List<User> userList = new List<User>();

            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", conn);

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        userList.Add(new User()
                        {
                            userId = Convert.ToInt32(reader["idUser"]),
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            privilege = Convert.ToInt32(reader["privilege"])
                        });
                    }
                }
            }

            return userList;
        }

        public void deleteUser(int id)
        {
             using (MySqlConnection conn = GetConnection())
            {
                Console.WriteLine("Delete user: " + id);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlUpdate = "DELETE FROM user WHERE idUser = @mcIdUser";
                cmd.CommandText = sqlUpdate;
                cmd.Parameters.AddWithValue("@mcIdUser", id);
                cmd.Connection = conn;
                int row = cmd.ExecuteNonQuery();
                Console.WriteLine("Delete done");

            }
        }

        public List<int> getAllListIdForUser(int id)
        {
            List<int> idList = new List<int>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT idList FROM list WHERE User_idUser = @mcUserId";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcUserId", id);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        idList.Add(Convert.ToInt32(reader["idList"]));
                    }
                }
            }

            return idList;
        }

        public List<int> getAllTaskIdByListId(int id)
        {
            List<int> taskIdList = new List<int>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string select = "SELECT idTask FROM task WHERE list_idList = @mcListId";
                cmd.CommandText = select;
                cmd.Parameters.AddWithValue("@mcListId", id);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        taskIdList.Add(Convert.ToInt32(reader["idTask"]));
                    }
                }
            }

            return taskIdList;
        }
        public void editUser(int id, string email, string username, string password, int privilege)
        {
            if(password == null){

                Console.WriteLine("Updating with no password");
                Console.WriteLine("Parameters:");
                Console.WriteLine("userID: " + id + ", username: " + username + ", email: " + email + ", privilege: " + privilege + ", password: " + password);

                using(MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    string sqlUpdate = "UPDATE user SET email = @mcEmail, username = @mcUsername, privilege = @mcPrivilege WHERE idUser = @mcUserId";
                    cmd.CommandText = sqlUpdate;
                    cmd.Parameters.AddWithValue("@mcEmail", email);
                    cmd.Parameters.AddWithValue("@mcUsername", username);
                    cmd.Parameters.AddWithValue("@mcPrivilege", privilege);
                    cmd.Parameters.AddWithValue("@mcUserId", id);
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

            }else{

                Console.WriteLine("Updating user with password");
                Console.WriteLine("Parameters:");
                Console.WriteLine("userID: " + id + ", username: " + username + ", email: " + email + ", privilege: " + privilege + ", password: " + password);

                using(MySqlConnection conn = GetConnection())
                {

                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    string sqlUpdate = "UPDATE user SET email = @mcEmail, username = @mcUsername, privilege = @mcPrivilege, password = @mcPassword WHERE idUser = @mcUserId";
                    cmd.CommandText = sqlUpdate;
                    cmd.Parameters.AddWithValue("@mcEmail", email);
                    cmd.Parameters.AddWithValue("@mcUsername", username);
                    cmd.Parameters.AddWithValue("@mcPrivilege", privilege);
                    cmd.Parameters.AddWithValue("@mcPassword", password);
                    cmd.Parameters.AddWithValue("@mcUserId", id);
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

            }
        

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
            Console.WriteLine("Getting todotask by task ID: " + id);

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
        
        public void editTask(int id, string title, string description, int week, string weekDay, string username)
        {

            int listId = getListIdForUserAndWeek(week, username);

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
                Console.WriteLine("Delete task: " + id);
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

        public void deleteList(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                Console.WriteLine("Delete list: " + id);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlUpdate = "DELETE FROM list WHERE idList = @mcIdList";
                cmd.CommandText = sqlUpdate;
                cmd.Parameters.AddWithValue("@mcIdList", id);
                cmd.Connection = conn;
                int row = cmd.ExecuteNonQuery();
                Console.WriteLine("Delete done");

            }
        }

        //Get listid from week and userid
        public int getListIdForUserAndWeek(int week, string username)
        {
            int userId = getUserIdByUsername(username);
            int listId = 0;

            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlSelect = "SELECT idList FROM list WHERE week = @mcWeek AND User_idUser = @mcUserId";
                cmd.CommandText = sqlSelect;
                cmd.Parameters.AddWithValue("@mcWeek", week);
                cmd.Parameters.AddWithValue("mcUserId", userId);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {

                        listId = Convert.ToInt32(reader["idList"]);

                    }
                }
                
            }

            return listId;
        }
    
        public int getWeekByListId(int listId)
        {

            int week = 0;
            
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                string sqlSelect = "SELECT week FROM list WHERE idList = @mcListId";
                cmd.CommandText = sqlSelect;
                cmd.Parameters.AddWithValue("@mcListId", listId);
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        week = Convert.ToInt32(reader["week"]);
                    }
                
                }

            }

            return week;

        }
    
    }
}