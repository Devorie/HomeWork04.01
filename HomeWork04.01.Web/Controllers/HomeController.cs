using HomeWork04._01.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using HomeWork04._01.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace HomeWork04._01.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionsRepository(_connectionString);
            var vm = new IndexViewModel();
            vm.Questions = repo.GetAllQuestions();

            if (HttpContext.Session.GetString("likedids") != null)
            {
                vm.LikedImages = HttpContext.Session.Get<List<int>>("likedids");
            }
            return View(vm);

        }

        public IActionResult ViewForTag(string tagName)
        {
            var repo = new QuestionsRepository(_connectionString);
            return View(repo.GetQuestionsForTag(tagName));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            var repoUser = new UserDb(_connectionString);
            question.DatePosted = DateTime.Now;
            question.UserId = repoUser.GetByEmail(User.Identity.Name).Id;
            var repo = new QuestionsRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/home/index");
        }

        [Authorize]
        public IActionResult NewQuestion()
        {
            return View();
        }

        [Authorize]
        public IActionResult ViewQuestion(int Id)
        {
            var repo = new QuestionsRepository(_connectionString);
            if (repo.GetQuestionById == null)
            {
                return Redirect("/home/index");
            }
            var repoUser = new UserDb(_connectionString);
            var vm = new ViewQuestionsVeiwModel();
            vm.Question = repo.GetQuestionById(Id);
            vm.UserName = repoUser.GetByEmail(User.Identity.Name).Name;
            
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAnswer(int Id, string Text)
        {
            var repoUser = new UserDb(_connectionString);
            Answer answer = new();
            answer.DateTime = DateTime.Now;
            answer.PersonId = repoUser.GetByEmail(User.Identity.Name).Id;
            answer.QuestionId = Id;
            answer.Text = Text;
            var repo = new QuestionsRepository(_connectionString);
            repo.AddAnswer(answer);
            return Redirect("/home/index");
        }

        [HttpPost]
        public void LikeImage(int id)
        {
            var repo = new QuestionsRepository(_connectionString);
            repo.AddLike(id);
            List<int> likedIds = HttpContext.Session.Get<List<int>>("likedids") ?? new List<int>();
            likedIds.Add(id);

            HttpContext.Session.Set("likedids", likedIds);
        }

        public ActionResult GetLikes(int id)
        {
            var repo = new QuestionsRepository(_connectionString);
            return Json(new { Likes = repo.GetLikes(id) });
        }

    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}
