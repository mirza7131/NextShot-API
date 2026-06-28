using HMIS.FamilyPlanning.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Repositories.UOW
{
	public class UnitOfWork<T> : IDisposable where T : class
	{
		private HmisAuthContext _context;

		public UnitOfWork()
		{
			_context = new HmisAuthContext();
		}

		public UnitOfWork(HmisAuthContext context)
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

		public HmisAuthContext GetDbContext()
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
