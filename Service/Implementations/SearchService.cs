using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Service.Interfaces;

namespace Service.Implementations
{
    public class SearchService : ISearchService
    {
        private IDataManager dataManager;

        public SearchService(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public List<Song> SearchByCategory(int category, string search)
        {
            var songs = new List<Song>();
            switch (category)
            {
                case 0:
                    songs = dataManager.SongRepository.GetAll().Where(t => 
                        t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                    break;
                case 1:
                    songs = dataManager.SongRepository.GetAll().Where(t => 
                        t.Album.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                    break;
                case 2:
                    songs = dataManager.SongRepository.GetAll().Where(t => 
                        t.Album.Artist.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                    break;
                case 3:
                    songs = dataManager.SongRepository.GetAll().Where(t => 
                        t.Tags.Any(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase))).OrderBy(t => t.Name).ToList();
                    break;
            }

            return songs;
        }
    }
}