namespace Chat.API.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User UserId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
