using HMIS.NCD.Domain.Models.OldDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Repositories._UOW
{
    public class UnitOfWorkPhcp<T> : IDisposable where T : class
    {
        private PhcpContext _context;

        public UnitOfWorkPhcp()
        {
            _context = new PhcpContext();
        }

        public UnitOfWorkPhcp(PhcpContext context)
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

        public PhcpContext GetDbContext()
        {
            return _context;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            //if (!this.disposed)
            //{
            //    if (disposing)
            //    {
            //        _context.Dispose();
            //    }
            //}
            //this.disposed = true;
        }

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
    }
}
