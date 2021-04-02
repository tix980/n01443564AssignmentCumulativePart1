using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01443564AssignmentCumulativePart1.Models;

namespace n01443564AssignmentCumulativePart1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        // Or GET: Teacher/List?Searchkey= {SearchKey}
        public ActionResult List(string SearchKey = null)
        {
            //Instantiating the TeacherDataController
            TeacherDataController controller = new TeacherDataController();
            ////Access ListTeachers() method
            List<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            //Instantiating the TeacherDataController
            TeacherDataController controller = new TeacherDataController();
            //Access FindTeacher(int id)Method
            Teacher Teacher = controller.FindTeacher(id);
            return View(Teacher);
        }
    }
}