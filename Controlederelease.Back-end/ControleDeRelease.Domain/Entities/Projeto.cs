
namespace ControleDeRelease.Domain.Entities
{
    public class Projeto : EntityBase
    {
        private string _subpasta;
        public string Subpasta
        {
            get { return _subpasta; }
            set 
            {
                if (value == null)
                    value = string.Empty;

                _subpasta = value; 
            }
        }

        public string Path
        {
            get
            {
                if(string.IsNullOrEmpty(Subpasta))
                    return $@"{Nome}";

                else
                    return $@"{Subpasta}\{Nome}";
            }
        }
    }
}
