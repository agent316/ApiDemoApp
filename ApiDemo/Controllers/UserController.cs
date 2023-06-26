using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiContext _dbContext;

        public UserController(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_dbContext.Users.ToList());
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUser addUser)
        {
            try
            {
                var user = new Users()
                {
                    Name = addUser.Name,
                    Email = addUser.Email,
                    Gender = addUser.Gender,
                    PhoneNumber = addUser.PhoneNumber,
                    Password = addUser.Password,
                    Cookie = addUser.Cookie,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                };

                await _dbContext.Users.AddAsync(user);
                var saved = await _dbContext.SaveChangesAsync();


                return Ok(user);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}