using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Share.Log;
using LiteDB;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Repository
{
    public class LiberacaoReleaseRepository : ILiberacaoReleaseRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ILiteCollection<LiberacaoRelease> _liberacaoReleaseCollection;

        public LiberacaoReleaseRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _liberacaoReleaseCollection = _dataBaseContext.GetCollection<LiberacaoRelease>();
        }

        public bool Cadastar(LiberacaoRelease liberacaoRelease)
        {
            try
            {
                var resul = _liberacaoReleaseCollection.Insert(liberacaoRelease);

                return true;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao cadastrar os dados liberação de release");
                return false;
            }
        }

        public LiberacaoRelease Seleionar(Expression<Func<LiberacaoRelease, bool>> predicate)
        {
            try
            {
                var result = _liberacaoReleaseCollection.Find(predicate).LastOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao cadastrar os dados liberação de release");
                return null;
            }
        }
    }
}
