using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Temp.Models.Entities.GenericEntities;
using Temp.Models.Enums;
using Temp.Models.Inpute;
using Temp.Models.Output;

namespace Temp.DAL.Repositories
{
    public interface IGenericRepo<T> where T : class, IEntityBase
    {
        /* GetById */
        Task<T> GetByIdAsync(object id, params string[] includes);

        IQueryable<T> Queryable();
        /* GetAll */
        Task<List<T>> GetAllAsync(params string[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes);

        /* Get */
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes);

        /* Find */
        Task<T> FindAsync(Expression<Func<T, bool>> expression, params string[] includes);

        /* Add */
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> Add([NotNull] T entity, bool saveChange = false);
        /* Update */
        Task UpdateAsync(T entity);
        Task<T> Update([NotNull] T entity, bool saveChange = false);
        Task UpdateRangeAsync(List<T> entities);

        /* Remove */
        Task RemoveAsync(object id, bool softDelete = true);
        Task RemoveRangeAsync(List<Guid> ids, bool softDelete = true);
        Task RemoveAsync(T entity, bool softDelete = true);
        Task RemoveRangeAsync(List<T> entities, bool softDelete = true);

        /* Paginate */
        Task<PageOutput<List<T>>> Paginate(PagerInput paginateInput, Expression<Func<T, bool>> expression, params string[] includes);
        Task<PageOutput<List<T>>> Paginate(PagerInput paginateInput, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Desc, params string[] includes);

        /*  Count */
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression, params string[] includes);

        //IQueryable<T> Get([NotNull] Expression<Func<T, bool>> predicate, bool asNoTracking = true);

    }
}
