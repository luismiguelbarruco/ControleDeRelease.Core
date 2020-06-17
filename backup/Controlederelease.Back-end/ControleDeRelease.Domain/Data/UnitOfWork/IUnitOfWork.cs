using ControleDeRelease.Domain.Repository;
using System;

namespace ControleDeRelease.Domain.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProjetoRepository GetProjetoRepository();
        IVersaoProjetoRepository GetVersaoProjetoRepository();
        ILiberacaoReleaseRepository GetLiberacaoReleaseRepository();
        void Commit();
    }
}
