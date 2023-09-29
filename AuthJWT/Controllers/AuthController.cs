using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IMongoCollection<User> _userCollection;
        public AuthController(IMongoClient client, IConfiguration configuration)
        {
            _configuration = configuration;
            var database = client.GetDatabase("AuthDB");
            _userCollection = database.GetCollection<User>("User");
        }
        [HttpGet("getAlluser")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers()
        {
            var usersCursor = _userCollection.Find(_ => true).ToCursor();
            var usersList = usersCursor.ToList();

            return Ok(usersList);
        }
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginModel userView)
        {
            User user = _userCollection.Find(user => user.name == userView.name).FirstOrDefault();

            if (user == null || user.password != userView.password)
                return Unauthorized();

            // Check if the user has the "admin" role
            // User is not authorized as an admin
            Authentication authentication = new Authentication(_configuration);
            var token = authentication.GenerateJwtToken(user.role != null ? user.role : "UnRole", user.id);
            return Ok(new { Token = token });
        }
        [HttpPost]
        public IActionResult addUser(UserView userView)
        {
            User user = new User
            {
                id = new Guid(),
                name = userView.name,
                password = userView.password,
                role = "admin"
            };
            _userCollection.InsertOne(user);
            return Ok(user);
        }
        [HttpPost("logout")]
        [Authorize] // Require authentication to access this endpoint
        public IActionResult Logout()
        {
            // You can perform any necessary logout actions here
            // For example, you can invalidate the current JWT token if needed

            // Return a successful response indicating that the user is logged out
            return Ok(new { Message = "Logged out successfully" });
        }
    }
}
