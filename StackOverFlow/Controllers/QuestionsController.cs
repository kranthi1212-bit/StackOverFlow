using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using StackOverFlow.Migrations;
using StackOverFlow.Models;
using System.Data;
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
            ViewBag.Titletag = "Newest";
            ViewBag.Count = context.Questions.Count();
            return View(context.Questions);
        }
        [HttpPost]
        public ActionResult DisplayQuestions(string buttonType)
        {
            ViewBag.Title = "Newest";
            switch (buttonType)
            {
                case "Newest":
                    ViewBag.Titletag = "Newest";
                    break;
                case "Active":
                    ViewBag.Titletag = "Recently Active";
                    var Queid = context.Answers.Select(U => U.QuestionId).ToList();
                    if (Queid != null)
                    {
                        var question = context.Questions.Where(U => U.QuestionId.Equals(Queid)).ToList();
                        return View(question);
                    }
                    break;
                case "Bountied":
                    ViewBag.Titletag = "Bountied";
                    break;
                case "Unanswered":
                    ViewBag.Titletag = "Unanswered";
                    var unansque = context.Answers.Select(U => U.QuestionId).ToList();
                    var unansque1 = context.Questions.Select(U => U.QuestionId).ToList();
                    if (unansque != null)
                        return View(context.Questions.Where(U => U.QuestionId.Equals(unansque)).ToList());   
                    break;
                case "Frequent":
                    ViewBag.Titletag = "Frequent";
                    break;
                case "Score":
                    ViewBag.Titletag = "High Score";
                    break;
                case "Trending":
                    ViewBag.Titletag = "Trending";
                    break;
                case "Week":
                    ViewBag.Titletag = "Most active";
                    break;
                default:
                    ViewBag.Titletag = "Most active";
                    break;
            }
            ViewBag.Count = context.Questions.Count();
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
            return RedirectToAction("DisplayQuestions", "Questions");
        }
    }
}
