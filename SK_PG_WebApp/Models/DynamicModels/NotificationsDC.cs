namespace SK_PG_WebApp.Models.DynamicModels
{
    public class NotificationsDC
    {
        public bool isGreatNews { get; set; } = true;
        public bool isBadNews { get; set; } = false;
        public bool isInfoNew { get; set; } = false;
        public string news { get; set; } = string.Empty;
    }
}
