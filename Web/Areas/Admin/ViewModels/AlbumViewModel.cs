using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Web.Configs;

namespace Web.Areas.Admin.ViewModels
{
    public class AlbumViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Display(Name = "Album image")]
        [AllowedExtensions.AllowedExtensionsAttribute(new [] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        [Required] public int ArtistId { get; set; }
    }
}