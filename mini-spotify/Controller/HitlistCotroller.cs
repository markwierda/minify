using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mini_spotify.Controller
{
    public class HitlistCotroller
    {
        public string GetHitlistSongsCount(List<Song> hitlist)
        {
            return hitlist.Count > 1 ? $"{hitlist.Count} songs" : $"{hitlist.Count} song";
        }

        public string GetHitlistDuration(List<Song> hitlist)
        {
            TimeSpan total = new TimeSpan(hitlist.Sum(x => x.Duration.Ticks));
            return total.Hours > 0 ? $"{total.Hours} hr {total.Minutes} min" : $"{total.Minutes} min";
        }
    }
}
