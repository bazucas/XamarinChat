namespace XamarinChat.Model
{
    public class Message
    {
        public int Id { get; set; }
        public int IdChat { get; set; }
        public int IdUser { get; set; }
        public string Msg { get; set; }
        public User User { get; set; }
    }
}