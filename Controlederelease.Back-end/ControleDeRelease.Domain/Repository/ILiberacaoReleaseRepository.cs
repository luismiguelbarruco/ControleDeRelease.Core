using ControleDeRelease.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Repository
{
    public interface ILiberacaoReleaseRepository
    {
        bool Cadastar(LiberacaoRelease liberacaoRelease);
        LiberacaoRelease Seleionar(Expression<Func<LiberacaoRelease, bool>> predicate);
    }
}
