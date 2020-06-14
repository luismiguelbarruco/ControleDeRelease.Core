using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Share.Log;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Repository
{
    public class VersaoProjetoRepository : IVersaoProjetoRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ILiteCollection<Versao> _versaoProjetoCollection;

        public VersaoProjetoRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _versaoProjetoCollection = _dataBaseContext.GetCollection<Versao>();
        }

        public bool Atualizar(Versao versao)
        {
            try
            {
                _versaoProjetoCollection.Update(versao);

                return true;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao atualizar versão");
                return false;
            }
        }

        public bool Cadastrar(Versao versao)
        {
            try
            {
                var result = _versaoProjetoCollection.Insert(versao);

                return true;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao cadastrar versão");
                return false;
            }
        }

        public IEnumerable<Versao> Selecionar()
        {
            try
            {
                var result = _versaoProjetoCollection.FindAll();

                return result;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao selecionar versão");
                return null;
            }
        }

        public bool Deletar(int id)
        {
            try
            {
                var result = _versaoProjetoCollection.Delete(id);

                return result;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao deletar versão");
                return false;
            }
        }

        public Versao Selecionar(Expression<Func<Versao, bool>> predicate)
        {
            try
            {
                var versao = _versaoProjetoCollection.Find(predicate).FirstOrDefault();

                return versao;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao selecionar versão");
                return null;
            }
        }
    }
}
