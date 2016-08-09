using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASP_TDD;
using ASP_TDD.Controllers;
using ASP_TDD.Models;
using ASP_TDD.DAL;
using System.Web.Routing;
using System.Web;
using System.Security.Principal;
using ASP_TDD.Tests.Models;

namespace ASP_TDD.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        Student GetPerson()
        {
            return GetPerson(1, "Bill", "Gates");
        }
        Student GetPerson(int id, string fName, string lName)
        {
            return new Student { PersonId = id, FirstName = fName, LastName = lName };
        }

        //Method to get the HomeController Object. Unit Tests will call methods in the HomeController
        private static HomeController GetHomeController(IStudentRepository repository)
        {
            HomeController controller = new HomeController(repository);

            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }
        //Mocks the HttpContext Object
        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someuser"), null);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }

                set
                {
                    base.User = value;
                }
            }
        }
        [TestMethod]
        public void Index_Get_AsksForIndexView()
        {
            //Arrange
            var controller = GetHomeController(new InMemoryStudentRepository());
            //Act
            ViewResult result = controller.Index();
            //Assert
            Assert.AreEqual("Index", result.ViewName);
            //Verifies that the Index method returns a view named Index
        }

        [TestMethod]
        public void Index_Get_RetrievesAllContactsFromRepository()
        {
            //Arrange
            Student p1 = GetPerson(1, "Sammy", "Hagar");
            Student p2 = GetPerson(2, "Johnny", "Walker");
            InMemoryStudentRepository repository = new InMemoryStudentRepository();
            repository.InsertStudent(p1);
            repository.InsertStudent(p2);
            var controller = GetHomeController(repository);

            //Act
            var result = controller.Index();

            //Assert
            var model = (IEnumerable<Student>)result.ViewData.Model;
            CollectionAssert.Contains(model.ToList(), p1);
            CollectionAssert.Contains(model.ToList(), p2);
        }

        [TestMethod]
        public void Create_Post_ReturnsViewIfModeStateIsNotValid()
        {
            //Arrange
            HomeController controller = GetHomeController(new InMemoryStudentRepository());
            // Simply executing a method during a unit test does just that - executes a method, and no more. 
            // The MVC pipeline doesn't run, so binding and validation don't run.
            controller.ModelState.AddModelError("", "mock error message");
            Student model = GetPerson(1, "", "");

            //Act
            var result = controller.Create(model);
            //Assert
            Assert.AreEqual("Create", ((ViewResult)result).ViewName); //This doesn't really work yet
        }
        [TestMethod]
        public void Create_Post_PutsValidContactIntoRepository()
        {
            // Arrange
            InMemoryStudentRepository repository = new InMemoryStudentRepository();
            HomeController controller = GetHomeController(repository);

            Student s1 = GetPerson();

            //Act
            controller.Create(s1);

            //Assert
            IEnumerable<Student> students = repository.GetStudents();
            Assert.IsTrue(students.Contains(s1));
        }

        [TestMethod]
        public void Create_Post_ReturnsViewIfRepositoryThrowsException()
        {
            //Arrange
            InMemoryStudentRepository repository = new InMemoryStudentRepository();
            Exception exception = new Exception();
            repository.ExceptionToThrow = exception;
            HomeController controller = GetHomeController(repository);
            Student s1 = GetPerson();

            //Act
            var result = controller.Create(s1);

            //Assert
            Assert.AreEqual("Create", result.ViewName);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
            Assert.AreEqual(exception, modelState.Errors[0].Exception);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
