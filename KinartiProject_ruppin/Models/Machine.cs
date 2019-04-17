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
    }
}