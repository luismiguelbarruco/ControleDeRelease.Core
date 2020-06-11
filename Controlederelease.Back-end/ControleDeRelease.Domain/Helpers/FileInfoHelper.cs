using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace ControleDeRelease.Domain.Helpers
{
    public static class FileInfoHelper
    {
        public static AttributeFile GetAttributeFile(string path)
        {
            var fileInfo = new FileInfo(path);
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(fileInfo.FullName);

            var attributeFile = new AttributeFile
            {
                FileVersionInfo = fileVersionInfo
                //obter data release
            };

            return attributeFile;
        }

        public static StatusRelease CompareVersion(Version fileVersionInfoRelease, Version fileVersionInfoTeste)
        {
            return fileVersionInfoTeste > fileVersionInfoRelease ? StatusRelease.Atualizado : StatusRelease.Mantido;
        }
    }
}
