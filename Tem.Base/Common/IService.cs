using System.Diagnostics.CodeAnalysis;
using Temp.Models.Base;
using Temp.Models.Dtos;

namespace Tem.Base.Common
{
    //public interface IService<TEntity> where TEntity : class
    //{
    //}


    //internal interface IService<TEntity, TKey> where TEntity : class, IEntityBase<TKey> where TKey : struct
    //{
    //}

    public interface IService
    {

    }
    public interface IService<TEntity> : IService where TEntity : class
    {
    }
    public interface IService<TEntity, TKey> : IService where TEntity : class
        where TKey : struct
    {
        Task<TEntity> Save<T>([NotNull] T dto, bool saveChange = false) where T : BaseDto<TKey>;
    }
    public interface IService<TBaseDto, TBaseView, TBaseFilter, TKey> : IService
        where TBaseFilter : BaseFilter
        where TBaseDto : BaseDto<TKey>
        where TBaseView : BaseDto<TKey>
        where TKey : struct
    {
        Task<PageList<TBaseView>> GetAll(TBaseFilter filter);
        Task<TBaseView> GetById(TKey id);
        Task<bool> DeleteById(TKey id);
        Task<TKey> Save([NotNull] TBaseDto dto, bool saveChange = false);
    }
}