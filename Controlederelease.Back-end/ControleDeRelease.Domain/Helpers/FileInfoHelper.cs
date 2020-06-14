using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Fake;
using System;
using System.Diagnostics;
using System.IO;

namespace ControleDeRelease.Domain.Helpers
{
    public static class FileInfoHelper
    {
        public static ReleaseAttributes GetDataFileVersion(string path)
        {
            var fileInfo = new FileInfo(path);
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(fileInfo.FullName);

            //obter data release

            var versao = new Version(
                fileVersionInfo.FileMajorPart,
                fileVersionInfo.FileMinorPart,
                fileVersionInfo.FileBuildPart,
                fileVersionInfo.FilePrivatePart
            );

            var dadosVersao = new ReleaseAttributes(versao.ToString(), DateTime.Now);

            return dadosVersao;
        }

        public static DadosVersaoFake GetDataFileVersionFake()
        {
            var fileInfo = new FileInfo(@"C:\Program Files (x86)\WinRAR\WinRAR.exe");
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(fileInfo.FullName);

            var versao = new Version(
                fileVersionInfo.FileMajorPart,
                fileVersionInfo.FileMinorPart,
                fileVersionInfo.FileBuildPart,
                fileVersionInfo.FilePrivatePart
            );

            var dadosVersao = new DadosVersaoFake(versao.ToString(), DateTime.Now);

            return dadosVersao;
        }
    }
}
