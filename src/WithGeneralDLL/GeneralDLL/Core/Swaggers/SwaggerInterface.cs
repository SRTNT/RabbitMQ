using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.Swaggers
{
    public interface SwaggerInterface
    {
        public void ApplySetting(SwaggerGenOptions options);
    }
}
