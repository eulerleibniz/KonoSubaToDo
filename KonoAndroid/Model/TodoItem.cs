using Java.Util;
using SQLite;

namespace KonoAndroid.Model
{
    [Table("TodoItem")]
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        public Date DueDate { get; set; }
        public int Priority { get; set; }
        public int Score { get; set; }
        public bool Completed { get; set; }


    }

    public class TodoAttachments
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

    }
}