using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Temp.DAL.Data;
using Temp.DAL.Interfaces;

namespace Temp.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        protected readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IDbContextTransaction _currentTransaction;
        #endregion Fields

        #region Constructor
        public UnitOfWork(ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Constructor

        #region UOW Methods
        public void Dispose()
        {
            _applicationDbContext.Dispose();

        }
        public IDbContext Context
        {
            get;
            set;
        }
        public async Task DisposeAsync()
        {
            await _applicationDbContext.DisposeAsync();
        }

        public void SaveCahnges()
        {
            _applicationDbContext.SaveChanges();
        }

        public async Task SaveChngesAsync()
        {
            Guid? _userId = GetUserId();

            if (_userId is not null)
                await _applicationDbContext.SaveChangesAsync(_userId.Value);
            else
                await _applicationDbContext.SaveChangesAsync();
        }
        public async Task CommitAsync()
        {
            if (_currentTransaction == null)
                return;
            await SaveChangesAsync();
            await CommitTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
                return;

            await _applicationDbContext.Database.CommitTransactionAsync();
            _currentTransaction = null;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }
        #endregion UOW Methods

        #region Utility Private Methods
        private Guid? GetUserId()
        {
            try
            {
                Guid? _userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Claims.First(i => i.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
                return _userId;
            }
            catch
            {
                return null;
            }
        }
        #endregion Utility Private Methods

        public async Task RollbackAsync()
        {
            if (_currentTransaction == null)
                return;

            await _applicationDbContext.Database.RollbackTransactionAsync();
            _currentTransaction = null;
        }
    }
}
