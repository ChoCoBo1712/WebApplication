using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Web.Configs;

namespace Web.Areas.Admin.ViewModels
{
    public class ArtistViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Display(Name = "Artist image")]
        [AllowedExtensions.AllowedExtensionsAttribute(new [] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        [Required] public string Description { get; set; }
    }
}