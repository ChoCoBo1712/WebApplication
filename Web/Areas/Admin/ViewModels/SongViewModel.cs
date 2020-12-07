using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Web.Configs;

namespace Web.Areas.Admin.ViewModels
{
    public class SongViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }

        [Required] public int AlbumId { get; set; }
        
        [Display(Name = "Song file")]
        [AllowedExtensions.AllowedExtensionsAttribute(new [] { ".mp3", ".ogg", ".wav" })]
        public IFormFile File { get; set; }
        
        [Required] public List<int> TagIds { get; set; }
    }
}