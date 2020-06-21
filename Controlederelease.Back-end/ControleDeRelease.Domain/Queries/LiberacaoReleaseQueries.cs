using ControleDeRelease.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Queries
{
    public class LiberacaoReleaseQueries
    {
        public static Expression<Func<LiberacaoRelease, bool>> Selecionar(int idVersao) => l => l.Versao.Id == idVersao;
    }
}
