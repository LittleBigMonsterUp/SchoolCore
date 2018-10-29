using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Core.Models
{     

     /// <summary>
     /// 办公室分配
     /// </summary>
    public class OfficeAssignment
    { /// <summary>
      /// 教师Id
      /// </summary>
        [Key]
        public int InstructorId { get; set; }

        /// <summary>
        /// 办公室的位置
        /// </summary>
        [StringLength(50)]
        [Display(Name = "办公室")]
        public string Location { get; set; }
        /// <summary>
        /// 教师的导航属性
        /// </summary>
        public Instructor Instructor { get; set; }
    }
}
