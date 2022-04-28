namespace service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Vtex.Api.Context;
    using Newtonsoft.Json;
    using PackingOptimization.Models;
    using CromulentBisgetti.ContainerPacking;
    using CromulentBisgetti.ContainerPacking.Entities;
    using CromulentBisgetti.ContainerPacking.Algorithms;

    public class RoutesController : Controller
    {

        public RoutesController()
        {
        }

        public async Task<IActionResult> pack()
        {
            var bodyAsText = await new System.IO.StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            ContainerPackingRequest packingRequest = JsonConvert.DeserializeObject<ContainerPackingRequest>(bodyAsText);

            List<ContainerPackingResult> result = PackingService.Pack(packingRequest.Containers, packingRequest.ItemsToPack, packingRequest.AlgorithmTypeIDs);
            return Json(result);
        }
    }
}
