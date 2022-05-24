using PackingOptimization.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.ContainerPacking.Algorithms;

namespace PackingOptimization.Services
{
    public class MultiboxPackService
    {
        public static List<ContainerPackingResult> multiboxPack(BasicContainerPackingRequest packingRequest, List<Container> containerList)
        {
            List<int> algoTypeID = new List<int>();
            algoTypeID.Add(1);
            
            List<ContainerPackingResult> packResult = recursivePack(containerList, packingRequest.ItemsToPack, algoTypeID, new List<ContainerPackingResult>());

            return packResult;
        }

        private static List<ContainerPackingResult> recursivePack(List<Container> containerList, List<Item> itemsToPack, List<int> algoTypeID, List<ContainerPackingResult> results)
        {
            // Base Case
            if (itemsToPack.Capacity == 0)
            {
                return results;
            }

            List<ContainerPackingResult> packResult = PackingService.Pack(containerList, itemsToPack, algoTypeID);

            decimal maxItemVolPacked = -1;
            ContainerPackingResult previousContainerPointer = new ContainerPackingResult();

            // Finds max value of PercentItemVolumePacked and PercentContainerVolumePacked
            foreach (ContainerPackingResult iterPackResult in packResult)
            {
                // If more item packed, set previous pointer to current container
                if (iterPackResult.AlgorithmPackingResults[0].PercentItemVolumePacked > maxItemVolPacked)
                {
                    maxItemVolPacked = iterPackResult.AlgorithmPackingResults[0].PercentItemVolumePacked;
                    previousContainerPointer = iterPackResult;
                }
                // If equal volume item packed, set pointer to greater container volume packed
                // Greater container volume packed and equal volume item packed means that the box is smaller
                else if (
                    iterPackResult.AlgorithmPackingResults[0].PercentItemVolumePacked == maxItemVolPacked &&
                    iterPackResult.AlgorithmPackingResults[0].PercentContainerVolumePacked > previousContainerPointer.AlgorithmPackingResults[0].PercentContainerVolumePacked
                )
                {
                    previousContainerPointer = iterPackResult;
                }
            }

            // No boxes can fit this item
            // Prevent stack overflow
            if (previousContainerPointer.AlgorithmPackingResults[0].PackedItems.Capacity == 0)
            {
                return results;
            }

            results.Add(previousContainerPointer);

            // Recursively pack the remainder items
            return recursivePack(containerList, previousContainerPointer.AlgorithmPackingResults[0].UnpackedItems, algoTypeID, results);
        }
    }
}