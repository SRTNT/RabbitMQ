// Ignore Spelling: SRT

using Newtonsoft.Json;
using GeneralDLL.SRTExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneralDLL.SRTFile
{
    public class BasicFile : IDisposable
    {
        public string FileExtension { get; set; } = ".txt";
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        #region Constructors

        public BasicFile(string fileExtension, string path, string nameFile)
        {
            FileExtension = fileExtension;
            FilePath = string.IsNullOrEmpty(path) ? Path.GetTempPath() : EnsureDirectoryExists(path);
            FileName = nameFile;
        }

        public BasicFile(string fileExtension, string pathAndFile)
        {
            FileExtension = fileExtension;
            FileFullPath = pathAndFile;
        }

        #endregion

        #region Properties

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            private set => _filePath = FixFolderPath(value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            private set => _fileName = ValidateFileName(value);
        }

        #region Full Address
        public string FileFullPath
        {
            get => Path.Combine(FilePath, FileName);
            private set
            {
                var _Path = value.Substring(0, value.LastIndexOf('\\'));
                var _NameFile = value.Replace(_Path, "").Replace("\\", "");

                if (value == "")
                    FilePath = Path.GetTempFileName();
                else
                {
                    if (!Directory.Exists(_Path))
                        Directory.CreateDirectory(_Path);
                    FilePath = _Path;
                }

                FileName = _NameFile;
            }
        }
        #endregion

        public bool FileExists => Exists();
        public DateTime FileDate => File.GetCreationTime(FileFullPath);

        #endregion

        #region Private Methods

        private string FixFolderPath(string path)
        {
            path = path.Replace(@"\\", @"\");
            return path.EndsWith(@"\") ? path : path + @"\";
        }

        private string ValidateFileName(string fileName)
        {
            return fileName.EndsWith(FileExtension) ? fileName : fileName + FileExtension;
        }

        private string EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        private bool Exists()
        {
            try
            {
                return File.Exists(FileFullPath);
            }
            catch /*(Exception ex)*/
            {
                // Log or handle exception as needed
                return false;
            }
        }

        #endregion

        #region File Operations

        public bool Delete()
        {
            try
            {
                File.Delete(FileFullPath);
                return true;
            }
            catch /*(Exception ex)*/
            {
                // Log or handle exception as needed
                return false;
            }
        }

        public bool WriteData(string data, Encoding encoding = null)
        {
            encoding ??= DefaultEncoding;
            try
            {
                using (var writer = new StreamWriter(FileFullPath, false, encoding))
                {
                    writer.Write(data);
                }
                return true;
            }
            catch /*(Exception ex)*/
            {
                // Log or handle exception as needed
                return false;
            }
        }

        public bool WriteDataLine(string data, Encoding encoding = null)
        {
            encoding ??= DefaultEncoding;
            try
            {
                using (var writer = new StreamWriter(FileFullPath, true, encoding))
                {
                    writer.WriteLine(data);
                }
                return true;
            }
            catch /*(Exception ex)*/
            {
                // Log or handle exception as needed
                return false;
            }
        }

        public string ReadData(Encoding encoding = null)
        {
            encoding ??= DefaultEncoding;
            using (var reader = new StreamReader(FileFullPath, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public T ReadData<T>()
        {
            var jsonData = ReadData();
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public List<string[]> ReadDataAsListOfLines(char separator = ';', Encoding encoding = null)
        {
            encoding ??= DefaultEncoding;
            try
            {
                var content = ReadData(encoding);
                return content.SRT_String_Converter().Split(Environment.NewLine)
                    .Select(line => line.Split(separator))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot read file => " + FileName, ex);
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            // Implement IDisposable pattern if needed
        }

        #endregion

        public static void CopyFolder(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            foreach (var filePath in Directory.GetFiles(sourcePath))
            {
                var fileName = Path.GetFileName(filePath);
                var destinationFilePath = Path.Combine(destinationPath, fileName);

                try
                {
                    if (File.Exists(destinationFilePath))
                    {
                        File.Delete(destinationFilePath);
                    }
                }
                catch /*(Exception ex)*/
                {
                    // Log or handle exception as needed
                }

                File.Copy(filePath, destinationFilePath, true);
            }

            foreach (var subdirectoryPath in Directory.GetDirectories(sourcePath))
            {
                var subdirectoryName = Path.GetFileName(subdirectoryPath);
                var destinationSubdirectoryPath = Path.Combine(destinationPath, subdirectoryName);
                CopyFolder(subdirectoryPath, destinationSubdirectoryPath);
            }
        }
    }
}
