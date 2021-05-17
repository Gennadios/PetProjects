using System;
using System.IO;

namespace CustomLogger
{
    class LogBase
    {
        enum FileFormat
        {
            TXT,
            PDF,
            DOC
        }

        private string _dateString = DateTime.Now.ToShortDateString();
        private string _timeString = DateTime.Now.ToLongTimeString();
        private static int _counter = 1;

        internal void LogDebug(string logMessage)
        {
            using StreamWriter sw = File.AppendText(GetFile(FileFormat.TXT));
            sw.Write("\n[DEBUG] ");
            sw.Write($"{_dateString} {_timeString}");
            sw.WriteLine($"{logMessage}");
        }

        internal void LogInfo(string logMessage)
        {
            using StreamWriter sw = File.AppendText(GetFile(FileFormat.TXT));
            sw.Write("\n[INFO]: ");
            sw.Write($"{_dateString} {_timeString}");
            sw.WriteLine($"{logMessage}");
        }

        internal void LogError(string logMessage)
        {
            string targetFile = GetFile(FileFormat.TXT);
            using StreamWriter sw = File.AppendText(GetFile(FileFormat.TXT));
            sw.Write("\n[ERROR]: ");
            sw.WriteLine($"{_dateString} {_timeString}");
            sw.WriteLine($"{logMessage}");
        }

        private string GetFile(FileFormat fileFormat)
        {
            string filePath = GetFilePath(fileFormat);
            UpdateFileName(filePath, fileFormat);

            return filePath;
        }

        private string GetFilePath(FileFormat fileFormat)
        {
            string filePath =
                Directory.CreateDirectory("Logs") + @".\\" +
                "log" + DateTime.Now.ToString("yyyy-MM-dd") +
                "_" + _counter;

            switch (fileFormat)
            {
                case FileFormat.TXT:
                    filePath += ".txt";
                    break;
                case FileFormat.PDF:
                    filePath += ".PDF";
                    break;
                case FileFormat.DOC:
                    filePath += ".doc";
                    break;
                default:
                    break;
            }

            return filePath;
        }

        private void UpdateFileName(string fileName, FileFormat fileFormat)
        {
            FileInfo logFileInfo = new(fileName);
            while (File.Exists(fileName) && logFileInfo?.Length > Logger.MaxFileSize)
            {
                _counter++;
                fileName = GetFilePath(fileFormat);
            }
        }
    }
}
