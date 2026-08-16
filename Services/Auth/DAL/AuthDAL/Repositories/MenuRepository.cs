using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class MenuRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly NextShotContext _dbContext;
        private readonly UnitOfWork<Menu> _uowMenu;


        #endregion

        #region Constructor

        public MenuRepository(NextShotContext dbContext, UnitOfWork<Menu> uowMenu)
        {
            _dbContext = dbContext;
            _uowMenu = uowMenu;
        }

        #endregion

        #region CUD Operations

        public async Task<Menu> Create(Menu input)
        {
            using var _uowMenu = new UnitOfWork<Menu>();
            var res = _uowMenu.Repository.Insert(input);
            await _uowMenu.Save();

            return await Task.FromResult(input);
        }

        public async Task<Menu> Update(Menu input)
        {
            using var _uowMenu = new UnitOfWork<Menu>();
            _uowMenu.Repository.Update(input);
            await _uowMenu.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(Menu input)
        {
            using var _uowMenu = new UnitOfWork<Menu>();
            _uowMenu.Repository.Update(input);
            await _uowMenu.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<Menu>> GetALL(Expression<Func<Menu, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowMenu = new UnitOfWork<Menu>();
            return await _uowMenu.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<Menu?> GetById(object Id)
        {
            using var _uowMenu = new UnitOfWork<Menu>();
            var obj = await _uowMenu.Repository.GetById(Id);
            return obj;
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
