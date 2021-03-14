using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infratructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
