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
    public class AnswersController : ControllerBase
    {
        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryAnswerVoting _repositoryAnswerVoting;
        private readonly IAnswerService _answerService;
        public AnswersController(IRepositoryAnswer repositoryAnswer,
            IAnswerService answerService,
            IRepositoryAnswerVoting repositoryAnswerVoting)
        {
            _repositoryAnswer = repositoryAnswer;
            _answerService = answerService;
            _repositoryAnswerVoting = repositoryAnswerVoting;
        }

        [HttpPost("add")]
        public IActionResult Add(InsertAnswerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            return Ok(_answerService.AddAnswer(request, userId));
        }

        [HttpGet("all/{questionId}")]
        public IActionResult Get(int questionId)
        {
            return Ok(_repositoryAnswer.FindAll(questionId));
        }

        [HttpGet("votings/{answerId}")]
        public IActionResult GetVotings(int answerId)
        {
            var answer = _repositoryAnswer.Find(answerId);
            if (answer == null)
                return BadRequest($"Answer with id: {answerId} does not exist!");

            return Ok(_repositoryAnswerVoting.GetAll(answerId));
        }

        [HttpPost("votings/add")]
        public IActionResult AddVoting(AddAnswerVotingRequest request)
        {
            var answer = _repositoryAnswer.Find(request.AnswerId);
            if (answer == null)
                return BadRequest($"Answer with id: {request.AnswerId} does not exist!");

            _answerService.AddVoting(request);

            return Ok(_repositoryAnswerVoting.GetAll(request.AnswerId));
        }

        [HttpDelete("delete/{answerId}")]
        public IActionResult Delete(int answerId)
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            var answer = _repositoryAnswer.Find(answerId);
            if (answer == null)
                return BadRequest($"Answer: {answerId} does not exist!");
           
            if (User.IsInRole("Admin") || answer.UserId == userId)
                _answerService.Delete(answer);
            else
                return BadRequest($"User: {userId} does not have permission to delete answer!");

            return Ok();
        }

        [HttpPost("correct/{answerId}")]
        public IActionResult MarkAsCorrect(int answerId)
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            var answer = _repositoryAnswer.Find(answerId);
            if (answer == null)
                return BadRequest($"Answer: {answerId} does not exist!");

            return Ok(_answerService.ChangeCorrectState(answer));
        }
    }
}