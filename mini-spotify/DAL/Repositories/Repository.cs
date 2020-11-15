using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mini_spotify.DAL.Repositories
{
    /// <summary>
    /// Generic Repository for the most basic actions to interact with an entity.
    /// The purpose of this generic class is to ensure that the user has limited access towards our data.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;

        /// <summary>
        /// Initializes a new instance of this generic <see cref="Repository{T}"/> to interact with an entity.
        /// </summary>
        /// <param name="context"></param>
        public Repository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds a entity to the datatable.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Finds one entity with the given predicate. Similar to <see cref="System.Linq"/>.
        /// </summary>
        /// <param name="predicate">The Expression used to find an entity</param>
        /// <returns>The entity based on the Expression</returns>
        public virtual T FindOneBy(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Finds one or multiple entities based on the given predicate. Similar to <see cref="System.Linq"/>.
        /// </summary>
        /// <param name="predicate">The Expression used to find one or more entities</param>
        /// <returns>An <see cref="IQueryable{T}"/> of The entities based on the Expression</returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Determines wether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>true, if any elements in the source sequence pass the test in the specified predicate, false otherwise.</returns>
        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        /// <summary>
        /// Gets all entities of the database table.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> with all the entities.</returns>
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The updated entity</param>
        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Removes the given entities.
        /// </summary>
        /// <param name="entities">The entities to be removed</param>
        public virtual void RemoveRange(params T[] entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Finds an entity based on the id.
        /// </summary>
        /// <typeparam name="TKey">The type of the id </typeparam>
        /// <param name="id"></param>
        /// <returns>The entity found, or null</returns>
        public T Find<TKey>(TKey id)
        {
            return DbSet.Find(id);
        }


        /// <summary>
        /// Adds the given entities to the datatable.
        /// </summary>
        /// <param name="entities">the entities to be added</param>
        public void AddRange(params T[] entities)
        {
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// Executes the changes made by the other methods of this class. 
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
