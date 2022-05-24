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
    using PackingOptimization.Data;
    using PackingOptimization.Services;
    using CromulentBisgetti.ContainerPacking;
    using CromulentBisgetti.ContainerPacking.Entities;
    using CromulentBisgetti.ContainerPacking.Algorithms;

    public class RoutesController : Controller
    {

        private readonly IMerchantSettingsRepository _merchantSettingsRepository;
        private MerchantSettings _merchantSettings;

        public RoutesController(IMerchantSettingsRepository merchantSettingsRepository)
        {
            this._merchantSettingsRepository = merchantSettingsRepository ?? throw new ArgumentNullException(nameof(merchantSettingsRepository));
        }

        public async Task<IActionResult> pack()
        {
            this._merchantSettings = await _merchantSettingsRepository.GetMerchantSettings();

            List<Container> containerList = new List<Container>();
            foreach (ContainerObject container in this._merchantSettings.ContainerList) {
                containerList.Add(new Container(container.Id, container.Length, container.Width, container.Height));
            }

            var bodyAsText = await new System.IO.StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            BasicContainerPackingRequest packingRequest = JsonConvert.DeserializeObject<BasicContainerPackingRequest>(bodyAsText);

            List<int> algoTypeID = new List<int>();
            algoTypeID.Add(1);

            List<ContainerPackingResult> result = PackingService.Pack(containerList, packingRequest.ItemsToPack, algoTypeID);
            return Json(result);
        }

        public async Task<IActionResult> multiPack()
        {
            this._merchantSettings = await _merchantSettingsRepository.GetMerchantSettings();

            List<Container> containerList = new List<Container>();
            foreach (ContainerObject container in this._merchantSettings.ContainerList) {
                containerList.Add(new Container(container.Id, container.Length, container.Width, container.Height));
            }

            var bodyAsText = await new System.IO.StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            List<BasicContainerPackingRequest> packingRequestList = JsonConvert.DeserializeObject<List<BasicContainerPackingRequest>>(bodyAsText);
            List<int> algoTypeID = new List<int>();
            algoTypeID.Add(1);
            List<List<ContainerPackingResult>> resultList = new List<List<ContainerPackingResult>>();

            foreach (BasicContainerPackingRequest packingRequest in packingRequestList) {
                resultList.Add(PackingService.Pack(containerList, packingRequest.ItemsToPack, algoTypeID));
            }

            return Json(resultList);
        }

        public async Task<IActionResult> packAll()
        {
            this._merchantSettings = await _merchantSettingsRepository.GetMerchantSettings();

            List<Container> containerList = new List<Container>();
            foreach (ContainerObject container in this._merchantSettings.ContainerList) {
                containerList.Add(new Container(container.Id, container.Length, container.Width, container.Height));
            }

            var bodyAsText = await new System.IO.StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            BasicContainerPackingRequest packingRequest = JsonConvert.DeserializeObject<BasicContainerPackingRequest>(bodyAsText);

            List<ContainerPackingResult> multiboxPack = MultiboxPackService.multiboxPack(packingRequest, containerList);

            ContainerPackingResponse containerPackingResponse = new ContainerPackingResponse(multiboxPack, containerList);

            return Json(containerPackingResponse);
        }
    }
}
