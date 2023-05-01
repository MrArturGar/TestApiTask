using System.ComponentModel.DataAnnotations.Schema;

namespace TestApiTask.Models
{
    public class DocumentSet
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public int MarkId { get; set; }
        public int DesignObjectId { get; set; }

        // навигационное свойство
        public DesignObject DesignObject { get; set; }
        public Mark Mark { get; set; }
    }
}
