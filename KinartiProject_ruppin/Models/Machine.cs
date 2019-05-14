using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Machine
    {
        public string MachineNum { get; set; }
        public string MachineName { get; set; }

        public Machine()
        {

        }
        public Machine(string _machineNum, string _machineName)
        {
            MachineNum = _machineNum;
            MachineName = _machineName;
        }

        public List<Machine> GetAllMachines()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadMachine("KinartiConnectionString");
        }
        

        public List<TempObjForDashboard> GetMachinesCurrentdetails()
        {
            DBServices dbs = new DBServices();
            List<TempObjForDashboard> Lobj = dbs.GetMachinesCurrentdetails();
            return Lobj;
        }
    }

    public class TempObjForDashboard
    {

        public string ProjectName { get; set; }
        public float ProjectNum { get; set; }
        public string ItemName { get; set; }
        public string ItemNum { get; set; }
        public string GroupName { get; set; }
        public string MachineName { get; set; }
        public int PartCount { get; set; }
        public int ScannedPartCount { get; set; }

        public TempObjForDashboard()
        {

        }

        public TempObjForDashboard(string ProjectName, float ProjectNum, string ItemName, string ItemNum, string GroupName, string MachineName, int PartCount, int ScannedPartCount)
        {
            this.ProjectName = ProjectName;
            this.ProjectNum = ProjectNum;
            this.ItemName = ItemName;
            this.ItemNum = ItemNum;
            this.GroupName = GroupName;
            this.MachineName = MachineName;
            this.PartCount = PartCount;
            this.ScannedPartCount = ScannedPartCount;
        }
    }
}