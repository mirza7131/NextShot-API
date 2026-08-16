using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class AuditLogRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;


        #endregion

        #region Constructor

        public AuditLogRepository(NextShotContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<AuditLog> Create(AuditLog input)
        {
            using var _uowAuditLog = new UnitOfWork<AuditLog>();
            var res = _uowAuditLog.Repository.Insert(input);
            await _uowAuditLog.Save();

            return await Task.FromResult(input);
        }

        public async Task<AuditLog> Update(AuditLog input)
        {
            using var _uowAuditLog = new UnitOfWork<AuditLog>();
            _uowAuditLog.Repository.Update(input);
            await _uowAuditLog.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(AuditLog input)
        {
            using var _uowAuditLog = new UnitOfWork<AuditLog>();
            _uowAuditLog.Repository.Update(input);
            await _uowAuditLog.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<AuditLog>> GetALL(Expression<Func<AuditLog, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowAuditLog = new UnitOfWork<AuditLog>();
            return await _uowAuditLog.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<AuditLog?> GetById(object Id)
        {
            using var _uowAuditLog = new UnitOfWork<AuditLog>();
            var obj = await _uowAuditLog.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
