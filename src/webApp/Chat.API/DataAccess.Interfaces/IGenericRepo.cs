using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IGenericRepo<TEntity>
        where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> condition);
        //Task UpdateAsync();
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity?> FindByIdAsync(string id);
    }
}
