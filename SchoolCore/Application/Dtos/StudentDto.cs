using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Application.Dtos
{
    /// <summary>
    /// 相当于viewmodel
    /// </summary>
    public class StudentDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "姓名长度不能超过30字符")]
        [DisplayName("学生姓名")]
        public string RealName { get; set; }

        [DisplayName("注册时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
    }
}
