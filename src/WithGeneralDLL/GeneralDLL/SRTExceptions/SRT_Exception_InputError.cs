﻿// Ignore Spelling: SRT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.SRTExceptions
{
    public class SRT_Exception_InputError : Exception
    {
        public SRT_Exception_InputError(string message) : base(message) { }
        public SRT_Exception_InputError() { }
    }
}
