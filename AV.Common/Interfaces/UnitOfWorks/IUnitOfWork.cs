using System;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}