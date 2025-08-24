// Ignore Spelling: DTO App

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.FileManagement
{
    public class FileInfoClient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameFile { get; set; }
        public string type { get; set; }
        public double size { get; set; }

        [JsonIgnore]
        public string _url;

        public string url
        {
            get
            {
                if (_url == null)
                    return null;

                if (_url.StartsWith("http"))
                {
#if DEBUG
                    if (_url.Contains("37.114.225.171:4512"))
                        return _url.Replace("http://37.114.225.171:4512/api/fileManagement", "http://localhost:10000/api/fileManagement")
                                   .Replace("http://37.114.225.171:4512/api/fileManagement", "http://localhost:6999/api/fileManagement");
#endif

                    return _url.Replace("http://localhost:10000/api/fileManagement", "http://localhost:6999/api/fileManagement");
                }

#if DEBUG
                return "http://localhost:6999/api/fileManagement" + _url;
#else
                return "http://37.114.225.171:4512/api/fileManagement" + _url;
#endif
            }
            set => _url = value;
        }

        public string application { get; set; }
    }
}
