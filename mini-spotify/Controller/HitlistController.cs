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

        /// <summary>
        /// Initialize the hitlist repository
        /// </summary>
        /// <param name="context"></param>
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
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.ToList();
        }

        /// <summary>
        /// d
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="withRelations"></param>
        /// <returns></returns>
        public List<Hitlist> GetHitlistsByUserId(Guid userId, bool withRelations = false)
        {
            if(userId == Guid.Empty)
            {
                throw new ArgumentException(nameof(userId));
            }

            var query = _hitlistRepository
                            .GetAll()
                            .Where(x => x.UserId == userId);

            if(withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.ToList();
        }

        public Hitlist Get(Guid id, bool withRelations = false)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = _hitlistRepository
                                        .GetAll();
            if (withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Song> GetSongs(ICollection<HitlistSong> hitlistSongs)
        {
            return hitlistSongs.Select(x => x.Song).ToList();
        }

        /// <summary>
        /// Put all chart information together in a string
        /// </summary>
        /// <param name="hitlist"></param>
        /// <returns>Returns the information about the hitlist</returns>
        public string GetHitlistInfo(Hitlist hitlist)
        {
            if (hitlist.Songs == null || hitlist.Songs.Count <= 0)
                return $"Created by {hitlist.User.UserName} at {hitlist.CreatedAt:dd/MM/yyyy} - This hitlist doesn't contain any songs yet";
            else
                return $"Created by {hitlist.User.UserName} at {hitlist.CreatedAt:dd/MM/yyyy} - {GetHitlistSongsCount(hitlist.Songs)}, {GetHitlistDuration(hitlist.Songs)}";
        }

        /// <summary>
        /// Get the song count of a hitlist
        /// </summary>
        /// <param name="songs"></param>
        /// <returns>Returns a string containing the number of songs</returns>
        private string GetHitlistSongsCount(ICollection<HitlistSong> songs)
        {
            return songs.Count > 1 ? $"{songs.Count} songs" : $"{songs.Count} song";
        }

        /// <summary>
        /// Get the total duration of the hitlist
        /// </summary>
        /// <param name="songs"></param>
        /// <returns>Returns the total duration of a hitlist</returns>
        private string GetHitlistDuration(ICollection<HitlistSong> songs)
        {
            TimeSpan total = new TimeSpan(songs.Sum(x => x.Song.Duration.Ticks));

            if (total.Hours > 0)
                return total.Minutes > 0 ? $"{total.Hours} hr {total.Minutes} min" : $"{total.Hours} hr";
            else
                return total.Seconds > 0 ? $"{total.Minutes} min {total.Seconds} sec" : $"{total.Minutes} min";
        }
    }
}
