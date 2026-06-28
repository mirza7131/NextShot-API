using AuthDAL.Models.DbModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Repositories.UOW
{
    public class UnitOfWork<T> : IDisposable where T : class
    {
        private GamaContext _context;

        public UnitOfWork()
        {
            _context = new GamaContext();
        }

        public UnitOfWork(GamaContext context)
        {
            _context = context;
        }

        private GenericRepository<T> repository;

        public GenericRepository<T> Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = new GenericRepository<T>(_context);
                }
                return repository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public GamaContext GetDbContext()
        {
            return  _context;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
