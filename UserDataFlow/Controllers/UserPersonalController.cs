using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UserDataFlow.Interface;
using UserDataFlow.Model.User;

namespace UserDataFlow.Controllers
{
    /// <summary>
    /// <summary>This api used for user management</summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserPersonalController : ControllerBase
    {
        private readonly ILogger<UserPersonalController> _logger;
        private readonly IUser _user;

        public UserPersonalController(ILogger<UserPersonalController> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }

        // GET: api/<UserController>
        /// <summary>
        /// Get All Users in the database
        /// </summary>
        /// <returns>Users list will return</returns>
        [HttpGet("getAllUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _user.GetUsers();
                return new OkObjectResult(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return new BadRequestResult();
        }

        /// <summary>
        /// Get Specific user from database using
        /// <param name="id">User id</param>.
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet("getUser/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _user.GetUser(id);
                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return new OkObjectResult("User not found");
        }

        /// <summary>
        /// Add new user with
        /// <param name="UserModel">UserModel</param>
        /// object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>User Model</returns>

        [HttpPost("addUser")]
        public IActionResult Post([FromBody] UserModel value)
        {
            try
            {
                var userId = _user.AddUser(value);
                return new OkObjectResult(userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }

        /// <summary>
        /// Update the user data, but password won't be update in this api.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("updateUser/{id}")]
        public IActionResult Put(int id, [FromBody] UserModel user)
        {
            try
            {
                var isUpdated = _user.UpdateUser(id, user);
                if (isUpdated)
                {
                    return new OkObjectResult("User updated successfully added");
                }

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new NotFoundObjectResult("User not found");
        }

        /// <summary>
        /// Delete the user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteAccount/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var isDeleted = _user.DeleteUser(id);
                if (isDeleted)
                    return new OkObjectResult("Successfully deleted");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                return new NotFoundObjectResult(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            try
            {
                var loginUser = _user.Login(user);
                return new OkObjectResult(loginUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
