using Microsoft.EntityFrameworkCore;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mini_spotify.Controller
{
    public class HitlistController
    {
        private Repository<Hitlist> _hitlistRepository;

        public HitlistController(AppDbContext context = null)
        {
            _hitlistRepository = new Repository<Hitlist>(context ?? new AppDbContextFactory().CreateDbContext());
        }

        /// <summary>
        /// Gets all the hitlists available.
        /// </summary>
        /// <param name="withRelations">if true, all the songs and the user data will be included in the list, false otherwise</param>
        /// <returns>A list with all the hitlists</returns>
        public List<Hitlist> GetAll(bool withRelations = false)
        {
            var query = _hitlistRepository
                            .GetAll();

            if(withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs);
            }

            return query.ToList();
        }

        public Hitlist Get(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _hitlistRepository.Find(id);
        }

        /// <summary>
        /// Put all chart information together in a string
        /// </summary>
        /// <param name="hitlist"></param>
        /// <returns>Returns the information about the hitlist</returns>
        public string GetHitlistInfo(List<Song> hitlist)
        {
            if (hitlist.Count > 0)
                return $"Created by User at {DateTime.Today:dd/MM/yyyy} - {GetHitlistSongsCount(hitlist)}, {GetHitlistDuration(hitlist)}";
            else
                return $"Created by User at {DateTime.Today:dd/MM/yyyy} - This hitlist doesn't contain any songs yet";
        }

        /// <summary>
        /// Get the song count of a hitlist
        /// </summary>
        /// <param name="hitlist"></param>
        /// <returns>Returns a string containing the number of songs</returns>
        private string GetHitlistSongsCount(List<Song> hitlist)
        {
            return hitlist.Count > 1 ? $"{hitlist.Count} songs" : $"{hitlist.Count} song";
        }

        /// <summary>
        /// Get the total duration of the hitlist
        /// </summary>
        /// <param name="hitlist"></param>
        /// <returns>Returns the total duration of a hitlist</returns>
        private string GetHitlistDuration(List<Song> hitlist)
        {
            TimeSpan total = new TimeSpan(hitlist.Sum(x => x.Duration.Ticks));
            return total.Hours > 0 ? $"{total.Hours} hr {total.Minutes} min" : $"{total.Minutes} min";
        }
    }
}
