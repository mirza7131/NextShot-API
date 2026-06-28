using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class UserLogRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;


        #endregion

        #region Constructor

        public UserLogRepository(GamaContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<UserLog> Create(UserLog input)
        {
            using var _uowUserLog = new UnitOfWork<UserLog>();
            var res = _uowUserLog.Repository.Insert(input);
            await _uowUserLog.Save();
            return await Task.FromResult(input);
        }

        public async Task<UserLog> Update(UserLog input)
        {
            using var _uowUserLog = new UnitOfWork<UserLog>();
            _uowUserLog.Repository.Update(input);
            await _uowUserLog.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(UserLog input)
        {
            using var _uowUserLog = new UnitOfWork<UserLog>();
            _uowUserLog.Repository.Update(input);
            await _uowUserLog.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<UserLog>> GetALL(Expression<Func<UserLog, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowUserLog = new UnitOfWork<UserLog>();
            return await _uowUserLog.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<UserLog?> GetById(object Id)
        {
            using var _uowUserLog = new UnitOfWork<UserLog>();
            var obj = await _uowUserLog.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
