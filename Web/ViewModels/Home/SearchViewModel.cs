using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Web.ViewModels.Home
{
    public class SearchViewModel
    {
        public string Search { get; set; }
        
        public int Category { get; set; }
        
        public List<Song> Songs { get; set; }
    }
    
    public enum Categories {Songs, Albums, Artists, Tags}
}