using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable StringLiteralTypo
namespace CustomArchiver
{
    internal class UiLogic
    {
        public static string RootPath, ArchivePath, UnpackPath;
        public static List<string> SrcFiles;

        // инициализация
        internal static void InitUi(IReadOnlyCollection<Control> ctrl)
        {
            ctrl.ElementAt(0).Click += delegate { SelectRoot(ctrl.ElementAt(4)); };
            ctrl.ElementAt(1).Click += delegate { CreateArchive(ctrl.ElementAt(4)); };
            ctrl.ElementAt(2).Click += delegate { OpenArchive(ctrl.ElementAt(4)); };
            ctrl.ElementAt(3).Click += delegate { UnpackArchive(ctrl.ElementAt(4)); };
            ctrl.ElementAt(4).Text +=
                $@"
{DateTime.Now} - Application started, please proceed with actions";
        }

        // выбор папки с файлами для архива
        public static void SelectRoot(Control ct)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();
                RootPath = string.IsNullOrWhiteSpace(fbd.SelectedPath) ? string.Empty : fbd.SelectedPath;
                if (result != DialogResult.OK) return;
                SrcFiles = new List<string>();
                RecursiveSearch(RootPath, SrcFiles);
                ct.Text +=
                    $@"
{DateTime.Now} - You have selected the root '{RootPath}', containing {SrcFiles.Count} files, {GetFolderSize(SrcFiles)} mb total";
            }
        }

        // создание архива
        public static void CreateArchive(Control ct)
        {
            if (string.IsNullOrWhiteSpace(RootPath))
                MessageBox.Show(@"Please select source folder first", @"Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
            {
                var sfd = new SaveFileDialog
                {
                    Filter = @"Custom Archive Format|*.caf", Title = @"Create custom archive..."
                };
                sfd.ShowDialog();
                ArchivePath = string.IsNullOrWhiteSpace(sfd.FileName) ? string.Empty : sfd.FileName;
                if (ArchivePath != "")
                    CustomArchiveProvider.ArchiveDirectory(RootPath, ArchivePath);
                ct.Text +=
                    $@"
{DateTime.Now} - Archive created at '{ArchivePath}', with {SrcFiles.Count} files, {GetFolderSize(SrcFiles)} mb files, {Math.Round((double) new FileInfo(ArchivePath).Length / 1024 / 1024, 2)} mb compressed";
                Process.Start("explorer.exe", Directory.GetParent(ArchivePath).FullName);
            }
        }

        // открытие и анализ архива
        public static void OpenArchive(Control ct)
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"Custom archive files|*.caf",
                InitialDirectory = @"%HOMEPATH%/Desktop",
                Title = @"Please select custom archive..."
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            ArchivePath = string.IsNullOrWhiteSpace(ofd.FileName) ? string.Empty : ofd.FileName;
            var count = 0;
            double com = 0;
            double raw = 0;
            using (var arc = CustomArchiveProvider.OpenArchive(ArchivePath, 0))
            {
                foreach (var entry in arc.Entries)
                {
                    count++;
                    com += entry.CompressedLength;
                    raw += entry.Length;
                }
            }

            ct.Text +=
                $@"
{DateTime.Now} - Selected archive at '{ArchivePath}', contains {count} files, {Math.Round(raw / 1024 / 1024, 2)} mb files, {Math.Round(com / 1024 / 1024, 2)} mb compressed";
        }

        // распаковка архива
        public static void UnpackArchive(Control ct)
        {
            if (string.IsNullOrWhiteSpace(ArchivePath))
                MessageBox.Show(@"Please create new or select archive first", @"Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    var result = fbd.ShowDialog();
                    UnpackPath = string.IsNullOrWhiteSpace(fbd.SelectedPath) ? string.Empty : fbd.SelectedPath;
                    if (result != DialogResult.OK) return;
                    CustomArchiveProvider.UnpackToDirectory(ArchivePath, UnpackPath);
                    SrcFiles = new List<string>();
                    RecursiveSearch(UnpackPath, SrcFiles);
                    ct.Text +=
                        $@"
{DateTime.Now} - Selected archive was unpacked to '{UnpackPath}', {SrcFiles.Count} files, {GetFolderSize(SrcFiles)} mb total";
                    Process.Start("explorer.exe", UnpackPath);
                }
            }
        }

        // размер папки в мб
        private static double GetFolderSize(IEnumerable<string> files) =>
            Math.Round(files.Aggregate<string, double>(0, (c, t) =>
                c + new FileInfo(t).Length) / 1024 / 1024, 2);

        // рекурсивный поиск путей файлов
        private static void RecursiveSearch(string src, List<string> files)
        {
            files.AddRange(Directory.GetFiles(src));
            foreach (var d in Directory.GetDirectories(src)) RecursiveSearch(d, files);
        }
    }
}