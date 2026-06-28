using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class DivisionRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;
        private readonly UnitOfWork<Division> _uowDivision;


        #endregion

        #region Constructor

        public DivisionRepository(GamaContext dbContext, UnitOfWork<Division> uowDivision)
        {
            _dbContext = dbContext;
            _uowDivision = uowDivision;
        }

        #endregion

        #region CUD Operations

        public async Task<Division> Create(Division input)
        {
            using var _uowDivision = new UnitOfWork<Division>();
            var res = _uowDivision.Repository.Insert(input);
            await _uowDivision.Save();

            return await Task.FromResult(input);
        }

        public async Task<Division> Update(Division input)
        {
            using var _uowDivision = new UnitOfWork<Division>();
            _uowDivision.Repository.Update(input);
            await _uowDivision.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(Division input)
        {
            using var _uowHealthFacility = new UnitOfWork<Division>();
            _uowDivision.Repository.Update(input);
            await _uowDivision.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<Division>> GetALL(Expression<Func<Division, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowDivision = new UnitOfWork<Division>();
            return await _uowDivision.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<Division?> GetById(object Id)
        {
            using var _uowDivision = new UnitOfWork<Division>();
            var obj = await _uowDivision.Repository.GetById(Id);
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
