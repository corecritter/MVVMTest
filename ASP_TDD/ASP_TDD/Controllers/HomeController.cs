using ASP_TDD.DAL;
using ASP_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_TDD.Controllers
{
    public class HomeController : Controller
    {
        IStudentRepository _repository;

        public HomeController()
        {

        }
        public HomeController(IStudentRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            return View("Index", _repository.GetStudents());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Create(Student newStudent)
        {
            _repository.InsertStudent(newStudent);
            _repository.Save();

            return View("Create");
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

    }
}