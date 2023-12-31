﻿using Entities.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity>
        where TEntity : BaseEntity
    {
        protected IMongoCollection<TEntity> _collection;

        public GenericRepo(IMongoCollection<TEntity> collection)
        {
            _collection = collection;
        }

        public async Task CreateAsync(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> condition)
            => await _collection.DeleteOneAsync(condition);

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition)
            => await _collection.Find(condition).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task<TEntity?> FindByIdAsync(string id)
            => await _collection.Find(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();
    }
}
