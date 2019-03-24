namespace XamarinChat.Model.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int Id_chat { get; set; }
        public int Id_usuario { get; set; }
        public string Mensagem { get; set; }
        public UserDto Usuario { get; set; }
    }
}