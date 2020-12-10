namespace Web.Configs
{
    public class MailConfig
    {
        public const string SectionName = "Mailing";
        
        public static string Sender { get; set; }
        public static string SmtpServer { get; set; }
        public static int SmtpPort { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
    }
}