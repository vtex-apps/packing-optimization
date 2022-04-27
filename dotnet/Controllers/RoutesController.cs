namespace service.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Vtex.Api.Context;

    public class RoutesController : Controller
    {

        public RoutesController()
        {
        }

        public string test()
        {
            return "string";
        }
    }
}
