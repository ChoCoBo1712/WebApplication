namespace Repository
{
    public class DbContextOptions
    {
        public const string SectionName = "Project";
        
        public string ConnectionString { get; set; }
    }
}