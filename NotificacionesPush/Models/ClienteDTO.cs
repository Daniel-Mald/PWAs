namespace NotificacionesPush.Models
{
    public class ClienteDTO
    {
        public string Endpoint { get; set; }
        public ClientKeys Keys { get; set; }
    }
    public class ClientKeys
    {
        public string Auth { get; set; }
        public string P256dh { get; set; }

    }
}
