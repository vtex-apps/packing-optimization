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
            BasicContainerPackingRequest packingRequest = JsonConvert.DeserializeObject<BasicContainerPackingRequest>(bodyAsText);

            List<int> algoTypeID = new List<int>();
            algoTypeID.Add(1);

            List<ContainerPackingResult> result = PackingService.Pack(packingRequest.Containers, packingRequest.ItemsToPack, algoTypeID);
            return Json(result);
        }

        public async Task<IActionResult> multiPack()
        {
            var bodyAsText = await new System.IO.StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            List<BasicContainerPackingRequest> packingRequestList = JsonConvert.DeserializeObject<List<BasicContainerPackingRequest>>(bodyAsText);
            List<int> algoTypeID = new List<int>();
            algoTypeID.Add(1);
            List<List<ContainerPackingResult>> resultList = new List<List<ContainerPackingResult>>();

            foreach (BasicContainerPackingRequest packingRequest in packingRequestList) {
                resultList.Add(PackingService.Pack(packingRequest.Containers, packingRequest.ItemsToPack, algoTypeID));
            }

            return Json(resultList);
        }
    }
}
