// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.FileManagement
{
    public class FileInfoCDN
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string NameBase { get; set; }
        public string fileType { get; set; }
        public double fileSize { get; set; }
        public string bucketName { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public int idAdmin { get; set; } = 1;

        public string GetURL()
        { return $@"http://localhost:9010/uploads/{fileName}"; }
    }
}
