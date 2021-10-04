namespace Assignment4.Entities
{
    public class TaskTag
    {
        public int TagID { get; set; }
        public Tag Tag { get; set; }

        public string Title { get; set; }
        public Task Task { get; set; }
    }
}