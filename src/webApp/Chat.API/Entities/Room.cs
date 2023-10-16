namespace Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public IList<string> Admins { get; set; }
        public IList<string> BlackList { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
