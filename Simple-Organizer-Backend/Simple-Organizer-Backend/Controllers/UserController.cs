using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace Simple_Organizer_Backend.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : Controller
    {
        //DB context
        private readonly ApplicationDbContext _context;
        // GET: UserController

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult AddUser([FromBody] UserDto userDto)

        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data");
            }

            // Check if the email is in a valid format
            if (!IsValidEmail(userDto.Email))
            {
                return BadRequest("Invalid email format");
            }

            // Check if the email already exists
            var existingUser = _context.User.FirstOrDefault(u => u.email == userDto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }

            // Create a User entity from the UserDto
            var user = new User
            {
                name = userDto.Name,
                email = userDto.Email,
                image = userDto.Image
                // You may set other fields to default values if necessary
            };

            // Add the user to the database
            _context.User.Add(user);
            _context.SaveChanges();

            return Ok("User added successfully");
        }
        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate the email format
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, pattern);
        }

        //Get the user by email
        [HttpGet("get-by-email")]
        public IActionResult GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required");
            }

            var user = _context.User.FirstOrDefault(u => u.email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromBody] User updatedUserDto)
        {
            if (updatedUserDto == null)
            {
                return BadRequest("Invalid user data");
            }

            // Check if the email is in a valid format
            if (!IsValidEmail(updatedUserDto.email))
            {
                return BadRequest("Invalid email format");
            }

            // Check if the user exists
            var existingUser = _context.User.FirstOrDefault(u => u.email == updatedUserDto.email);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Update user properties
            existingUser.name = updatedUserDto.name;
            existingUser.email = updatedUserDto.email;
            existingUser.image = updatedUserDto.image;
            existingUser.address = updatedUserDto.address;  
            existingUser.phone = updatedUserDto.phone;  
            // Update other user properties as needed

            // Update the user in the database
            _context.User.Update(existingUser);
            _context.SaveChanges();

            return Ok("User updated successfully");
        }

        



    }
}
