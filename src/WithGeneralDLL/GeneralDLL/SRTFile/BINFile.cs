using Newtonsoft.Json;
using GeneralDLL.SRTExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneralDLL.SRTFile
{
    public class BINFile : IDisposable
    {
        private string FileExtension => ".bin";

        #region Constructors

        public BINFile(string path, string nameFile)
        {
            FilePath = string.IsNullOrEmpty(path) ? Path.GetTempPath() : EnsureDirectoryExists(path);
            FileName = nameFile;
        }

        public BINFile(string pathAndFile)
        {
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

        public bool WriteData(string data)
        {
            try
            {
                if (FileExists)
                    Delete();

                using (var fs = new FileStream(FileFullPath, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        //bw.Write(true);  //writing bool value
                        //bw.Write(Convert.ToByte('a')); //writing byte
                        //bw.Write('a');                 //writing character
                        bw.Write(data);            //string
                                                    //bw.Write(123);                 //number
                                                    //bw.Write(123.12);              // double value
                        bw.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch /*(Exception ex)*/
            {
                // Log or handle exception as needed
                return false;
            }
        }

        public string ReadData()
        {
            if (!FileExists)
                return null;

            var _string = "";
            using (var fs = new FileStream(FileFullPath, FileMode.Open))
            {
                using (var br = new BinaryReader(fs))
                {
                    //bool b = br.ReadBoolean();
                    //byte _byte = br.ReadByte();
                    //char _char = br.ReadChar();
                    _string = br.ReadString();
                    //int _int = br.ReadInt16();
                    //double _dbl = br.ReadDouble();

                    br.Close();
                }
                fs.Close();
            }

            return _string;
        }

        public T ReadData<T>()
        {
            var jsonData = ReadData();
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            // Implement IDisposable pattern if needed
        }

        #endregion
    }
}
