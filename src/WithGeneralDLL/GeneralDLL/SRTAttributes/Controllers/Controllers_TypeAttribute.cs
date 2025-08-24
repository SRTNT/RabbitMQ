using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.SRTAttributes.Controllers
{
    public class Controllers_TypeAttribute : Attribute
    {
        public enum TypeController
        {
            None,
            Admin,
            Client,
            Expert,
            ClientExpert,
            ClientExpertNone,
            Internal
        }

        public Controllers_TypeAttribute(TypeController ControllerType)
        { this.ControllerType = ControllerType; }

        public TypeController ControllerType { get; set; }
    }
}