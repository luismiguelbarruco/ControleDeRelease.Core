
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Entities
{
    public class Projeto : EntityBase
    {
        public string SubpastaRelease { get; set; }
        public string SubpastaTeste { get; set; }

        public string PathRelease
        {
            get
            {
                if(string.IsNullOrEmpty(SubpastaRelease))
                    return $@"{Nome}";

                else
                    return $@"{SubpastaRelease}\{Nome}";
            }
        }

        public string PathTeste
        {
            get
            {
                if (string.IsNullOrEmpty(SubpastaTeste))
                    return $@"{Nome}";

                else
                    return $@"{SubpastaTeste}\{Nome}";
            }
        }

        public List<Versao> Versoes { get; set; } = new List<Versao>();

        public Versao Versao { get; set; }
    }
}
