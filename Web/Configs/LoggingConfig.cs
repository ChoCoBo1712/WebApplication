namespace Web.Configs
{
    public class LoggingConfig
    {
        public const string SectionName = "Logging";
        
        public string CommonLogFilePath { get; set; }
        public string ErrorLogFilePath { get; set; }
    }
}