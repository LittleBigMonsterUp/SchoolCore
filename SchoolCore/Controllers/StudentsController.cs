using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolCore.Common;
using SchoolCore.Core.Models;
using SchoolCore.EntityFramework;

namespace SchoolCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchStudent, int? page, string currentStudent)//首先看传过来的值，排序是否有值
        {
            #region   搜索和排序
            //ViewData DataView表示用于排序、筛选、搜索、编辑和导航的 DataTable 的可绑定数据的自定义视图。
            //姓名的排序参数
            ViewData["Name_Sort_Parm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //时间的排序参数
            ViewData["Date_Sort_Parm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //搜索的关键字
            ViewData["SearchStudent"] = searchStudent;
            //当前排序的参数
            //当前过滤页的参数
            #endregion


            ViewData["CurrentSort"] = sortOrder;


            if (searchStudent != null)
                page = 1;
            else
                searchStudent = currentStudent;

            #region 搜索和排序功能

            var students = from student in _context.Students select student;

            if (!string.IsNullOrWhiteSpace(searchStudent))
                students = students.Where(a => a.RealName.Contains(searchStudent));//Contains模糊查询相当于sql 的likeS


            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(a => a.RealName);//倒序
                    break;

                case "Date":
                    students = students.OrderBy(a => a.EnrollmentDate);//正序
                    break;

                case "date_desc":
                    students = students.OrderByDescending(a => a.EnrollmentDate);
                    break;

                default:
                    students = students.OrderBy(a => a.RealName);
                    break;
            }

            #endregion

            var pageSize = 4;//当前页为4页


            var entities = students.AsNoTracking();
            //将当前的页传过去，如果当前页为1，再把页传给他          //先将当前数据库实体传过去，在将当前页传过去，在将每一页有多少分页值
            var dtos = await PaginatedList<Student>.CreatepagingAsync(entities, page ?? 1, pageSize);

            return View(dtos);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //贪婪加载
            //AsNoTracking 告诉ef不用标记他我，我只做查询，在查询到数据实体是我不会对其操作，加快查询速度，提高效率
            var student = await _context.Students.Include(a => a.Enrollments).ThenInclude(e => e.Course).AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            //FirstOrDefaultAsync 返回第一个
            //SingleOrDefaultAsync 如果查到两个一样的就会出错
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]//防止跨站攻击利用viewModel
        public async Task<IActionResult> Create( Student student)//学生传过来的实体不应直接操作数据库
        {                                                           //通过dto进行所有的属性一个关联，达到一个复用的可能性
            try
            {
                if (ModelState.IsValid)
                {
                    var students = new Student();
                    {
                        students.RealName = student.RealName;
                        students.EnrollmentDate = student.EnrollmentDate;
                    }
                }
                
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "无法进行数据保存，请检查你的数据。");

            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,RealName,EnrollmentDate,Secret")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            var entity = await _context.Students.SingleOrDefaultAsync(a => a.Id == id);

            if (await TryUpdateModelAsync<Student>(entity, "", s => s.RealName, s => s.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException e)
                {

                    ModelState.AddModelError("", "无法进行数据保存，请检查你的数据,是否异常");

                }
            }
                return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id,bool? saveChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.SaveError = "删除失败，请在次尝试，如果尝试失败，请联系管理员。";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
