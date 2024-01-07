namespace Chat.API.Data.Entities
{
    public class RoomMember
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public Room RoomId { get; set; }

    }
}
