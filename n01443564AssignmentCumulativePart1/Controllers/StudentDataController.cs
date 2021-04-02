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
    public class StudentDataController : ApiController
    {
        //Instantiating the schooldb database context class to allow us
        //to access the schooldb database.
        private SchoolDbContext School = new SchoolDbContext();


        ///Objective:Create a method that allows us to access the students table of the datbase
        /// <summary>
        /// The method will return a list of student objects
        /// This method will also allow us to reduce numbers of students on the list by entering key searchwords
        /// of students' first name or last name.
        /// </summary>
        /// <param name="id">The searchwords that can be found in students' first name or last name. </param>
        /// <example>
        /// GET api/studentData/Liststudents/{SearchKey?}
        /// </example>
        /// <returns>A list of student objects</returns>
        [HttpGet]
        [Route("api/studentData/Liststudents/{SearchKey?}")]
        public List<Student> ListStudents(string SearchKey = null)
        {
            //Create a empty list for student objects
            List<Student> Students = new List<Student> { };

            //Build an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            // Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            // Write a sql query
            cmd.CommandText = "SELECT * FROM students WHERE studentfname like @SearchKey OR studentlname like @SearchKey";
            cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //Create a student object to hold properties by accessing each row of the result set
                Student NewStudent = new Student();
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"].ToString());

                //Add the student object to the empty list
                Students.Add(NewStudent);
            }
            //close the connection
            conn.Close();

            //Return the list of students
            return Students;
        }

        ///Objective:Create a method that allows us to access a specific student's information by entering an interger value of studentid
        /// <summary>
        /// This method will return a specific student's information  based on the input interger value of the studentid
        /// </summary>
        /// <param name="id">the student ID in the database</param>
        /// <example>
        /// GET api/StudentData/FindStudent/{id}
        /// </example>
        /// <returns>A Student object</returns>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            //Create a student object
            Student NewStudent = new Student();

            //Build a instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //Activate the connection between the web server and database
            conn.Open();

            //Create a command for our database
            MySqlCommand cmd = conn.CreateCommand();

            //Write a sql query
            cmd.CommandText = "SELECT * FROM students WHERE studentid = " + id;

            //Gather the result set after executing the query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set via while statement
            while (ResultSet.Read())
            {
                //The student object hold properties by accessing each row of the result set
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"].ToString());
            }

            //close the connection
            conn.Close();

            //Return the student information
            return NewStudent;

        }
    }
}
