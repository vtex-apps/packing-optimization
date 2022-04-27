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
    using CromulentBisgetti.ContainerPacking;
    using CromulentBisgetti.ContainerPacking.Entities;
    using CromulentBisgetti.ContainerPacking.Algorithms;

    public class RoutesController : Controller
    {

        public RoutesController()
        {
        }

        public string test()
        {
            return "string";
        }

        public string pack()
        {
            List<Container> containers = new List<Container>();
            containers.Add(new Container(1, 2, 2, 3));

            List<Item> itemsToPack = new List<Item>();
            itemsToPack.Add(new Item(1, 2, 2, 2, 2));

            List<int> algorithms = new List<int>();
            algorithms.Add((int)AlgorithmType.EB_AFIT);

            List<ContainerPackingResult> result = PackingService.Pack(containers, itemsToPack, algorithms);
            Console.WriteLine(JsonConvert.SerializeObject(result));
            return JsonConvert.SerializeObject(result);
        }
    }
}
