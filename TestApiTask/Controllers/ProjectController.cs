using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestApiTask.Models;
using TestTask.Data;

namespace TestApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController :Controller
    {
        private AppDbContext _dbContext;
        public ProjectController(AppDbContext dbContext) { 
            _dbContext = dbContext; 
        }

        [HttpGet("/projects")]
        public IEnumerable<Project> Get()
        {
            return _dbContext.Projects.ToArray();
        }

        [HttpGet("/projects/{projectId}")]
        public Project GetById(int projectId)
        {
            return _dbContext.Projects.Where(c=>c.Id == projectId).Single();
        }

        [HttpPost("/projects")]
        public int Post([Required] string name, [Required] string code)
        {
            Project project = new Project()
            {
                Name = name,
                Code = code
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return project.Id;
        }

        [HttpPut("/projects/{projectId}")]
        public void Put(int projectId, string? name, string? code)
        {
            Project project = _dbContext.Projects.Where(c => c.Id == projectId).Single();

            if (!string.IsNullOrEmpty(name))
                project.Name = name;

            if (!string.IsNullOrEmpty(code))
                project.Code = code;
            _dbContext.SaveChanges();
        }

        [HttpDelete("/projects/{projectId}")]
        public void Delete(int projectId) {
            _dbContext.Projects.Where(c => c.Id == projectId).ExecuteDelete();
            _dbContext.SaveChanges();
        }
    }
}
