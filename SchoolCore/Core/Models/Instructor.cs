using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Core.Models
{    

      /// <summary>
      /// 教师
      /// </summary>
    public class Instructor
    {

        public int Id { get; set; }

        public string RealName { get; set; }
        [DisplayName("入职时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }



        public ICollection<CourseAssignment> CourseAssignments { get; set; }


        public OfficeAssignment OfficeAssignment { get; set; }
    }
}
