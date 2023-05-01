using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using TestApiTask.Models;
using TestTask.Data;

namespace TestApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesignObjectController : Controller
    {
        private AppDbContext _dbContext;
        public DesignObjectController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/designobjects")]
        public IEnumerable<DesignObject> Get()
        {
            return _dbContext.DesignObjects.ToArray();
        }
        [HttpGet("/designobjects/{designObjectId}")]
        public DesignObject GetById(int designObjectId)
        {
            return _dbContext.DesignObjects.Where(c => c.Id == designObjectId).Single();
        }

        [HttpPost("/designobjects")]
        public int Post([Required] string name, int? parentId, [Required] int projectId, [Required] string code)
        {
            DesignObject obj = new DesignObject()
            {
                Name = name,
                ParentId = parentId,
                ProjectId = projectId,
                Code = GenerateCode(code, parentId)
            };


            _dbContext.DesignObjects.Add(obj);
            _dbContext.SaveChanges();
            return obj.Id;
        }

        [HttpPut("/designobjects/{designObjectId}")]
        public void Put(int designObjectId, string? name, string? code, int? projectId)
        {
            DesignObject obj = _dbContext.DesignObjects.Where(c => c.Id == designObjectId).Single();

            if (!string.IsNullOrEmpty(name))
                obj.Name = name;

            if (projectId != null)
                obj.ProjectId = (int)projectId;

            if (!string.IsNullOrEmpty(code))
                obj.Code = GenerateCode(code, obj.ProjectId);

            _dbContext.SaveChanges();
        }

        [HttpDelete("/designobjects/{designObjectId}")]
        public void Delete(int designObjectId)
        {
            _dbContext.DesignObjects.Where(c => c.Id == designObjectId).ExecuteDelete();
            _dbContext.SaveChanges();
        }

        private string GenerateCode(string srcCode, int? parentId)
        {
            if (parentId != null && parentId != 0)
            {
                DesignObject parent = _dbContext.DesignObjects.Where(c => c.Id == parentId).Single();
                return parent.Code + "." + srcCode;
            }
            else
                return srcCode;
        }
    }
}
