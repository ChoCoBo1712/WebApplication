using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Home
{
    public class TagViewModel
    {
        [Required] public int Id { get; set; }
        
        [MaxLength(12, ErrorMessage = "Maximum length of a tag name is 12 characters")]
        [Required] public string Name { get; set; }
        
        [Required] public int SongId { get; set; }
    }
}