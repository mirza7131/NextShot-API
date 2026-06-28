using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class RoleRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;
        private readonly UnitOfWork<Role> _uowRole;


        #endregion

        #region Constructor

        public RoleRepository(GamaContext dbContext , UnitOfWork<Role> uowRole)
        {
            _dbContext = dbContext;
            _uowRole = uowRole;
        }

        #endregion

        #region CUD Operations

        public async Task<Role> Create(Role input)
        {
            var res = _uowRole.Repository.Insert(input);
            await _uowRole.Save();
            return await Task.FromResult(input);
        }

        public async Task<Role> Update(Role input)
        {
            _uowRole.Repository.Update(input);
            await _uowRole.Save();
            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(Role input)
        {
            _uowRole.Repository.Update(input);
            await _uowRole.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<Role>> GetALL(Expression<Func<Role, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowRole = new UnitOfWork<Role>();
            return await _uowRole.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<Role?> GetById(object Id)
        {
            var obj = await _uowRole.Repository.GetById(Id);
            return obj;
        }

        public async Task<Role?> GetByIdWithRoleMenuDetails(object Id)
        {
            var obj = await _uowRole.Repository.GetALL(x => x.RoleId.ToString() == Id.ToString()).Include(x => x.RoleMenus).FirstOrDefaultAsync();
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
