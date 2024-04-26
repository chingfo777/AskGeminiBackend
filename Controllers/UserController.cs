using AskGemini.Dto;
using AskGemini.Interfaces;
using AskGemini.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AskGemini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper )
        {
          _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string userId)
        {
            if(!_userRepository.UserExist(userId))
            {
                return NotFound();
            }

            var user = _userRepository.GetUser(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUsers()
                .Where(c => c.Id.Trim().
                ToUpper() == userCreate.Id.TrimEnd()
                .ToUpper()).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving...");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created...");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(string userId, [FromBody]UserDto upadatedUser) {
            if(upadatedUser == null)
            {
                return BadRequest(ModelState);
            }

            if(userId != upadatedUser.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExist(userId)) { return NotFound(); }

            if(!ModelState.IsValid) { return BadRequest(); }
            var userMap = _mapper.Map<User>(upadatedUser);
            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(string userId)
        {
            if (!_userRepository.UserExist(userId))
            {
                return NotFound();
            }

            var categoryToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.DeleteUser(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }
            return NoContent();
        }

    }
}
