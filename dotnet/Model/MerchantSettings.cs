using System;
using System.Collections.Generic;

namespace PackingOptimization.Models
{
    public class MerchantSettings
    {
        public List<ContainerObject> ContainerList { get; set; } = new List<ContainerObject>();
        public string AccessKey { get; set; } = "";
    }

    public class ContainerObject
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }

        public ContainerObject() {}
        public ContainerObject(int Id, int Length, int Width, int Height, string Description) {
            this.Id = Id;
            this.Length = Length;
            this.Width = Width;
            this.Height = Height;
            this.Description = Description;
        }
    }
}
