using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class TehsilRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;
        private readonly UnitOfWork<Tehsil> _uowTehsil;


        #endregion

        #region Constructor

        public TehsilRepository(GamaContext dbContext, UnitOfWork<Tehsil> uowTehsil)
        {
            _dbContext = dbContext;
            _uowTehsil = uowTehsil;
        }

        #endregion

        #region CUD Operations

        public async Task<Tehsil> Create(Tehsil input)
        {
            using var _uowTehsil = new UnitOfWork<Tehsil>();
            var res = _uowTehsil.Repository.Insert(input);
            await _uowTehsil.Save();

            return await Task.FromResult(input);
        }

        public async Task<Tehsil> Update(Tehsil input)
        {
            using var _uowTehsil = new UnitOfWork<Tehsil>();
            _uowTehsil.Repository.Update(input);
            await _uowTehsil.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(Tehsil input)
        {
            using var _uowHealthFacility = new UnitOfWork<Tehsil>();
            _uowTehsil.Repository.Update(input);
            await _uowTehsil.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<Tehsil>> GetALL(Expression<Func<Tehsil, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowTehsil = new UnitOfWork<Tehsil>();
            return await _uowTehsil.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<Tehsil?> GetById(object Id)
        {
            using var _uowTehsil = new UnitOfWork<Tehsil>();
            var obj = await _uowTehsil.Repository.GetById(Id);
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
