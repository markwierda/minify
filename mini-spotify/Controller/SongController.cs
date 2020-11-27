using Microsoft.EntityFrameworkCore;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mini_spotify.Controller
{
    public class SongController
    {
        private readonly Repository<Song> _repository;

        /// <summary>
        /// create a sog repository with the context
        /// </summary>
        public SongController()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _repository = new Repository<Song>(context);
        }

        // the getAll() method of Respository return an IQueryable. This is not what we want to return to the screen, 
        // so we cast it to a list object
        public List<Song> GetAll()
        {
            var query = _repository.GetAll().AsNoTracking();
            return query.ToList();

        }

        /// <summary>
        /// Gets a <see cref="Song"/> by the <see cref="Guid"/> id.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> id of the song</param>
        /// <returns>The song found, or null</returns>
        public Song Get(Guid id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _repository.Find(id);
        }

        /// <summary>
        /// Adds a song to the database
        /// </summary>
        /// <param name="song"></param>
        public void Add(Song song)
        {
            if(song.Id == null)
            {
                throw new ArgumentNullException("id");
            }

            //TO DO: check on values of required properties. 
            // For more information check the Acceptation Criteria.
            _repository.Add(song);
        }
    }
}
