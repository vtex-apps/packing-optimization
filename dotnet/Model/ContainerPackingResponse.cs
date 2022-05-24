using CromulentBisgetti.ContainerPacking.Entities;
using System.Collections.Generic;

namespace PackingOptimization.Models
{
	public class ContainerPackingResponse
	{
		public List<ContainerPackingResult> PackedResults { get; set; }
		public List<Container> Containers { get; set; }

		public ContainerPackingResponse() {}

		public ContainerPackingResponse(List<ContainerPackingResult> PackedResults, List<Container> Containers)
		{
			this.PackedResults = PackedResults;
			this.Containers = Containers;
		}
	}
}