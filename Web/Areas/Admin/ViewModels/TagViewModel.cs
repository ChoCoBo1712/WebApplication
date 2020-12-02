using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels
{
    public class TagViewModel
    {
        [Required] public int Id { get; set; }
        
        [MaxLength(12, ErrorMessage = "Maximum length of a tag name is 12 characters")]
        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }
        
        public List<int> SongIds { get; set; }
    }
}