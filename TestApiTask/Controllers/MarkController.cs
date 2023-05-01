using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestApiTask.Models;
using TestTask.Data;

namespace TestApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkController : Controller
    {
        private AppDbContext _dbContext;
        public MarkController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/marks")]
        public IEnumerable<Mark> Get()
        {
            return _dbContext.Marks.ToArray();
        }

        [HttpGet("/marks/{markId}")]
        public Mark GetById(int markId)
        {
            return _dbContext.Marks.Where(c => c.Id == markId).Single();
        }

        [HttpPost("/marks")]
        public int Post([Required] string shortname, [Required] string longname)
        {
            Mark mark = new Mark()
            {
                ShortName = shortname,
                LongName = longname
            };

            _dbContext.Marks.Add(mark); 
            _dbContext.SaveChanges();
            return mark.Id;
        }

        [HttpPut("/marks/{markId}")]
        public void Put(int markId, string? shortname, string? longname)
        {
            Mark mark = _dbContext.Marks.Where(c => c.Id == markId).Single();

            if (!string.IsNullOrEmpty(shortname))
                mark.ShortName = shortname;

            if (!string.IsNullOrEmpty(longname))
                mark.LongName = longname;

            _dbContext.SaveChanges();
        }


        [HttpDelete("/marks/{markId}")]
        public void Delete(int markId)
        {
            _dbContext.Marks.Where(c => c.Id == markId).ExecuteDelete();
            _dbContext.SaveChanges();
        }
    }
}
