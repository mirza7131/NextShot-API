using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class RoleMenuRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly GamaContext _dbContext;
        private UnitOfWork<RoleMenu> _uowRoleMenu;


        #endregion

        #region Constructor

        public RoleMenuRepository(GamaContext dbContext, UnitOfWork<RoleMenu> uowRoleMenu)
        {
            _dbContext = dbContext;
            _uowRoleMenu = uowRoleMenu;
        }

        #endregion

        #region CUD Operations

        public async Task<RoleMenu> Create(RoleMenu input)
        {
            var res = _uowRoleMenu.Repository.Insert(input);
            await _uowRoleMenu.Save();

            return await Task.FromResult(input);
        }

        public async Task<RoleMenu> Update(RoleMenu input)
        {
            _uowRoleMenu.Repository.Update(input);
            await _uowRoleMenu.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(RoleMenu input)
        {
            _uowRoleMenu.Repository.Update(input);
            await _uowRoleMenu.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<RoleMenu>> GetALL(Expression<Func<RoleMenu, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return await _uowRoleMenu.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<RoleMenu?> GetById(object Id)
        {
            var obj = await _uowRoleMenu.Repository.GetById(Id);
            return obj;
        }

        public async Task<List<ViewGetEditRoleMenuAccess>> GetEditRoleMenuAccess(Guid? RoleId)
        {


            using (var db = new GamaContext())
            {
                var conn = _uowRoleMenu.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetEditRoleMenuAccess", (SqlConnection)conn);
                    sqlComm.Parameters.AddWithValue("@RoleId", RoleId);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewGetEditRoleMenuAccess> lst =  ds.Tables[0].ToList<ViewGetEditRoleMenuAccess>();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<List<ViewGetCreateRoleMenuAccess>> GetCreateRoleMenuAccess()
        {
            List<ViewGetCreateRoleMenuAccess> viewGetCreateRoleMenuAccesses = null;
            return viewGetCreateRoleMenuAccesses;
            //return await _uowRoleMenu.GetDbContext().ViewGetCreateRoleMenuAccesses.ToListAsync();
        }

        #endregion

        #region Helper Methods




        #endregion
    }
}
