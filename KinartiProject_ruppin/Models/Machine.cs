using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Machine
    {
        public int MachineNum { get; set; }
        public string MachineName { get; set; }

        public Machine()
        {

        }
        public Machine(int machineNum, string machineName)
        {
            MachineNum = machineNum;
            MachineName = machineName;
        }

        public List<Machine> GetAllMachines()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadMachine("KinartiConnectionString");
        }
    }
}