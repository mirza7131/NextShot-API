using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class UserRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;
        private readonly UnitOfWork<User> _uowUser;


        #endregion

        #region Constructor

        public UserRepository(GamaContext dbContext, UnitOfWork<User> uowUser)
        {
            _dbContext = dbContext;
            _uowUser = uowUser;
        }

        #endregion

        #region CUD Operations

        public async Task<User> Create(User input)
        {
            using var _uowUser = new UnitOfWork<User>();
            var res = _uowUser.Repository.Insert(input);
            await _uowUser.Save();

            return await Task.FromResult(input);
        }

        public async Task<User> Update(User input)
        {
            using var _uowUser = new UnitOfWork<User>();
            _uowUser.Repository.Update(input);
            await _uowUser.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(User input)
        {
            using var _uowUser = new UnitOfWork<User>();
            _uowUser.Repository.Update(input);
            await _uowUser.Save();
            return true;
        }

        #endregion

        #region Read Operations
        public async Task<List<User>> GetALL(Expression<Func<User, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowUser = new UnitOfWork<User>();
            return await _uowUser.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<User?> GetById(object Id)
        {
            using var _uowUser = new UnitOfWork<User>();
            var obj = await _uowUser.Repository.GetById(Id);
            return obj;
        }

        public async Task<User?> GetByCnic(string cnic)
        {
            using var _uowUser = new UnitOfWork<User>();
            var obj = await _uowUser.Repository.GetALL(x => x.Cnic == cnic).Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.UserRoles).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<User?> GetByIdWithUserRoleDetails(object Id)
        {
            using var _uowRole = new UnitOfWork<User>();
            var obj = await _uowRole.Repository.GetALL(x => x.UserId.ToString() == Id.ToString()).Include(x => x.UserRoles).FirstOrDefaultAsync();
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
