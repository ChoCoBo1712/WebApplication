using System.ComponentModel.DataAnnotations;

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