using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Web.Configs;

namespace Web.ViewModels.Home
{
    public class AlbumViewModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }

        public string ImagePath { get; set; }

        [Required] public int ArtistId { get; set; }
    }
}