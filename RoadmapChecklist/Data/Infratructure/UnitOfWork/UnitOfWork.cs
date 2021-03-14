using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infratructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RoadmapChecklistDbContext dbContext;

        public UnitOfWork(RoadmapChecklistDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    dbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
