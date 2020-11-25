namespace Repository.Models
{
    public class EFUser
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string Userpic { get; set; }
    }
}