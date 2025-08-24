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
    public class TXTFile : BasicFile
    {
        public TXTFile(string _PathAndFile) : base(".txt", _PathAndFile)
        { }

        public TXTFile(string _Path, string _NameFile) : base(".txt", _Path, _NameFile)
        { }
    }
}