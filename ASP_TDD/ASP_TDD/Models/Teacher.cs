using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_TDD.Models
{
    public class Teacher : Person
    {
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        //public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}