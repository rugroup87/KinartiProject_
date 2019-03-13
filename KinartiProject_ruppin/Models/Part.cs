using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Part
    {
        public string PartID { get; set; }
        public string PartNum { get; set; }
        public string PartName { get; set; }
        public int PartSetNumber { get; set; }
        public string PartStatus { get; set; }
        public string PartQuantity { get; set; }
        public string PartMaterial { get; set; }
        public string PartColor { get; set; }
        public int PartLength { get; set; }
        public int PartWidth { get; set; }
        public int PartThickness { get; set; }
        public int AdditionToLength { get; set; }
        public int AdditionToWidth { get; set; }
        public int AdditionToThickness { get; set; }
        public string PartCropType { get; set; }
        public string PartCategory { get; set; }
        public string PartComment { get; set; }
    }
}