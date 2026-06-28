using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Repositories
{
    public class ProfileRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;


        #endregion

        #region Constructor

        public ProfileRepository(GamaContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<Profile> Create(Profile input)
        {
            using var _uowProfile = new UnitOfWork<Profile>();
            var res = _uowProfile.Repository.Insert(input);
            await _uowProfile.Save();

            return await Task.FromResult(input);
        }

        public async Task<Profile> Update(Profile input)
        {
            using var _uowProfile = new UnitOfWork<Profile>();
            _uowProfile.Repository.Update(input);
            await _uowProfile.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(Profile input)
        {
            using var _uowProfile = new UnitOfWork<Profile>();
            _uowProfile.Repository.Update(input);
            await _uowProfile.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<Profile>> GetALL(Expression<Func<Profile, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowProfile = new UnitOfWork<Profile>();
            return await _uowProfile.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<Profile?> GetById(object Id)
        {
            using var _uowProfile = new UnitOfWork<Profile>();
            var obj = await _uowProfile.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
