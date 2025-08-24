// Ignore Spelling: Enums DTO

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum FileType
    {
        [Description("jpeg")]
        jpeg = 0,
        [Description("jpg")]
        jpg = 1,
        [Description("webp")]
        webp = 2,
        [Description("png")]
        png = 3,
        [Description("gif")]
        gif = 4,
        [Description("bmp")]
        bmp = 5,
        [Description("svg")]
        svg = 6,
        [Description("tif")]
        tif = 7,
        [Description("pdf")]
        pdf = 8,
        [Description("rar")]
        RAR = 9,
        [Description("word")]
        Word = 10,
        [Description("excel")]
        Excel = 11,
        [Description("csv")]
        csv = 12,
    }

    public class FileTypeGetList
    {
        public static List<FileType> GetPictureType()
        {
            return new List<FileType>()
            {
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.png,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.bmp,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.webp,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.jpg,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.jpeg,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.jpg,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.png,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.gif,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.tif,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.svg,
            };
        }
        public static List<FileType> GetFileType()
        {
            return new List<FileType>()
            {
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.Word,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.pdf,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.Excel,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.csv,
                GeneralDLL.DTO.SSOServices.SystemEnums.FileType.Excel,
            };
        }
    }
}