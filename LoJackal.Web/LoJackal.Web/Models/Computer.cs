using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoJackal.Web.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string IPAddress { get; set; }
        public DateTime LastCommunicated { get; set; }
    }
}