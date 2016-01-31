using System;

namespace TicketStore.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
