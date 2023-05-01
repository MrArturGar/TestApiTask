using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestApiTask.Models;
using TestTask.Data;

namespace TestApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentSetController : Controller
    {
        private AppDbContext _dbContext;
        public DocumentSetController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/documentsets")]
        public IEnumerable<DocumentSet> GetDocumentSets()
        {
            return _dbContext.DocumentationSets.ToArray();
        }

        [HttpGet("/documentsets/{designObjectId}")]
        public IEnumerable<DocumentSet> GetById(int designObjectId)
        {
            return _dbContext.DocumentationSets.Where(c => c.DesignObjectId == designObjectId).ToArray();
        }
        [HttpGet("/documentsets/{documentSetId}")]
        public IEnumerable<DocumentSet> GetByDesignObjectIdAndDocumentSetId(int designObjectId, string documentSetId)
        {
            return _dbContext.DocumentationSets.Where(c => c.Id == documentSetId).ToArray();
        }
        [HttpPost("/documentsets/{designObjectId}")]
        public string Post([Required]int designObjectId, [Required] int markId)
        {
            DocumentSet documentSet = new DocumentSet()
            {
                Number = _dbContext.DocumentationSets.Count(),
                DesignObjectId = designObjectId,
                MarkId = markId
            };

            documentSet.Id = _dbContext.Marks.Where(c => c.Id == markId).Single().ShortName + documentSet.Number;

            _dbContext.DocumentationSets.Add(documentSet);
            _dbContext.SaveChanges();
            return documentSet.Id;
        }

        [HttpPut("/documentsets/{documentSetId}")]
        public void Put(int designObjectId, string documentSetId, int markId)
        {
            DocumentSet doc = _dbContext.DocumentationSets.Where(c => c.Id == documentSetId).Single();

            doc.MarkId = markId;
            doc.Id = _dbContext.Marks.Where(c => c.Id == markId).Single().ShortName + doc.Number;
            _dbContext.SaveChanges();
        }

        [HttpDelete("/documentsets/{documentSetId}")]
        public void Delete(int designObjectId, string documentSetId)
        {
            _dbContext.DocumentationSets.Where(c => c.Id == documentSetId).ExecuteDelete();
            _dbContext.SaveChanges();
        }

        [HttpGet("/documentsets/{documentSetId}/cipher")]
        public string GetCipher(string documentSetId)
        {
            DocumentSet doc = _dbContext.DocumentationSets.Where(c => c.Id == documentSetId).Include(c=>c.DesignObject).ThenInclude(c=>c.Project).Single();

            return $"{doc.DesignObject.Project.Code}-{doc.DesignObject.Code}-{doc.Number}";

        }
    }
}
