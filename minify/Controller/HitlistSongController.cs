using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;

using System;

namespace mini_spotify.Controller
{
    public class HitlistSongController
    {
        private readonly Repository<HitlistSong> _hitlistSongRepository;
        private readonly Repository<Hitlist> _hitlistRepository;
        private readonly Repository<Song> _songRepository;

        public HitlistSongController(AppDbContext context = null)
        {
            context ??= new AppDbContextFactory().CreateDbContext();
            _hitlistSongRepository = new Repository<HitlistSong>(context);
            _hitlistRepository = new Repository<Hitlist>(context);
            _songRepository = new Repository<Song>(context);
        }

        /// <summary>
        /// Adds a song to a hitlist.
        /// </summary>
        /// <param name="hitlistId">The globally unique identifier of the hitlist</param>
        /// <param name="songId">The globally unique identifier of the song</param>
        /// <returns>True, if the song is added to the hitlist, False otherwise.</returns>
        public bool AddSongToHitList(Guid hitlistId, Guid songId)
        {
            // check if the song and the hitlist exists. if it doesn't, return false.
            if (_songRepository.Find(songId) == null || _hitlistRepository.Find(hitlistId) == null)
            {
                return false;
            }

            // check if relation already exists. when it exists, return false;
            if (_hitlistSongRepository.FindOneBy(hs => hs.HitlistId == hitlistId && hs.SongId == songId) != null)
            {
                return false;
            }

            _hitlistSongRepository.Add(new HitlistSong()
            {
                HitlistId = hitlistId,
                SongId = songId,
            });

            _hitlistSongRepository.SaveChanges();

            return true;
        }
    }
}