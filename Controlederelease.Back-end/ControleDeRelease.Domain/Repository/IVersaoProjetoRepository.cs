using ControleDeRelease.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Repository
{
    public interface IVersaoProjetoRepository
    {
        bool Cadastrar(Versao diretorioProjeto);
        bool Atualizar(Versao diretorioProjeto);
        bool Deletar(int id);
        Versao Selecionar(Expression<Func<Versao, bool>> predicate);
        IEnumerable<Versao> Selecionar();
    }
}
