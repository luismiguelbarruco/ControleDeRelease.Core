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
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ILiteCollection<Projeto> _projetoCollection;

        public ProjetoRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _projetoCollection = _dataBaseContext.GetCollection<Projeto>();
        }

        public bool Cadastrar(Projeto projeto)
        {
            try
            {
                _projetoCollection.Insert(projeto);

                return true;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao cadastrar projeto");
                return false;
            }
        }

        public bool Atualizar(Projeto projeto)
        {
            try
            {
                return _projetoCollection.Update(projeto);
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao atualizar projeto");
                return false;
            }
        }

        public bool Deletar(int id)
        {
            try
            {
                return _projetoCollection.Delete(id);
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao deletar projeto");
                return false;
            }
        }

        public IEnumerable<Projeto> Selecionar()
        {
            //Revisar log
            try
            {
                var projetos = _projetoCollection.FindAll();

                return projetos;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao selecionar projeto");
                return null;
            }
        }

        public Projeto Selecionar(Expression<Func<Projeto, bool>> predicate)
        {
            //Revisar log
            try
            {
                var projeto = _projetoCollection.Find(predicate).FirstOrDefault();

                return projeto;
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "Erro ao selecionar projeto");
                return null;
            }
        }
    }
}
