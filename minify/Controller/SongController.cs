using Microsoft.EntityFrameworkCore;
using minify.DAL;
using minify.DAL.Entities;
using minify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace minify.Controller
{
    public class SongController
    {
        private readonly Repository<Song> _repository;

        /// <summary>
        /// Create a sog repository with the context
        /// </summary>
        public SongController()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _repository = new Repository<Song>(context);
        }

        /// <summary>
        /// Get all songs
        /// </summary>
        /// <returns>returns list of songs</returns>
        public List<Song> GetAll()
        {
            var query = _repository.GetAll().AsNoTracking();
            return query.ToList();
        }

        /// <summary>
        /// Gets all songs by name, artist or genre
        /// </summary>
        /// <returns></returns>
        public List<Song> Search(string searchquery)
        {
            var Songs = _repository.GetAll();
            Songs = Songs.Where(s => 
                (s.Name.ToUpper().Contains(searchquery.ToUpper()))||
                (s.Artist.ToUpper().Contains(searchquery.ToUpper()))||
                (s.Genre.ToUpper().Contains(searchquery.ToUpper())));
            return Songs.ToList();
        }

        /// <summary>
        /// Gets a <see cref="Song"/> by the <see cref="Guid"/> id.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> id of the song</param>
        /// <returns>The song found, or null</returns>
        public Song Get(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _repository.Find(id);
        }
    }
}