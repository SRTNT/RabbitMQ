// Ignore Spelling: SRT admin dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GeneralDLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GeneralDLL.Core.ErrorController.Repository;
using GeneralDLL.Core.ENV;
using GeneralDLL.Core.RedisCash.Repository;

namespace GeneralDLL.Core.SYS_Controllers
{
    public class SRTBaseCashControllerSystem : SRTBaseControllerSystem
    {
        #region Injection Fields
        protected IGeneralDLLRepository SRT_Base_CashRedis => HttpContext.RequestServices.GetService<IGeneralDLLRepository>();
        #endregion

        #region Constructors
        public SRTBaseCashControllerSystem(ILogger<SRTBaseCashControllerSystem> logger):base(logger)
        { }
        #endregion
    }
}