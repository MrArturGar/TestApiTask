namespace TestApiTask.Models
{
    /// <summary>
    // Сначала хотел сделать с помощью ENUM, но это не правильно,
    // т.к. нужна расшифровка все равно и возможность масштабирования.
    /// </summary>
    public class Mark
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }
}
