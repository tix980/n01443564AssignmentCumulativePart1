using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01443564AssignmentCumulativePart1.Models;

namespace n01443564AssignmentCumulativePart1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/List
        public ActionResult List()
        {
            //Instantiating the StudentDataController
            StudentDataController Controller = new StudentDataController();
            //Access ListStudents() method
            List<Student> Students = Controller.ListStudents();
            return View(Students);
        }

        // GET: Student/Show/{id}
        public ActionResult Show(int id)
        {
            //Instantiating the StudentDataController
            StudentDataController Controller = new StudentDataController();
            //Access FindStudent(int id)Method
            Student Student = Controller.FindStudent(id);
            return View(Student);
        }
    }
}