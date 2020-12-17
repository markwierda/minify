using Microsoft.EntityFrameworkCore;
using minify.DAL;
using minify.DAL.Entities;
using minify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace minify.Controller
{
    public class StreamroomController
    {
        private readonly Repository<Streamroom> _repository;

        /// <summary>
        /// Create a streamroom repository with the context
        /// </summary>
        public StreamroomController()
        {
            _repository = new Repository<Streamroom>(new AppDbContextFactory().CreateDbContext());
        }

        /// <summary>
        /// Get streamroom by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="withRelations"></param>
        /// <returns></returns>
        public Streamroom Get(Guid id, bool withRelations = false)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = _repository.GetAll();

            if (withRelations)
            {
                query = query
                    .Include(s => s.Song)
                    .Include(s => s.Hitlist)
                        .ThenInclude(h => h.User)
                    .Include(s => s.Hitlist)
                        .ThenInclude(h => h.Songs)
                            .ThenInclude(hs => hs.Song);
            }

            return query.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets all the streamrooms available.
        /// </summary>
        /// <param name="withRelations">If true, the song and the user data will be included in the list, false otherwise</param>
        /// <returns>A list with all the hitlists</returns>
        public List<Streamroom> GetAll(bool withRelations = false)
        {
            var query = _repository.GetAll();

            if (withRelations)
            {
                query = query
                    .Include(x => x.Song)
                    .Include(x => x.Hitlist);
            }

            return query.ToList();
        }

        /// <summary>
        /// Get streamroom by hitlist id
        /// </summary>
        /// <param name="hitlistId"></param>
        /// <param name="withRelations"></param>
        /// <returns></returns>
        public Streamroom GetStreamroomByHitlistId(Guid hitlistId, bool withRelations = false)
        {
            if (hitlistId == Guid.Empty)
            {
                throw new ArgumentException(nameof(hitlistId));
            }

            var query = _repository
                            .GetAll()
                            .Where(x => x.HitlistId == hitlistId);

            if (withRelations)
            {
                query = query
                    .Include(s => s.Song)
                    .Include(s => s.Hitlist);
            }

            return query.Where(x => x.HitlistId == hitlistId).FirstOrDefault();
        }

        /// <summary>
        /// Adds a streamroom to the database
        /// </summary>
        /// <param name="streamroom"></param>
        public void Add(Streamroom streamroom)
        {
            if (streamroom.Id == null)
                throw new ArgumentNullException("id");

            _repository.Add(streamroom);
            _repository.SaveChanges();
        }

        /// <summary>
        /// Adds a streamroom to the database
        /// </summary>
        /// <param name="streamroom"></param>
        public void Update(Streamroom streamroom)
        {
            if (streamroom.Id == null)
                throw new ArgumentNullException("id");

            _repository.Update(streamroom);
            _repository.SaveChanges();
        }

        public void Pause(Streamroom streamroom)
        {
            if (streamroom.Id == null)
                throw new ArgumentNullException("id");

            streamroom.IsPaused = true;
            Update(streamroom);
        }

        public void Play(Streamroom streamroom)
        {
            if (streamroom.Id == null)
                throw new ArgumentNullException("id");

            streamroom.IsPaused = false;
            Update(streamroom);
        }

        public bool IsPaused(Streamroom streamroom)
        {
            if (streamroom.Id == null)
                throw new ArgumentNullException("id");

            return Get(streamroom.Id).IsPaused;
        }

        /// <summary>
        /// Check if streamroom with given hitlist id already exist
        /// </summary>
        /// <param name="hitlistId"></param>
        /// <returns>Returns true ot false</returns>
        public bool DoesRoomAlreadyExist(Guid hitlistId)
        {
            return GetStreamroomByHitlistId(hitlistId) != null;
        }
    }
}