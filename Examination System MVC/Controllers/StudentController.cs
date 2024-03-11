using DAL.DatabaseManager;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Examination_System_MVC.Controllers
{
    public class StudentController : Controller
    {


        public IActionResult Main(int personId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");

            if (UserId != null)
            {

            
                var student = ContextManager.MyContext.Students.Where(s => s.PersonID == personId).Include(s => s.StudentCourses).FirstOrDefault();
                List<Exam> exams = new List<Exam>();
                if (student != null || student.StudentCourses != null)
                {
                    foreach (var crs in student.StudentCourses)
                    {
                        var exam = ContextManager.MyContext.Exams.Where(e => e.CrsID == crs.CrsID).Include(e => e.Crs).OrderByDescending(p => p.ExamID).FirstOrDefault();
                        if (exam != null)
                        {
                            bool examTaken = ContextManager.MyContext.ExamQuestions
                                .Any(e => e.ExamID == exam.ExamID && e.StID == student.StID);
                            if (!examTaken)
                            {
                                exams.Add(exam);
                            }
                        }
                    }
                }
                ViewBag.Exams = exams;
                return View(student);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Instructions(int StID, int ExamID)
        {
            ViewBag.StID = StID;
            var exam = ContextManager.MyContext.Exams.Where(e=>e.ExamID == ExamID).Include(e=>e.Crs).FirstOrDefault();
            if (exam != null)
            {
                return View(exam);
            }
            else
            {
                return RedirectToAction("Main");
            }
        }
        [HttpPost]
        public IActionResult Exam(int StID, int examId)
        {

            var exam = ContextManager.MyContext.Exams.Where(e => e.ExamID == examId).Include(e => e.Crs).FirstOrDefault();

            var res = ContextManager.MyContext.Database.ExecuteSql($"GenerateExam {exam.CrsID}, {StID}, {examId}");
            if (res == 0)
            {
                return RedirectToAction("Instructions");
            }
            
            var questions = ContextManager.MyContext.ExamQuestions.Where(q => q.ExamID == examId && q.StID == StID).Include(q=>q.QIDNavigation).ThenInclude(q=>q.Choices).Select(Q=>Q.QIDNavigation).ToList();
            
            ViewBag.exam = exam;
            ViewBag.StID = StID;
            return View(questions);
        }
        [HttpPost]
        public IActionResult SubmitMessage(int StID, int  ExamID , int Choice_1 , int Choice_2 , int Choice_3 , int Choice_4, int  Choice_5, int  Choice_6 , int Choice_7 , int Choice_8, int  Choice_9, int  Choice_10)
        {
            var res = ContextManager.MyContext.Database.ExecuteSql($"ExamAnsProc {StID}, {ExamID}, {Choice_1}, {Choice_2}, {Choice_3}, {Choice_4}, {Choice_5}, {Choice_6}, {Choice_7}, {Choice_8}, {Choice_9}, {Choice_10}");
            if (res != 0)
            {
                var corr = ContextManager.MyContext.Database.ExecuteSql($"  [dbo].[ExamCorrection] {StID}, {ExamID}");   
            }
            int personId = (int)ContextManager.MyContext.Students.Where(p => p.StID == StID).Select(s => s.PersonID).FirstOrDefault();
            return View(personId);
        }

    }
}
