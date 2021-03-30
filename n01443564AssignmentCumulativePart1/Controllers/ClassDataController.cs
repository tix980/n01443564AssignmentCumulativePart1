using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01443564AssignmentCumulativePart1.Models;
using MySql.Data.MySqlClient;

namespace n01443564AssignmentCumulativePart1.Controllers
{
    public class ClassDataController : ApiController
    {
        //Instantiating the schooldb database context class to allow us
        //to access the schooldb database.
        private SchoolDbContext School = new SchoolDbContext();

        ///Objective:Create a method that allows us to access the teachers table of the datbase
        /// <summary>
        /// This method will return a list of class objects
        /// </summary>
        /// <example>
        /// GET api/ClassData/ListClasses
        /// </example>
        /// <returns>A list of class objects</returns>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public List<Class> ListClasses()
        {
            //Create a empty list for class objects
            List<Class> Classes = new List<Class>{ };
            //Build an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            // Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            // Write a sql query
            cmd.CommandText = "SELECT * FROM `classes` JOIN teachers ON classes.teacherid = teachers.teacherid";

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //Create a class object to hold properties by accessing each row of the result set
                Class NewClass = new Class();
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewClass.TeacherFname = ResultSet["teacherfname"].ToString();
                NewClass.TeacherLname = ResultSet["teacherlname"].ToString();
                NewClass.StartDate = ResultSet["startdate"].ToString();
                NewClass.FinishDate = ResultSet["finishdate"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();

                //Add the class object to the empty list
                Classes.Add(NewClass);
            }

            //close the connection
            conn.Close();

            //Return the list of classes
            return Classes;
        }

        ///Objective:Create a method that allows us to access a specific class's information by entering an interger value of the classid
        /// <summary>
        /// This method will return a specific class's information  based on the input interger value of theteacherid
        /// </summary>
        /// <example>
        /// GET api/ClasstData/FindClass/{id}
        /// </example>
        /// <returns>A class object</returns>
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            //Create a class object
            Class NewClass = new Class();

            //Build a instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            //Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            //Write a sql query
            cmd.CommandText = "SELECT* FROM `classes` JOIN teachers ON classes.teacherid = teachers.teacherid WHERE classid = " + id;

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //The class object hold properties by accessing each row of the result set
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewClass.TeacherFname = ResultSet["teacherfname"].ToString();
                NewClass.TeacherLname = ResultSet["teacherlname"].ToString();
                NewClass.StartDate = ResultSet["startdate"].ToString();
                NewClass.FinishDate = ResultSet["finishdate"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();
            }

            //close the connection
            conn.Close();

            //Return the class' information
            return NewClass;

        }
    }
}
