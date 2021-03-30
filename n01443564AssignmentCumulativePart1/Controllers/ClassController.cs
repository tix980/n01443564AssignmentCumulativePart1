using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01443564AssignmentCumulativePart1.Models;

namespace n01443564AssignmentCumulativePart1.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class/List
        public ActionResult List()
        {
            //Instantiating the ClassDataController
            ClassDataController Controller = new ClassDataController();
            //Access ListClasses() method
            List<Class> Class = Controller.ListClasses();
            return View(Class);
        }

        //Get: Class/Show/{id}
        public ActionResult Show(int id)
        {
            //Instantiating the ClassDataController
            ClassDataController Controller = new ClassDataController();
            //Access FindClass(int id)Method
            Class Class = Controller.FindClass(id);

            return View(Class);
        }
    }
}