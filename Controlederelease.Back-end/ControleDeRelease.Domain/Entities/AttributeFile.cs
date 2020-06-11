using System;
using System.Diagnostics;

namespace ControleDeRelease.Domain.Entities
{
    public class AttributeFile
    {
        private FileVersionInfo _FileVersionInfo;
        public FileVersionInfo FileVersionInfo 
        {
            get => _FileVersionInfo;
            set
            {
                _FileVersionInfo = value;
                SetVersionFile();
            }
        }
        public DateTime DataVersao { get; set; }
        public Version Versao { get; private set; }

        private void SetVersionFile()
        {
            Versao = new Version(
                        FileVersionInfo.FileMajorPart,
                        FileVersionInfo.FileMinorPart,
                        FileVersionInfo.FileBuildPart,
                        FileVersionInfo.FilePrivatePart
                     );
        }
    }
}
