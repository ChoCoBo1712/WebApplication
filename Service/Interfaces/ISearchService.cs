using System.Collections.Generic;
using Domain.Models;

namespace Service.Interfaces
{
    public interface ISearchService
    {
        List<Song> SearchByCategory(int category, string search);
    }
}