using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestApiTask.Models
{
    public class DesignObject
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int ProjectId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        // навигационное свойство
        public DesignObject Parent{ get; set; }
        public Project Project { get; set; }


    }
}
