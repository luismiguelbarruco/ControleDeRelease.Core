
namespace ControleDeRelease.Domain.Entities
{
    public class Versao : EntityBase
    {
        private string _diretorioRelease;
        public string DiretorioRelease
        {
            get { return _diretorioRelease; }
            set
            {
                if (value == null)
                    value = string.Empty;

                _diretorioRelease = value;
            }
        }

        private string _diretorioTeste;
        public string DiretorioTeste
        {
            get { return _diretorioTeste; }
            set
            {
                if (value == null)
                    value = string.Empty;

                _diretorioTeste = value;
            }
        }
    }
}
