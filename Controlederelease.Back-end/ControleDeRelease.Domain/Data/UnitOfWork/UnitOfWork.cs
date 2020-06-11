
using ControleDeRelease.Domain.Repository;

namespace ControleDeRelease.Domain.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _dataBaseContext;
        private IProjetoRepository _projetoRepository;
        private IVersaoProjetoRepository _diretorioProjetoRepository;

        public UnitOfWork(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _dataBaseContext.BeginTrans();
        }

        public void Commit()
        {
            _dataBaseContext.Commit();
            _dataBaseContext.BeginTrans();
        }

        public void Dispose()
        {
            if (_dataBaseContext != null)
                _dataBaseContext.Rollback();

            _dataBaseContext.Dispose();
        }

        public IProjetoRepository GetProjetoRepository()
        {
            return _projetoRepository ?? (_projetoRepository = new ProjetoRepository(_dataBaseContext));
        }

        public IVersaoProjetoRepository GetVersaoProjetoRepository()
        {
            return _diretorioProjetoRepository ?? (_diretorioProjetoRepository = new VersaoProjetoRepository(_dataBaseContext));
        }
    }
}
