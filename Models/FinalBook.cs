using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5NETD.Models
{
    public class FinalBook
    {
        public int ID { get; set; }
        public string title { get; set; }

        public int isbn { get; set; }

        public int version { get; set; }

        public int price { get; set; }

        public string condition { get; set; }
    }
}
