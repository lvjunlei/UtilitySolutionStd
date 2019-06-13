using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.Authority.Test.Swaggers
{
    public class OpenApiContact : Swashbuckle.AspNetCore.Swagger.Contact
    {
        public new Uri Url { get; set; }
    }
}
