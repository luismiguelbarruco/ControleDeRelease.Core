using ControleDeRelease.Domain.Entities;
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

            var dataVersao = fileInfo.LastWriteTime;
            var versao = new Version(
                fileVersionInfo.FileMajorPart,
                fileVersionInfo.FileMinorPart,
                fileVersionInfo.FileBuildPart,
                fileVersionInfo.FilePrivatePart
            );

            var dadosVersao = new ReleaseAttributes(versao.ToString(), dataVersao);

            return dadosVersao;
        }
    }
}
