using Microsoft.EntityFrameworkCore;

using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
