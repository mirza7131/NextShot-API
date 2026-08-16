using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class UserRoleRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;


        #endregion

        #region Constructor

        public UserRoleRepository(NextShotContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<UserRole> Create(UserRole input)
        {
            using var _uowUserRole = new UnitOfWork<UserRole>();
            var res = _uowUserRole.Repository.Insert(input);
            await _uowUserRole.Save();

            return await Task.FromResult(input);
        }

        public async Task<UserRole> Update(UserRole input)
        {
            using var _uowUserRole = new UnitOfWork<UserRole>();
            _uowUserRole.Repository.Update(input);
            await _uowUserRole.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(UserRole input)
        {
            using var _uowUserRole = new UnitOfWork<UserRole>();
            _uowUserRole.Repository.Update(input);
            await _uowUserRole.Save();
            return true;
        }

        #endregion

        #region Read Operations
        
        public async Task<List<UserRole>> GetALL(Expression<Func<UserRole, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowUserRole = new UnitOfWork<UserRole>();
            return await _uowUserRole.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<UserRole?> GetById(object Id)
        {
            using var _uowUserRole = new UnitOfWork<UserRole>();
            var obj = await _uowUserRole.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
