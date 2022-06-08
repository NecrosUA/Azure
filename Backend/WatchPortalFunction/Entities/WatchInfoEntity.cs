using System;
using System.Collections.Generic;
using System.Text;

namespace WatchPortalFunction.Entities
{
    public class WatchInfoEntity
    {
        public string Manufacturer { get; set; }
        public string CaseType { get; set; }
        public string Bezel { get; set; }
        public string Dial { get; set; }
        public string CaseFinish { get; set; }
        public int? Jewels { get; set; }
    }
}
