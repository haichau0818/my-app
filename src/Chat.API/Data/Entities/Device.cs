namespace Chat.API.Data.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
