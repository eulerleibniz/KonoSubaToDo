using System;

namespace KonoAndroid.Model
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        // Storring an enum as string instead of default integer storing
        public ItemCategory ItemCategory { get; set; }
    }

    public enum ItemCategory
    {
        Shopping,
        Work,
        Private
    }

    public class Attachment
    {
        public int Id { get; set; }
        public int ParentTaskId { get; set; }
        public string FilePath { get; set; }
    }
}