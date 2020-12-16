using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using minify.DAL;
using minify.DAL.Entities;
using minify.DAL.Repositories;
using minify.Interfaces;
using minify.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace minify.Controller
{
    public class HitlistController : IController
    {
        private readonly Repository<Hitlist> _hitlistRepository;
        private readonly Repository<HitlistSong> _hitlistSongRepository;

        /// <summary>
        /// Create a hitlist repository with the context
        /// </summary>
        public HitlistController()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _hitlistRepository = new Repository<Hitlist>(context);
            _hitlistSongRepository = new Repository<HitlistSong>(context);
        }

        /// <summary>
        /// Gets all the hitlists available.
        /// </summary>
        /// <param name="withRelations">If true, all the songs and the user data will be included in the list, false otherwise</param>
        /// <returns>A list with all the hitlists</returns>
        public List<Hitlist> GetAll(bool withRelations = false)
        {
            var query = _hitlistRepository.GetAll();

            if (withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.ToList();
        }

        /// <summary>
        /// Get hitlist by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="withRelations"></param>
        /// <returns></returns>
        public List<Hitlist> GetHitlistsByUserId(Guid userId, bool withRelations = false)
        {
            if (Utility.GuidIsNullOrEmpty(userId))
            {
                throw new ArgumentException(nameof(userId));
            }

            var query = _hitlistRepository
                            .GetAll()
                            .Where(x => x.UserId == userId);

            if (withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.ToList();
        }

        /// <summary>
        /// Get hitlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="withRelations"></param>
        /// <returns></returns>
        public Hitlist Get(Guid id, bool withRelations = false)
        {
            if (Utility.GuidIsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = _hitlistRepository.GetAll();

            if (withRelations)
            {
                query = query
                    .Include(hl => hl.User)
                    .Include(hl => hl.Songs)
                        .ThenInclude(s => s.Song);
            }

            return query.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Adds a hitlist to the database
        /// </summary>
        /// <param name="hitlist"></param>
        public Hitlist Add(Hitlist hitlist, List<Song> songs = null)
        {
            if (hitlist.Id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (songs != null && songs.Count > 0)
            {
                List<HitlistSong> hitlistSongs = CreateHitlistSongList(hitlist, songs.ToArray());

                if (hitlist.Songs == null)
                {
                    hitlist.Songs = hitlistSongs;
                }
                else
                {
                    hitlist.Songs.Union(hitlistSongs);
                }
            }
            try
            {
                _hitlistRepository.Add(hitlist);
                _hitlistRepository.SaveChanges();

                return hitlist;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return null;
            }

        }

        public bool AddSongsToHitlist(Hitlist hitlist, params Song[] songs)
        {
            if (songs == null || songs.Length == 0)
            {
                return false; 
            }
                
            List<HitlistSong> hitlistSongs = CreateHitlistSongList(hitlist, songs);

            try
            {
                foreach(var hitlistSong in hitlistSongs)
                {
                    _hitlistSongRepository.Add(hitlistSong);
                }

                int ammount = _hitlistSongRepository.SaveChanges();

                return ammount > 0;
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
                return false;
            }
        }

        private List<HitlistSong> CreateHitlistSongList(Hitlist hitlist, params Song[] songs)
        {
            List<HitlistSong> hitlistSongs = new List<HitlistSong>();

            foreach (Song song in songs)
            {
                if (!Utility.GuidIsNullOrEmpty(song.Id) && !hitlist.Songs.Any(x => x.SongId == song.Id))
                {
                    hitlistSongs.Add(new HitlistSong() { HitlistId = hitlist.Id, SongId = song.Id });
                }
            }

            return hitlistSongs;
        }
        /// <summary>
        /// Creates a list with the hitlist's songs
        /// </summary>
        /// <param name="hitlistSongs"></param>
        /// <returns>Hitlist songs</returns>
        public void Delete(Hitlist hitlist)
        {
            if (hitlist.Id == null)
                throw new ArgumentNullException("id");
            if (AppData.UserId == hitlist.UserId)
            {
                _hitlistRepository.Remove(hitlist);
                _hitlistRepository.SaveChanges();
            }
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

        /// <summary>
        /// Validate title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Returns true if not empty and false if empty</returns>
        public bool Validation_Title(string title)
        {
            return !title.IsNullOrEmpty();
        }

        /// <summary>
        /// Validate description
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Returns true if lenght is not greater than 140 and false if greater than 140</returns>
        public bool Validation_Description(string description)
        {
            return description.Length <= 140;
        }
    }
}