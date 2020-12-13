using System.Collections.Generic;
using Domain.Models;

namespace Web.ViewModels.Home
{
    public class SearchViewModel
    {
        public string Search { get; set; }
        
        public int Category { get; set; }
        
        public List<Song> Songs { get; set; }
    }
    
    public enum Categories {Audios, Albums, Artists, Tags}
}