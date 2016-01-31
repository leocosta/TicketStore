using System;

namespace TicketStore.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
