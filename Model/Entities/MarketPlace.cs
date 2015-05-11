namespace Model.Entities
{
    public class MarketPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string ClientId { get; set; }
        public string SecretId { get; set; }
        public string RedirectUri { get; set; }
        public string NotificationsCallbackUri { get; set; }
        public string SenderId { get; set; } 
    }
}
