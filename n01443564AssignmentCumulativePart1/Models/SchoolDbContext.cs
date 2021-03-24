using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace n01443564AssignmentCumulativePart1.Models
{
    // This is a class represents as a connection to the database
    public class SchoolDbContext
    {   
        // Create properties that are required to access the database server.
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //Create a connection string that describes what server,username,database,port,password, and 
        //convert zero datetime are. 
        protected static string connectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }

        //Objective: Create a method that allows us to access the database server
        ///with the connection string
        /// <summary>
        /// Thie method will returns a connection to the schooldb database.
        /// </summary>
        /// <returns>A Sqlconnect object</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
