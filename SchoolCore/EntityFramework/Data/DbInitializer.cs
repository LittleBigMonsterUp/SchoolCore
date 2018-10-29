using SchoolCore.Application.enumsType;
using SchoolCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.EntityFramework.Data
{
    public class DbInitializer
    { /// <summary>
      /// 初始化数据
      /// </summary>
      /// <param name="context"></param>
        public static void Initalizer(SchoolDbContext context)
        {
            context.Database.EnsureCreated();

            //检查是否有学生信息
            if (context.Students.Any())
            {
                return; //返回，不执行。
            }
            #region
            var students = new Student[]
                {
                    new Student{ RealName="爱新觉罗",EnrollmentDate=DateTime.Parse("2018-9-01")},
                    new Student{ RealName="王富贵",EnrollmentDate=DateTime.Parse("2015-9-01")},
                    new Student{ RealName="李丹",EnrollmentDate=DateTime.Parse("2018-3-01")},
                    new Student{ RealName="李小明",EnrollmentDate=DateTime.Parse("2018-9-01")},
                    new Student{ RealName="贺兰进明",EnrollmentDate=DateTime.Parse("2018-8-01")},

                };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ CourseId=11,Title="数学",Credits=3},
                new Course{ CourseId=25,Title="政治",Credits=3},
                new Course{ CourseId=30,Title="物理",Credits=1},
                new Course{ CourseId=70,Title="化学",Credits=4},
                new Course{ CourseId=65,Title="英语",Credits=2},
                new Course{ CourseId=55,Title="历史",Credits=4},
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);

            }
            context.SaveChanges();
            var enrollments = new Enrollment[]
            {
                new Enrollment{ EnrollmentId=1,CourseId=11,Grade=CourseGrade.A},
                new Enrollment{ EnrollmentId=1,CourseId=25,Grade=CourseGrade.A},
                new Enrollment{ EnrollmentId=2,CourseId=65,Grade=CourseGrade.B},
                new Enrollment{ EnrollmentId=2,CourseId=55,Grade=CourseGrade.C},
                new Enrollment{ EnrollmentId=2,CourseId=30,Grade=CourseGrade.B},
                new Enrollment{ EnrollmentId=3,CourseId=30,Grade=CourseGrade.F},
                new Enrollment{ EnrollmentId=3,CourseId=11,Grade=CourseGrade.D},
                new Enrollment{ EnrollmentId=4,CourseId=25,Grade=CourseGrade.E},
                new Enrollment{ EnrollmentId=5,CourseId=55,Grade=CourseGrade.A},

            };
            foreach (Enrollment f in enrollments)
            {
                context.Enrollments.Add(f);

            }
            context.SaveChanges();
            #endregion
            #region 添加种子老师信息

            var instructors = new[]
            {
                new Instructor
                {
                    RealName = "孔子",
                    HireDate = DateTime.Parse("1995-03-11")
                },
                new Instructor
                {
                    RealName = "墨子",
                    HireDate = DateTime.Parse("2003-03-11")
                },
                new Instructor
                {
                    RealName = "荀子",
                    HireDate = DateTime.Parse("1990-03-11")
                },
                new Instructor
                {
                    RealName = "鬼谷子",
                    HireDate = DateTime.Parse("1985-03-11")
                },
                new Instructor
                {
                    RealName = "孟子",
                    HireDate = DateTime.Parse("2003-03-11")
                },
                new Instructor
                {
                    RealName = "朱熹",
                    HireDate = DateTime.Parse("2003-03-11")
                }
            };

            foreach (var i in instructors)
                context.Instructors.Add(i);
            context.SaveChanges();

            #endregion
            #region 添加部门的种子的数据

            var departments = new[]
            {
                new Department
                {
                    Name = "论语",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new Department
                {
                    Name = "兵法",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new Department
                {
                    Name = "文言文",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                },
                new Department
                {
                    Name = "世界和平",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                }
            };

            foreach (var d in departments)
                context.Departments.Add(d); 
            context.SaveChanges();

            #endregion
            #region 办公室分配的种子数据

            var officeAssignments = new[]
            {
                new OfficeAssignment
                {
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id,
                    Location = "逸夫楼 17"
                },
                new OfficeAssignment
                {
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id,
                    Location = "青霞路 27"
                },
                new OfficeAssignment
                {
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id,
                    Location = "天府楼 304"
                }
            };

            foreach (var o in officeAssignments)
                context.OfficeAssignments.Add(o);
            context.SaveChanges();

            #endregion
            #region 课程老师的种子数据

            var courseInstructors = new[]
            {
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "政治").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "化学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "生物").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "英语").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "物理").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "历史").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                }

            };

            foreach (var ci in courseInstructors)
                context.CourseAssignments.Add(ci);
            context.SaveChanges();

            #endregion
           
        }
    }
}
