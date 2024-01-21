using integral_api.Data;
using integral_api.Models;
using IntegralAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApplication1;

namespace IntegralAPI.Controllers
{
    [ApiController]

    [Route(template: "[controller]")]
    public class IntegralController : Controller
    {
        [HttpGet("calculateIntegral")]
        public async Task<ActionResult<double>> CalculateIntegral(int SplitNumbers, double UpLim, double LowLim)
        {
            try
            {
                SimsonMethod simsonMethod = new SimsonMethod(SplitNumbers, UpLim, LowLim);
                simsonMethod.Calculate();

                if (double.IsInfinity(simsonMethod.res) || double.IsNaN(simsonMethod.res))
                {
                    return BadRequest("Result of integration is not a valid number.");
                }

                return Ok(simsonMethod.res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       
    }

    [Route(template: "[controller]")]
        public class QnAController : Controller
        {
            private readonly QnAContext _context;

            public QnAController(QnAContext context)
            {
            _context = context;
            }


            [HttpGet("get-questions")]
            public ActionResult<IEnumerable<QuestionModel>> GetQuestions()
            {
                var questions = _context.Questions.Include(q => q.Answers).ToList();

                foreach (var question in questions)
                {
                    question.Answers = _context.Answers.Where(a => a.QuestionId == question.Id).ToList();
                }

                return Ok(questions);
            }

            [HttpPost("check-answers")]
            public ActionResult<int> CheckAnswers(System.Collections.Generic.List<int> userAnswers)
            {
                // Проверка наличия ответов пользователя
                if (userAnswers == null || userAnswers.Count == 0)
                {
                    return BadRequest("Не переданы ответы пользователя.");
                }

                // Получение правильных ответов из базы данных
                var correctAnswers = _context.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList();

                // Подсчет количества правильных ответов
                var correctCount = userAnswers.Count(answer => correctAnswers.Contains(answer));

                return Ok(correctCount);
            }
        }
}
