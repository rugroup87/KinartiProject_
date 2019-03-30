using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Part
    {
        //public string PartID { get; set; }
        public string PartBarCode { get; set; }
        public string PartNum { get; set; }
        public string PartName { get; set; }
        public float ProjectNum { get; set; }
        public string ItemNum { get; set; }
        public string GroupName { get; set; }
        public string PartKantim { get; set; }
        public string PartFirstMachine { get; set; }
        public string PartSecondMachine { get; set; }
        public int PartSetNumber { get; set; }
        public string PartStatus { get; set; }
        public int PartQuantity { get; set; }
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


        //בנאי מלא
        public Part(string _partBarCode, string _partNum, string _partName,
            float _projectNum, string _itemNum, string _groupName, string _partKantim,
            string _partFirstMachine, string _partSecondMachine, int _partSetNumber,
            string _partStatus, int _partQuantity, string _partMaterial, string _partColor,
            int _partLength, int _partWidth, int _partThickness, int _additionToLength,
            int _additionToWidth, int _additionToThickness, string _partCropType,
            string _partCategory, string _partComment)
        {
            //PartID = _partID;
            PartBarCode = _partBarCode;
            PartNum = _partNum;
            PartName = _partName;
            ProjectNum = _projectNum;
            ItemNum = _itemNum;
            GroupName = _groupName;
            PartKantim = _partKantim;
            PartFirstMachine = _partFirstMachine;
            PartSecondMachine = _partSecondMachine;
            PartSetNumber = _partSetNumber;
            PartStatus = _partStatus;
            PartQuantity = _partQuantity;
            PartMaterial = _partMaterial;
            PartColor = _partColor;
            PartLength = _partLength;
            PartWidth = _partWidth;
            PartThickness = _partThickness;
            AdditionToLength = _additionToLength;
            AdditionToWidth = _additionToWidth;
            AdditionToThickness = _additionToThickness;
            PartCropType = _partCropType;
            PartCategory = _partCategory;
            PartComment = _partComment;
        }

        public Part(float _projectNum, string _itemNum)
        {
            ProjectNum = _projectNum;
            ItemNum = _itemNum;
        }

        //בנאי ריק
        public Part()
        {

        }

        //public List<Part> GetAllPart()
        //{
        //    DBServices dbs = new DBServices();
        //    List<Part> lp = new List<Part>();
        //    lp = dbs.GetAllPart();
        //    return lp;
        //}

        public void StatusChange(Objectdata obj)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(obj.newStatus, obj.projNumStatus, obj.itemNumStatus, obj.partNumStatus);
        }

        public Part[] GetPartFromItem(float projNumStatus, string itemNumStatus)
        {
            DBServices dbs = new DBServices();
            return dbs.GetPartFromItem(projNumStatus, itemNumStatus);
        }

    }
}