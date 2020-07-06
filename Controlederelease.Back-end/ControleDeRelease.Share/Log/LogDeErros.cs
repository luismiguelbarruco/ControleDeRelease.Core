using ControleDeRelease.Share.Helper;
using System;
using System.IO;
using System.Text;

namespace ControleDeRelease.Share.Log
{
    public class LogDeErros
    {
        private readonly string _arquivo;
        private static readonly Encoding _Win28591 = Encoding.GetEncoding(28591);
        private static object _locker = new object();

        public static readonly LogDeErros Default = new LogDeErros(Path.Combine($@"{AppDomain.CurrentDomain.BaseDirectory}", "ControleDeRelease.log"));

        public string Arquivo => _arquivo;

        private string AssemblyName => AssemblyHelper.Current.Title;

        private string AssemblyVersion
        {
            get
            {
                Version version = AssemblyHelper.Current.Version;

                return $"{version.Major}.{version.Minor}.{version.Revision}";
            }
        }

        internal void Gravar(Exception erro) => Gravar(erro, erro.Message);

        public void Gravar(Exception erro, string mensagem)
        {
            lock (_locker)
            {
                string arquivo = Arquivo;

                Gravar(arquivo, erro, mensagem);

                CorrigirTamanhoDoArquivo(arquivo);
            }
        }

        protected virtual void Gravar(string arquivo, Exception erro, string mensagem)
        {
            using StreamWriter streamWriter = new StreamWriter(arquivo, true, _Win28591);

            streamWriter.WriteLine($"Assembly de Origem: {AssemblyName}.exe ({AssemblyVersion})");
            streamWriter.WriteLine("Ocorrido em {0:F}", DateTime.Now);
            streamWriter.WriteLine(mensagem.Replace("\r\n", "\n").Replace("\n", "  "));

            if (erro != null)
            {
                streamWriter.WriteLine(erro.Message);
                streamWriter.WriteLine("Origem");
                streamWriter.WriteLine(string.Join("\r\n", (erro.StackTrace ?? erro.InnerException?.StackTrace ?? "Sem stack trace").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)));
            }

            streamWriter.WriteLine();
        }

        protected virtual void CorrigirTamanhoDoArquivo(string arquivo)
        {
            const int maximum = 102400;

            if (File.Exists(arquivo) && new FileInfo(arquivo).Length > maximum)
            {
                using MemoryStream memroyStream = new MemoryStream(maximum);
                using FileStream fileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite);

                fileStream.Seek(-maximum, SeekOrigin.End);
                fileStream.CopyTo(memroyStream);
                fileStream.SetLength(maximum);
                fileStream.Position = 0;
                memroyStream.Position = 0;
                memroyStream.CopyTo(fileStream);
            }
        }

        public LogDeErros(string arquivo) => _arquivo = arquivo;
    }
}
