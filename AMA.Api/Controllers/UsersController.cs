using AMA.Models.DTOS;
using AMA.Repositories.Interfaces;
using AMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryMessage _repositoryMessage;
        private readonly IRepositoryPayment _repositoryPayment;
        private readonly IRepositoryUserRole _repositoryUserRole;
        private readonly IRepositoryBan _repositoryBan;
        public UsersController(IUserService userService, 
            IRepositoryUser repositoryUser,
            IRepositoryMessage message,
            IRepositoryPayment repositoryPayment, 
            IRepositoryUserRole repositoryUserRole,
            IRepositoryBan repositoryBan
            )
        {
            _userService = userService;
            _repositoryUser = repositoryUser;
            _repositoryMessage = message;
            _repositoryPayment = repositoryPayment;
            _repositoryUserRole = repositoryUserRole;
            _repositoryBan = repositoryBan;
        }

        [HttpPost("user/register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_repositoryUser.CheckMailAvailability(request.Mail))
                return BadRequest("Mail already in use!");
            

            if(_repositoryUser.TryFind(request.UserName) != null)
                return BadRequest("Username already in use!");
            

            _userService.RegisterUser(request);

            return Ok();
        }

        [HttpGet("find")]
        public IActionResult FindAll([FromQuery] FindUsersRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = _repositoryUser.FindAll(request);

            return Ok(users);
        }

        [HttpGet("user/{userId}")]
        public IActionResult Get(int userId)
        {
            return Ok(_repositoryUser.TryFind(userId));
        }

        [HttpGet("user/role/{userId}")]
        public IActionResult GetRole(int userId)
        {
            return Ok(_repositoryUserRole.Get(userId));
        }

        [HttpPost("user/edit")]
        public IActionResult Edit([FromBody] EditUserProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _repositoryUser.TryFind(request.UserId);

            if (user == null)
                return BadRequest($"User with id: {request.UserId} does not exist");

            if(request.Mail != user.Mail && !_repositoryUser.CheckMailAvailability(request.Mail))
                return BadRequest("Mail already in use!");
            
            _userService.EditUser(user, request);

            return Ok();
        }

        [HttpPost("user/ban")]
        [Authorize(Roles = "Admin")]
        public IActionResult Ban([FromBody] BanUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_repositoryUser.TryFind(request.UserId) == null)
                return BadRequest($"User {request.UserId} does not exist!");

            _userService.BanUser(request);

            return Ok();
        }

        [HttpGet("user/{userId}/bans")]
        public IActionResult GetBans(int userId)
        {
            return Ok(_repositoryBan.FindAllByUserId(userId));
        }

        [HttpGet("user/messages")]
        public IActionResult GetMessages()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            return Ok(_repositoryMessage.FindByUser(userId));
        }

        [HttpPost("user/message")]
        public IActionResult SendMessage([FromBody] SendMessageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            int userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
            
            if (_repositoryUser.TryFind(userId) == null)
                return BadRequest($"User with id: {userId} does not exist");

            if (_repositoryUser.TryFind(request.ToUserId) == null)
                return BadRequest($"User with id: {request.ToUserId} does not exist");

            _userService.SendMessage(request, userId);

            return Ok();
        }

        [HttpGet("user/payments")]
        public IActionResult GetPayments()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            return Ok(_repositoryPayment.FindByUser(userId));
        }

        [HttpPost("user/payment")]
        public IActionResult MakePayment(MakePaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = int.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            if (_repositoryUser.TryFind(userId) == null)
                return BadRequest($"User with id: {userId} does not exist");

            if (_repositoryUser.TryFind(request.ToUserId) == null)
                return BadRequest($"User with id: {request.ToUserId} does not exist");

            _userService.MakePayment(request, userId);

            return Ok();
        }
    }
}