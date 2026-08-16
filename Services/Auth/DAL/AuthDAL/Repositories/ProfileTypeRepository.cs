using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class ProfileTypeRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;


        #endregion

        #region Constructor

        public ProfileTypeRepository(NextShotContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<ProfileType> Create(ProfileType input)
        {
            using var _uowProfileType = new UnitOfWork<ProfileType>();
            var res = _uowProfileType.Repository.Insert(input);
            await _uowProfileType.Save();

            return await Task.FromResult(input);
        }

        public async Task<ProfileType> Update(ProfileType input)
        {
            using var _uowProfileType = new UnitOfWork<ProfileType>();
            _uowProfileType.Repository.Update(input);
            await _uowProfileType.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(ProfileType input)
        {
            using var _uowProfileType = new UnitOfWork<ProfileType>();
            _uowProfileType.Repository.Update(input);
            await _uowProfileType.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ProfileType>> GetALL(Expression<Func<ProfileType, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowProfileType = new UnitOfWork<ProfileType>();
            return await _uowProfileType.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<ProfileType?> GetById(object Id)
        {
            using var _uowProfileType = new UnitOfWork<ProfileType>();
            var obj = await _uowProfileType.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods

        


        #endregion
    }
}
