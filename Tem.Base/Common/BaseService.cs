using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Temp.DAL.Repositories;
using Temp.DAL.UnitOfWorks;
using Temp.Models.Dtos;
using Temp.Models.Entities.GenericEntities;

namespace Tem.Base.Common
{
    public abstract class BaseService
    {
        //public readonly ICurrentUserAccessor _currentUserAccessor;

        public readonly IServiceProvider ServiceProvider;
        protected BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            //_currentUserAccessor = serviceProvider.GetRequiredService<ICurrentUserAccessor>();
        }

        //protected long CurrentUserId
        //{
        //    get
        //    {
        //        var userId = _currentUserAccessor?.User?.Id;
        //        return userId ?? throw new AuthenticationException(); ;
        //    }
        //}

        protected T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }


        //protected bool IsInRole(params long[] permissionIds)
        //{
        //    return _currentUserAccessor.IsInRole(permissionIds);
        //}
    }
    public abstract class BaseService<TEntity> : BaseService, IService<TEntity> where TEntity : class, IEntityBase
    {
        protected readonly IGenericRepo<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;
        public readonly IMapper Mapper;

        protected BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Repository = serviceProvider.GetRequiredService<IGenericRepo<TEntity>>();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            Mapper = serviceProvider.GetRequiredService<IMapper>();
        }



    }


    public abstract class BaseService<TEntity, TKey> : BaseService, IService<TEntity, TKey> where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        protected readonly IGenericRepo<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;
        public readonly IMapper Mapper;

        protected BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Repository = serviceProvider.GetRequiredService<IGenericRepo<TEntity>>();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            Mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        protected T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        public virtual void ValidatedData<T>([NotNull] T dto) where T : BaseDto<TKey>
        {

        }

        public async Task<TEntity> Save<T>([NotNull] T dto, bool saveChange = false) where T : BaseDto<TKey>
        {
            ValidatedData<T>(dto);
            var entity = Mapper.Map<TEntity>(dto);
            if (dto.Id == null || dto.Id.Equals(default) || entity.Id.Equals(default) || entity.Id.Equals(obj: "0"))
            {
                await Repository.Add(entity, saveChange);
                return entity;
            }
            else
            {
                //var oldEntity = await Repository.GetById(dto.Id);
                var oldEntity = await Repository.GetByIdAsync(dto.Id);
                //entity.CopyPropertiesTo(oldEntity);
                await Repository.Update(oldEntity, saveChange);
                return oldEntity;
            }

        }
    }


}
