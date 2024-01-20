using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Temp.DAL.Data;
using Temp.DAL.UnitOfWorks;
using Temp.Models.Entities.GenericEntities;
using Temp.Models.Enums;
using Temp.Models.Inpute;
using Temp.Models.Output;

namespace Temp.DAL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, IEntityBase
    {
        #region Fields
        protected readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly DbSet<T> _repository;
        private readonly IUnitOfWork _unityOfWork;
        #endregion Fields

        #region Constructor
        public GenericRepo(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Constructor

        #region IQueryable
        public IQueryable<T> Queryable()
        {
            IQueryable<T> query = _applicationDbContext.Set<T>().AsSplitQuery().AsNoTracking();
            return query;
        }
        #endregion IQueryable

        #region Get Methods

        /* Get */
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            query = query.AsQueryable().Where(expression);
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (order == OrderByEnum.Asc)
                query = query.AsQueryable().Where(expression).OrderBy(orderBy);
            else
                query = query.AsQueryable().Where(expression).OrderByDescending(orderBy);

            return await query.ToListAsync();
        }

        /* Find */
        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            query = query.Where(expression);

            return await query.FirstOrDefaultAsync();
        }

        /*  GetById */
        public async Task<T> GetByIdAsync(object id, params string[] includes)
        {
            DbSet<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query.Include(include);
            }

            return await query.FindAsync(id);
        }
        //public async Task<T> GetById(object id)
        //{
        //    return await _repository.FindAsync(id);
        //}
        /* GetAll */
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes)
        {
            DbSet<T> query = _applicationDbContext.Set<T>();
            if (includes.Any())
            {
                foreach (var include in includes)
                    query.Include(include);
            }

            if (order == OrderByEnum.Asc)
                return await query.AsQueryable().OrderBy(orderBy).ToListAsync();

            return await query.AsQueryable().OrderByDescending(orderBy).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.Where(expression).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (order == OrderByEnum.Asc)
                return await query.Where(expression).OrderBy(orderBy).ToListAsync();

            return await query.Where(expression).OrderByDescending(orderBy).ToListAsync();
        }

        #endregion Get Methods

        #region Add Methods
        /* Add */
        public async Task AddAsync(T entity)
        {
            try
            {
                await TryAuditEntity(entity);
            }
            finally
            {
                await _applicationDbContext.Set<T>().AddAsync(entity);
            }

        }
        public async Task<T> Add([NotNull] T entity, bool saveChange = false)
        {
            if (saveChange)
            {
                await AddAsync(entity);
                await Detached(entity);
            }
            return entity;
        }

        public async Task<T> Update([NotNull] T entity, bool saveChange = false)
        {
            if (entity == null) return entity;

            if (saveChange)
            {
                await UpdateAsync(entity);
                await Detached(entity);
            }
            return entity;
        }
        public async Task<T> Detached([NotNull] T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task AddRangeAsync(List<T> entities)
        {
            try
            {
                entities.ForEach(async entity =>
                {
                    await TryAuditEntity(entity);
                });
            }
            finally
            {
                await _applicationDbContext.Set<T>().AddRangeAsync(entities);
            }

        }
        #endregion Add Methods

        #region Update Methods
        /* Update */
        public async Task UpdateAsync(T entity)
        {
            try
            {
                await TryAuditEntity(entity);
            }
            finally
            {
                _applicationDbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (T entity in entities)
            {
                try
                {
                    await TryAuditEntity(entity);
                }
                finally
                {
                    await UpdateAsync(entity);
                }
            }
        }
        #endregion Update Methods

        #region Remove Methods
        /* Remove */
        public async Task RemoveAsync(object id, bool softDelete = true)
        {
            T entity = await GetByIdAsync(id);
            if (softDelete)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);

            }
            else
            {
                PhiscalEntityRemove(entity);
            }


        }

        public async Task RemoveRangeAsync(List<Guid> ids, bool softDelete = true)
        {
            List<T> _entities = new();

            foreach (object id in ids)
            {
                T entity = await GetByIdAsync(id);

                if (softDelete)
                    entity.IsDeleted = true;

                _entities.Add(entity);
            }

            if (softDelete)
                await UpdateRangeAsync(_entities);
            else
                PhiscalEntitiesRemove(_entities);
        }

        public async Task RemoveAsync(T entity, bool softDelete = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);
            }
            else
            {
                PhiscalEntityRemove(entity);
            }
        }

        public async Task RemoveRangeAsync(List<T> entities, bool softDelete = true)
        {
            if (softDelete)
            {
                foreach (T entity in entities)
                    entity.IsDeleted = true;

                await UpdateRangeAsync(entities);
            }
            else
            {
                PhiscalEntitiesRemove(entities);
            }

        }
        #endregion Remove Methods

        #region Paginate Methods
        /* Paginate */
        public async Task<PageOutput<List<T>>> Paginate(PagerInput paginateInput, Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>().AsNoTracking();

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            PageOutput<List<T>> result = new()
            {
                TotalRecords = await query.CountAsync()
            };

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            result.PageSize = result.TotalRecords > paginateInput.PageSize ? paginateInput.PageSize : result.TotalRecords;
            result.PageIndex = paginateInput.PageIndex;

            int skipedItemsCount = 0;

            if (paginateInput.PageIndex > 1)
                skipedItemsCount = paginateInput.PageSize * (paginateInput.PageIndex - 1);

            result.Data = await query.Skip(skipedItemsCount).Take(paginateInput.PageSize).ToListAsync();

            return result;
        }

        public async Task<PageOutput<List<T>>> Paginate(PagerInput paginateInput, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, OrderByEnum order = OrderByEnum.Asc, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            PageOutput<List<T>> result = new()
            {
                TotalRecords = await query.Where(expression).CountAsync(),
            };

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            result.PageSize = result.TotalRecords > paginateInput.PageSize ? paginateInput.PageSize : result.TotalRecords;
            result.PageIndex = paginateInput.PageIndex;
            int skipedItemsCount = 0;

            if (paginateInput.PageIndex > 1)
                skipedItemsCount = paginateInput.PageSize * (paginateInput.PageIndex - 1);

            if (order == OrderByEnum.Asc)
                result.Data = await query.Where(expression).OrderBy(orderBy).Skip(skipedItemsCount).Take(paginateInput.PageSize).ToListAsync();
            else
                result.Data = await query.Where(expression).OrderByDescending(orderBy).Skip(skipedItemsCount).Take(paginateInput.PageSize).ToListAsync();

            return result;
        }




        #endregion  Paginate Methods

        #region Count Methods
        /*  Count */
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            query = query.AsQueryable().Where(expression);
            int result = query.Count();
            return await Task.FromResult(result);
        }

        public async Task<int> CountAsync()
        {
            int result = _applicationDbContext.Set<T>().Count();
            return await Task.FromResult(result);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            int count = await query.Where(expression).CountAsync();
            return count;
        }
        #endregion Count Methods

        #region Private Methods

        #region Audit Entity 
        private async Task TryAuditEntity(dynamic entity)
        {
            try
            {
                if (!IsAuth())
                    return;

                if (entity?.Id == default(Guid)) //Add 
                {
                    await AddAudit(entity);
                }
                else if (entity?.Id != null)//Edit & SoftDelete
                {
                    if (entity.IsDeleted && entity?.DeletionTime is null)
                    {
                        await DeleteAudit(entity);
                    }
                    else
                    {
                        await UpdateAudit(entity);
                    }
                }
            }
            catch
            {
                return;
            }

            async Task AddAudit(dynamic entity)
            {
                entity.CreationTime = DateTime.Now;
                entity.CreatedById = await GetUserId();
            }

            async Task DeleteAudit(dynamic entity)
            {
                entity.DeletionTime = DateTime.Now;
                entity.DeletedById = await GetUserId();
            }

            async Task UpdateAudit(dynamic entity)
            {
                entity.UpdateTime = DateTime.Now;
                entity.UpdatedById = await GetUserId();
            }
        }


        private bool IsAuth()
        {
            bool isAuth = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(i => i.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value != null;
            return isAuth;
        }

        private async Task<Guid> GetUserId()
        {
            Guid _userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Claims.First(i => i.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            return await Task.FromResult(_userId);
        }
        #endregion Audit Entity

        #region Phiscal Delete
        private void PhiscalEntityRemove(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
        }

        private void PhiscalEntitiesRemove(List<T> entities)
        {
            _applicationDbContext.Set<T>().RemoveRange(entities);
        }


        #endregion  Phiscal Delete

        #endregion Private Metods
    }
}
