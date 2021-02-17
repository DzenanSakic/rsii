using AMA.Models.DTOS;
using AMA.Repositories.Interfaces;
using AMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryQuestionSubCategory _repositoryQuestionSubCategory;
        public QuestionsController(IRepositoryQuestion repositoryQuestion, 
            IQuestionService questionService,
            IRepositoryQuestionSubCategory repositoryQuestionSubCategory)
        {
            _repositoryQuestion = repositoryQuestion;
            _questionService = questionService;
            _repositoryQuestionSubCategory = repositoryQuestionSubCategory;
        }

        [HttpGet("category/{questionId}")]
        public IActionResult GetQuestionCategory(int questionId)
        {
            return Ok(_repositoryQuestionSubCategory.Find(questionId));
        }

        [HttpGet("find")]
        public IActionResult Find([FromQuery] FindQuestionsRequest request)
        {
            return Ok(_repositoryQuestion.Find(request));
        }

        [HttpGet("find/suggested")]
        public IActionResult FindSuggested()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            return Ok(_questionService.FindSuggested(userId));
        }

        [HttpPost("add")]
        public IActionResult Add(InsertQuestionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            _questionService.AddQuestion(request, userId);

            return Ok();
        }

        [HttpDelete("delete/{questionId}")]
        public IActionResult Delete(int questionId)
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
            
            var question = _repositoryQuestion.TryFind(questionId);
            if (question == null)
                return BadRequest($"Question {questionId} does not exist!");

            if (User.IsInRole("Admin") || question.UserId == userId)
                _questionService.Delete(question);
            else
                return BadRequest($"User: {userId} does not have permission to delete question!");

            return Ok();
        }

        [HttpPost("edit")]
        public IActionResult Edit(EditQuestionRequest request)
        {
            _questionService.Edit(request);

            return Ok();
        }

    }
}