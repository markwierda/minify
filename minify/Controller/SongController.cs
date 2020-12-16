using Microsoft.EntityFrameworkCore;
using minify.DAL;
using minify.DAL.Entities;
using minify.DAL.Repositories;
using minify.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace minify.Controller
{
    public class SongController : IController
    {
        private readonly Repository<Song> _repository;

        /// <summary>
        /// Create a sog repository with the context
        /// </summary>
        public SongController()
        {
            _repository = new Repository<Song>(new AppDbContextFactory().CreateDbContext());
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
            var query = _repository.GetAll();

            var likeSearch = $"%{searchquery}%";

            query = query.Where(s =>
                EF.Functions.Like(s.Name.ToUpper(), likeSearch.ToUpper()) ||
                EF.Functions.Like(s.Artist.ToUpper(), likeSearch.ToUpper()) ||
                EF.Functions.Like(s.Genre.ToUpper(), likeSearch.ToUpper())
            );

            return query.ToList();
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