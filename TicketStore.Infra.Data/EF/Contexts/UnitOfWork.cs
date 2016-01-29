using ProReserve.Reserve.Domain.Licenciados;
using ProReserve.Reserve.Domain.Repositories;
using System;

namespace ProReserve.Reserve.Infrastructure.Data.EF.Contexts
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _licenciadoContextHasValidConnection = false;
        private Licenciado _licenciado;

        public virtual ReserveContext ReserveContext { get; set; }
        public virtual ProReserveContext ProReserveContext { get; set; }
        public virtual LicenciadoContext LicenciadoContext { get; set; }

        public InfoBancoDeDados BDLicenciado
        {
            set
            {
                LicenciadoContext.SetupConnection(value);
                _licenciadoContextHasValidConnection = true;
            }
        }

        public Licenciado LicenciadoDaSessao
        {
            get
            {
                return _licenciado;
            }
            set
            {
                _licenciado = value;
                BDLicenciado = value.BD;
            }
        }

        public UnitOfWork(ReserveContext reserveContext, ProReserveContext proreserveContext, LicenciadoContext licenciadoContext)
        {
            ReserveContext = reserveContext;
            ProReserveContext = proreserveContext;
            LicenciadoContext = licenciadoContext;
        }

        public void SalvarAlteracoes()
        {
            ReserveContext.Commit();
            ProReserveContext.Commit();

            if (_licenciadoContextHasValidConnection)
                LicenciadoContext.Commit();
        }

        public virtual void Dispose()
        {
            ReserveContext.Dispose();
            ProReserveContext.Dispose();
            LicenciadoContext.Dispose();
        }
    }
}