
using ControleDeRelease.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace ControleDeRelease.Domain.Queries
{
    public abstract class QueriesBase<TEntity> where TEntity : IEntity
    {
        public static Expression<Func<TEntity, bool>> Selecionar(string projeto) => p => p.Nome == projeto;

        public static Expression<Func<TEntity, bool>> Selecionar(int id) => p => p.Id == id;
    }
}
