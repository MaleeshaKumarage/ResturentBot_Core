using MySql.Data.MySqlClient;
using System;

namespace DBConnector
{
    public class DBConnection
    {
        public static string connectionString = "Server=148.72.232.179;Database=rest-o-bot_chat_log;Uid=maleesha;Pwd=mal33sha";
        private MySqlConnection connection;
        public DBConnection()
        {
            Initialize();
        }


        //Initialize values
        private void Initialize()
        {

            connection = new MySqlConnection(connectionString);
        }
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(ChatHistory chatHistory)
        {
            var x = chatHistory;
            string query = "INSERT INTO Chat (Id,UserName,Utterence,Intent) VALUES(@Id,@UserName,@Utterence,@Intent)";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {

                    cmd.Parameters.Add(new MySqlParameter("Id", chatHistory.id));
                    cmd.Parameters.Add(new MySqlParameter("UserName", chatHistory.UserName));
                    cmd.Parameters.Add(new MySqlParameter("Utterence", chatHistory.Utterence));
                    cmd.Parameters.Add(new MySqlParameter("Intent", chatHistory.Intent));


                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                //close connection
                this.CloseConnection();
            }
        }
        //public void Update()
        //{
        //    string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

        //    //Open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //create mysql command
        //        MySqlCommand cmd = new MySqlCommand();
        //        //Assign the query using CommandText
        //        cmd.CommandText = query;
        //        //Assign the connection using Connection
        //        cmd.Connection = connection;

        //        //Execute query
        //        cmd.ExecuteNonQuery();

        //        //close connection
        //        this.CloseConnection();
        //    }
        //}
        ////Delete statement
        //public void Delete()
        //{
        //    string query = "DELETE FROM Student WHERE RegistrationNo='@regNo'";

        //    if (this.OpenConnection() == true)
        //    {
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        cmd.ExecuteNonQuery();
        //        this.CloseConnection();
        //    }
        //}
        //Select statement
        //public List<Student> Select()
        //{
        //    string query = "SELECT * FROM Student";

        //    //Create a list to store the result
        //    List<Student> studentList = new List<Student>();



        //    //Open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //Create Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        //Create a data reader and Execute the command
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            studentList.Add(new Student(Guid.Parse(dataReader["Id"].ToString()), Int32.Parse(dataReader["RegistrationNo"].ToString()), dataReader["FirstName"].ToString(), dataReader["MiddleName"].ToString(), dataReader["LastName"].ToString(), dataReader["Address_Line1"].ToString(), dataReader["Address_Line2"].ToString(), dataReader["Address_Line3"].ToString(), dataReader["Guardian_Name"].ToString(), Int64.Parse(dataReader["LandPhoneNo"].ToString()), Int64.Parse(dataReader["MobileNo"].ToString()), dataReader["DOB"].ToString(), dataReader["Gender"].ToString()));

        //        }

        //        //close Data Reader
        //        dataReader.Close();

        //        //close Connection
        //        this.CloseConnection();

        //        //return list to be displayed
        //        return studentList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
