using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using StackOverFlow.Migrations;
using StackOverFlow.Models;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace StackOverFlow.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly DBContext context;
        public QuestionsController(DBContext context)
        {
            this.context = context;
        }
        [Authorize]
        public IActionResult askquestion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> askquestion(Question question)
        {
            try
            {
                question.UserId = context.Users.First().Id;
                context.Questions.Add(question);
                context.SaveChanges();
                return RedirectToAction("DisplayQuestions");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid Content");
            }
            return View();
        }
        public ActionResult DisplayQuestions()
        {
            return View(context.Questions);
        }
        public ActionResult TopQuestions()
        {
            return View(context.Questions);
        }
        public ActionResult DisplayQuestion(int QuestionId)
        {
            if (ModelState.IsValid)
            {
                var question = context.Questions.Find(QuestionId);
                var answer = context.Answers.Where(a => a.QuestionId.Equals(QuestionId)).ToList();
                var model = new Tuple<Question, IEnumerable<Answer>>(question, answer);
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Content Not Available");
            }
            return View();
        }
        public async Task<ActionResult> Answer(Answer answer)
        {
            answer.UserId = context.Users.First().Id;
            context.Answers.Add(answer);
            context.SaveChanges();
            return RedirectToAction("DisplayQuestions","Questions");
        }
    }
}
