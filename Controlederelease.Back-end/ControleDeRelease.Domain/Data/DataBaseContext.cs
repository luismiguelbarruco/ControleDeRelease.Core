using LiteDB;
using System;

namespace ControleDeRelease.Domain.Data
{
    public class DataBaseContext : IDisposable
    {
        private ILiteDatabase _dataBase { get; set; }

        public DataBaseContext() => _dataBase = new LiteDatabase(CreateConnectionString());

        public bool Commit() => _dataBase.Commit();

        public bool Rollback() => _dataBase.Rollback();

        public bool BeginTrans() => _dataBase.BeginTrans();

        public ILiteCollection<T> GetCollection<T>() => _dataBase.GetCollection<T>();

        public void Dispose() => _dataBase.Dispose();

        private ConnectionString CreateConnectionString()
        {
            return new ConnectionString
            {
                Filename = @"D:\Desenvolvimento\Projetos\ControleDeRelease.Core\database.db",
                Connection = ConnectionType.Shared
            };
        }
    }
}
