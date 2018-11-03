using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolCore.Core.Models;
namespace SchoolCore.ViewModels
{
    public class InstructorIndexData
    {
        public  List<Instructor> Instructor { get; set; }
        public List <Course > Courses { get; set; }
        public List<Enrollment> Enrollments { get; set; }

    }
}
