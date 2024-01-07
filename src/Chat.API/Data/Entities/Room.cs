namespace Chat.API.Data.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public RoomMember RoomMemberId { get; set; }
        public Message MessageId { get; set; }
    }
}
