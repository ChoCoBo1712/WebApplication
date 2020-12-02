using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels
{
    public class SongViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }

        [Required] public int AlbumId { get; set; }
        
        [Required] public List<int> TagIds { get; set; }
    }
}