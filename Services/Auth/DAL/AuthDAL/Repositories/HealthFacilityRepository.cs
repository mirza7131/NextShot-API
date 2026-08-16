using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class HealthFacilityRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;
        private readonly UnitOfWork<HealthFacility> _uowHealthFacility;


        #endregion

        #region Constructor

        public HealthFacilityRepository(NextShotContext dbContext, UnitOfWork<HealthFacility> uowHealthFacility)
        {
            _dbContext = dbContext;
            _uowHealthFacility = uowHealthFacility;
        }

        #endregion

        #region CUD Operations

        public async Task<HealthFacility> Create(HealthFacility input)
        {
            using var _uowHealthFacility = new UnitOfWork<HealthFacility>();
            var res = _uowHealthFacility.Repository.Insert(input);
            await _uowHealthFacility.Save();

            return await Task.FromResult(input);
        }

        public async Task<HealthFacility> Update(HealthFacility input)
        {
            using var _uowHealthFacility = new UnitOfWork<HealthFacility>();
            _uowHealthFacility.Repository.Update(input);
            await _uowHealthFacility.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(HealthFacility input)
        {
            using var _uowHealthFacility = new UnitOfWork<HealthFacility>();
            _uowHealthFacility.Repository.Update(input);
            await _uowHealthFacility.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<HealthFacility>> GetALL(Expression<Func<HealthFacility, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowHealthFacility = new UnitOfWork<HealthFacility>();
            return await _uowHealthFacility.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<HealthFacility?> GetById(object Id)
        {
            using var _uowHealthFacility = new UnitOfWork<HealthFacility>();
            var obj = await _uowHealthFacility.Repository.GetById(Id);
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
