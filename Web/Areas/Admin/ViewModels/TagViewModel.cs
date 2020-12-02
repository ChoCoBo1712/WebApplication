using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels
{
    public class TagViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }
    }
}