﻿using System;
using System.Collections.Generic;

namespace PackingOptimization.Models
{
    public class MerchantSettings
    {
        public List<ContainerObject> ContainerList { get; set; } = new List<ContainerObject>();
    }

    public class ContainerObject
    {
        public string Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }

        public ContainerObject() {}
        public ContainerObject(string Id, int Length, int Width, int Height, string Description) {
            this.Id = Id;
            this.Length = Length;
            this.Width = Width;
            this.Height = Height;
            this.Description = Description;
        }
    }
}