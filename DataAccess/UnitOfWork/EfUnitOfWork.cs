using System;
using System.Data.Objects;
using RulesRepository.UnitOfWorkInterfaces;

namespace RulesDataAccess.UnitOfWork
{
    public class EfUnitOfWork : IEfUnitOfWork<ObjectContext>
    {
        #region Private Fields

        private readonly ObjectContext _context;
        private bool _disposed = false;

        #endregion
        #region Constructor

        public EfUnitOfWork(ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context", "Unable to initialize Unit of work - context not provided");
            _context = context;
        }
        #endregion
        #region Public Methods

        public ObjectContext GetContext
        {
            get { return _context; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        #region Private Methods

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!disposing) return;
            if (_context == null) return;

            _context.Dispose();
            _disposed = true;
        }

        #endregion
    }
}

