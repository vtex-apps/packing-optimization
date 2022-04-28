using System;
using System.Collections.Generic;
using System.Text;

namespace PackingOptimization.Models
{
    public class UnitDimension
    {
        public double weight { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
    }

    public class VtexItem
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string modal { get; set; }
        public object groupId { get; set; }
        public double unitPrice { get; set; }
        public UnitDimension unitDimension { get; set; }
    }

    public class Geolocation
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class Origin
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
        public bool residential { get; set; }
        public Geolocation coordinates { get; set; }
    }

    public class Destination
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
        public bool residential { get; set; }
        public Geolocation coordinates { get; set; }
    }

    public class GetRatesRequest
    {
        public List<VtexItem> items { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public string currency { get; set; }
        public DateTime shippingDateUTC { get; set; }
    }
}
