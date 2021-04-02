using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01443564AssignmentCumulativePart1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace n01443564AssignmentCumulativePart1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Instantiating the schooldb database context class to allows us
        //to access the schooldb database.
        private SchoolDbContext School = new SchoolDbContext();

        ///Objective:Create a method that allows us to access the teachers table of the datbase
        /// <summary>
        /// This method will return a list of teacher objects.
        /// This method will also allow us to reduce numbers of teachers on the list by entering key searchwords
        /// of teachers' first name or last name.
        /// </summary>
        /// <param name="id">The searchwords that can be found in teachers' first name or last name. </param>
        /// <example>
        /// GET api/TeacherData/ListTeachers/{SearchKey?}
        /// </example>
        /// <returns>A list of teacher objects</returns>
        [HttpGet]
        [Route("api/TeacherDate/ListTeachers/{SearchKey?}")]
        //GET api/TeacherData/ListTeachers/{SearchKey?}
        public List<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create a empty list for teacher objects
            List<Teacher> Teachers = new List<Teacher> { };

            //Build an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            //Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            // Write a sql query
            cmd.CommandText = "SELECT * FROM `teachers` JOIN classes ON teachers.teacherid = classes.teacherid WHERE teacherfname like @SearchKey OR teacherlname like @SearchKey ";
            cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //Create a teacher object to hold properties by accessing each row of the result set
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                NewTeacher.Salary = (decimal)ResultSet["salary"];
                NewTeacher.ClassCode = ResultSet["classcode"].ToString();
                NewTeacher.ClassName = ResultSet["classname"].ToString();

                //Add the teacher object to the empty list
                Teachers.Add(NewTeacher);
            }
            //close the connection
            conn.Close();

            //Return the list of teachers
            return Teachers;

        }

        ///Objective:Create a method that allows us to access a specific teacher's information by entering an interger value of teacherid
        /// <summary>
        /// This method will return a specific teacher's information  based on the input interger value of the teacherid
        /// </summary>
        /// <param name="id">the teacher ID in the database</param>
        /// <example>
        /// GET api/TeacherData/FindTeacher/{id}
        /// </example>
        /// <returns>A teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //Create a teacher object
            Teacher NewTeacher = new Teacher();

            //Build a instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            //Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            //Write a sql query
            cmd.CommandText = "SELECT * FROM `teachers` JOIN classes ON teachers.teacherid = classes.teacherid WHERE teachers.teacherid = " + id;

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //The teacher object hold properties by accessing each row of the result set
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                NewTeacher.Salary = (decimal)ResultSet["salary"];
                NewTeacher.ClassCode = ResultSet["classcode"].ToString();
                NewTeacher.ClassName = ResultSet["classname"].ToString();
            }
            //close the connection
            conn.Close();

            //Return the teacher's information
            return NewTeacher;

        }
    }
}
