using ControleDeRelease.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Repository
{
    public interface IProjetoRepository
    {
        bool Cadastrar(Projeto projeto);
        bool Atualizar(Projeto projeto);
        bool Deletar(int id);
        Projeto Selecionar(Expression<Func<Projeto, bool>> predicate);
        IEnumerable<Projeto> Selecionar();
    }
}
