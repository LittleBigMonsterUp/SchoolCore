using SchoolCore.Application.enumsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Core.Models
{      
      /// <summary>
      /// 注册信息
      /// </summary>
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public CourseGrade? Grade { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
