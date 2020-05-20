using System;
using System.IO;
using System.IO.Compression;

namespace CustomArchiver
{
    // кастомный провайдер на базе формата ZipArchive
    public static class CustomArchiveProvider
    {
        // открытие архива
        public static ZipArchive OpenArchive(string name, ZipArchiveMode mode)
        {
            FileMode fm = 0;
            FileAccess acc = 0;
            FileShare share = 0;
            if (mode == ZipArchiveMode.Read)
            {
                fm = FileMode.Open;
                acc = FileAccess.Read;
                share = FileShare.Read;
            }
            else if (mode == ZipArchiveMode.Create)
            {
                fm = FileMode.CreateNew;
                acc = FileAccess.Write;
                share = FileShare.None;
            }

            return new ZipArchive(File.Open(name, fm, acc, share), mode, false, null);
        }

        // распаковка архива
        public static void UnpackToDirectory(string src, string dst)
        {
            using (var s = OpenArchive(src, 0))
                s.ExtractToDirectory(dst);
        }

        // архив из директории
        internal static void ArchiveDirectory(string src, string dst)
        {
            using (var dest = OpenArchive(Path.GetFullPath(dst), ZipArchiveMode.Create))
            {
                var di = new DirectoryInfo(Path.GetFullPath(src));
                var fn = di.FullName;
                foreach (var si in di.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    var name = si.FullName.Substring(fn.Length, si.FullName.Length - fn.Length)
                        .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    if (si is FileInfo)
                        ArchiveFile(dest, si.FullName, name);
                    else if (si is DirectoryInfo possiblyEmptyDir && IsEmptyDir(possiblyEmptyDir))
                        dest.CreateEntry(name + '\\');
                }
            }
        }

        // добавление файла в архив
        internal static ZipArchiveEntry ArchiveFile(ZipArchive dst, string src, string e)
        {
            using (Stream stream = File.Open(src, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var ze = dst.CreateEntry(e, 0);
                var dt = File.GetLastWriteTime(src);
                if (dt.Year < 1980 || dt.Year > 2107)
                    dt = new DateTime(1980, 1, 1, 0, 0, 0);
                ze.LastWriteTime = dt;
                using (var destination1 = ze.Open())
                    stream.CopyTo(destination1);
                return ze;
            }
        }

        // проверка папки на пустоту
        private static bool IsEmptyDir(DirectoryInfo di)
        {
            using (var e =
                di.EnumerateFileSystemInfos("*", SearchOption.AllDirectories).GetEnumerator())
                return !e.MoveNext();
        }
    }
}