using ControleDeRelease.Share.Inicialization;
using ControleDeRelease.Share.Log;
using LiteDB;
using System;
using System.IO;

namespace ControleDeRelease.Domain.Data
{
    public class DataBaseContext : IDisposable
    {
        private ILiteDatabase _dataBase { get; set; }

        public DataBaseContext()
        {
            try
            {
                var conenctionString = CreateConnectionString();

                if (!File.Exists(conenctionString.Filename))
                {
                    var exception = new FileNotFoundException($"Arquivo {conenctionString.Filename} não encontrado.");
                    
                    LogDeErros.Default.Gravar(exception, $"Arquivo {conenctionString.Filename} não encontrado.");

                    throw exception;
                }

                _dataBase = new LiteDatabase(CreateConnectionString());
            }
            catch (Exception ex)
            {
                LogDeErros.Default.Gravar(ex, "erro ao conectar no banco de dados!");
                throw;
            }
        }

        public bool Commit() => _dataBase.Commit();

        public bool Rollback() => _dataBase.Rollback();

        public bool BeginTrans() => _dataBase.BeginTrans();

        public ILiteCollection<T> GetCollection<T>() => _dataBase.GetCollection<T>();

        public void Dispose() => _dataBase.Dispose();

        private ConnectionString CreateConnectionString()
        {
            return new ConnectionString
            {
                Filename = new Inicialization().DataBasePath,
                Connection = ConnectionType.Shared
            };
        }
    }
}
