using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace Simple_Organizer_Backend.Controllers
{
    [Route("/api/items")]
    [ApiController]
    public class ListsController : Controller
    {
        //DB context
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ListsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("items")]
        public IActionResult GetItemsByUserAndListType(int userId, string listType)
        {
            if (userId <= 0 || string.IsNullOrEmpty(listType))
            {
                return BadRequest("Invalid parameters");
            }

            var items = _context.items
                .Where(item => item.user_id == userId && item.list == listType)
                .ToList();

            return Ok(items);
        }

        [HttpGet("list-types")]
        public IActionResult GetListTypes()
        {
            var listTypes = _configuration.GetSection("listTypes").Get<string[]>();

            if (listTypes == null)
            {

                return NotFound("List types not found in configuration.");
            }

            return Ok(listTypes);
        }

        [HttpPost("add-item")]
        public IActionResult AddItem([FromBody] Item newItem)
        {
            if (newItem == null)
            {
                return BadRequest("Invalid item data");
            }

            try
            {
                // Add the new item to the database
                _context.items.Add(newItem);
                _context.SaveChanges();

                return Ok("Item added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
