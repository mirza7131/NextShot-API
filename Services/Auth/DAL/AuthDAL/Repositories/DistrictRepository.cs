using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class DistrictRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;
        private readonly UnitOfWork<District> _uowDistrict;


        #endregion

        #region Constructor

        public DistrictRepository(NextShotContext dbContext, UnitOfWork<District> uowDistrict)
        {
            _dbContext = dbContext;
            _uowDistrict = uowDistrict;
        }

        #endregion

        #region CUD Operations

        public async Task<District> Create(District input)
        {
            using var _uowDistrict = new UnitOfWork<District>();
            var res = _uowDistrict.Repository.Insert(input);
            await _uowDistrict.Save();

            return await Task.FromResult(input);
        }

        public async Task<District> Update(District input)
        {
            using var _uowDistrict = new UnitOfWork<District>();
            _uowDistrict.Repository.Update(input);
            await _uowDistrict.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(District input)
        {
            using var _uowHealthFacility = new UnitOfWork<District>();
            _uowDistrict.Repository.Update(input);
            await _uowDistrict.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<District>> GetALL(Expression<Func<District, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowDistrict = new UnitOfWork<District>();
            return await _uowDistrict.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<District?> GetById(object Id)
        {
            using var _uowDistrict = new UnitOfWork<District>();
            var obj = await _uowDistrict.Repository.GetById(Id);
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
