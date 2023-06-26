using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    { 
        private readonly ApiContext _dbContext;

        public ReportController(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetReports")]
        public IActionResult GetReports()
        {
            try
            {
                return Ok(_dbContext.Reports.ToList());
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost("AddReport")]
        public async Task<IActionResult> AddReport(AddReport addReport)
        {

               int ranking = await CalculateRanking(addReport);

                 long userId = 0;
                if (addReport.UserId == null)
                {
                  var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Cookie == addReport.Cookie);
                userId = user.Id;
                }
                else
                {
                    userId = addReport.UserId;
                }

            
                var report = new Reports()
                {
                    UserId = userId,
                    Description = addReport.Description,
                    Address = addReport.Address,
                    Lga = addReport.Lga,
                    Lcda = addReport.Lcda,
                    Ranking = ranking,                   
                    Cookie = addReport.Cookie,
                    TimeofEvent = DateTime.Now,
                };

                await _dbContext.Reports.AddAsync(report);
                await _dbContext.SaveChangesAsync();
            return Ok(report);
        }

        private async Task<int> CalculateRanking(AddReport addReport)
        {
            int ranking = 1;
            if (addReport.Images == null || addReport.Images != null && addReport.UserId == null)
            {
                ranking = 2;
            }

            if (addReport.UserId != null)
            {
                ranking = 3;
            }

            if (addReport.Images != null || addReport.UserId != null)
            {
                ranking = 4;
            }

            DateTime currentDate = DateTime.UtcNow.Date.AddDays(-2);
            var eventCoincidences = _dbContext.Reports.Where(x => x.Lcda == addReport.Lcda && x.TimeofEvent >= currentDate).ToList();

            if (eventCoincidences.Count > 2) ranking = 5; 

            return ranking;
        }
    }
}