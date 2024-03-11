using DAL.Context;
using DAL.DatabaseManager;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examination_System_MVC.Controllers
{
	public record StudentRec(int StID, string Fname, string Lname, double grade);

	public class InstructorController : Controller
	{
		ExamSystemContext context = ContextManager.MyContext;
		public IActionResult AddQuestion()
		{
            int? InstructorID = HttpContext.Session.GetInt32("InstructorID");

			if (InstructorID != null)
			{
				ViewBag.allcourses = context.Courses.ToList();
				return View();
			}
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
		[HttpPost]
		public IActionResult AddQuestion(IFormCollection f)
		{
            
				ViewBag.allcourses = context.Courses.ToList();
				Choice first = new() { Value = f["firstChoice"] };
				Choice second = new() { Value = f["secondChoice"] };
				Choice third = new() { Value = f["thirdChoice"] };
				Choice forth = new() { Value = f["forthChoice"] };
				Choice[] choices = [first, second, third, forth];


				context.Choices.Add(first);
				context.Choices.Add(second);
				context.Choices.Add(third);
				context.Choices.Add(forth);
				int c = int.Parse(f["ModelAnswer"]);
				Question newQuest = new() { Choices = choices, Complexity = f["complexity"], QText = f["qBody"], CrsID = int.Parse(f["CrsID"]), Type = "MCQ", ModelAnsNavigation = choices[c] };

				context.Questions.Add(newQuest);
				context.SaveChanges();

				return View();
			
          
        }

		[HttpPost]
		public IActionResult AddTFQuestion(IFormCollection f)
		{
			ViewBag.allcourses = context.Courses.ToList();
			Choice first = null;
			Choice second = null;


			if (context.Choices.Where(c => c.Value == "True").ToList().Count > 0)
			{
				first = context.Choices.FirstOrDefault(c => c.Value == "True");
			}
			else
			{
				first = new() { Value = "True" };
				context.Choices.Add(first);
			}
			if (context.Choices.Where(c => c.Value == "False").ToList().Count > 0)
			{
				second = context.Choices.FirstOrDefault(c => c.Value == "False");
			}
			else
			{
				second = new() { Value = "False" };
				context.Choices.Add(second);
			}

			Choice[] choices = [first, second];

			int c = int.Parse(f["ModelAnswer"]);
			Question newQuest = new() { Choices = choices, Complexity = f["complexity"], QText = f["qBody"], CrsID = int.Parse(f["CrsID"]), Type = "TF", ModelAnsNavigation = choices[c] };

			context.Questions.Add(newQuest);
			context.SaveChanges();

			return RedirectToAction("AddQuestion");
		}


		public IActionResult CreateExam()
		{
            int? InstructorID = HttpContext.Session.GetInt32("InstructorID");

            if (InstructorID != null)
            {
                ViewBag.allcourses = context.Courses.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
		}

		[HttpPost]
		public IActionResult CreateExam(Exam exam)
		{
			ViewBag.allcourses = context.Courses.ToList();
			if (exam.StartTime > exam.EndTime)
			{
				return View(exam);
			}
			context.Exams.Add(exam);
			context.SaveChanges();
			return RedirectToAction("CreateExam");
		}


		public IActionResult ShowResult(int CrsID = 0)
		{
            //public record StudentRec(int id, string Fname, string Lname, decimal grade);
            int? InstructorID = HttpContext.Session.GetInt32("InstructorID");

            if (InstructorID != null)
            {
                var Stud = context.Database.SqlQuery<StudentRec>($"""
				select s.StID, p.Fname , p.Lname, st.Grade
				from StudentCourse st
				inner join Student s
				on s.StID = st.StID
				inner join Person p
				on s.PersonID = p.ID
				where st.CrsID = {CrsID} and Grade is not null
				""").ToList();

                SelectList crsList = new(context.Courses, "CrsID", "CrsName", CrsID);
                ViewBag.custList = crsList;
                ViewBag.allcourses = context.Courses.ToList();
                //var StdsQuery = context.StudentCourses.Include(s => s.St).Where(s => s.CrsID == CrsID);
                ViewBag.AllStudents = Stud;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
		}
        public IActionResult Reports()

        {
            int? InstructorID = HttpContext.Session.GetInt32("InstructorID");

			if (InstructorID != null)
			{
				return View();
			}
            return RedirectToAction("Index", "Home");


        }
        public IActionResult Logout()
		{
			return View("~/Views/Auth/login.cshtml");
		}


	}
}