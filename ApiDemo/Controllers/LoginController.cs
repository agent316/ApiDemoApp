using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiDemo.Data;


namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _connectionString;

        public LoginController()
        {
            _connectionString = "Data Source=REBORN\\SQLEXPRESS;Initial Catalog=AccidentrptDB;Integrated Security=True;";
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (IsValidCredentials(model.Email, model.Password))
            {
               return Ok(new { Status = "Ok", Message = "Login successful" });
            }

            return Unauthorized("Invalid email or password");
        }

        private bool IsValidCredentials(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

