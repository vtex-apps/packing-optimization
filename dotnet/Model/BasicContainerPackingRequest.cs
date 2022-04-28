using CromulentBisgetti.ContainerPacking.Entities;
using System.Collections.Generic;

namespace PackingOptimization.Models
{
	public class BasicContainerPackingRequest
	{
		public List<Container> Containers { get; set; }

		public List<Item> ItemsToPack { get; set; }
	}
}