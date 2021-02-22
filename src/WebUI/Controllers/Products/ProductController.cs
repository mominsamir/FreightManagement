using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Products
{
    [Authorize]
    public class ProductController : ApiControllerBase
    {
    }
}
