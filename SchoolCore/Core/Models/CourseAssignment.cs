using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Core.Models
{     
    /// <summary>
    /// 课程分配
    /// </summary>
    public class CourseAssignment
    {
        /// <summary>
        /// 教师id
        /// </summary>
        public int InstructorId { get; set; }


        public int CourseId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
