using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWorking
{
    public class GeneralAlerts
    {
        string TargetRecipient { get; set; }
        string NatureOfAlert { get; set; }

        string Message { get; set; }

        DateTime TimeSent { get; set; }
    }

    public class RegionAlerts
    {
        string[] Region = new string[4] { "A", "B", "C", "D" } ;
        string NatureOfAlert { get; set; }

        string Message { get; set; }

        DateTime TimeSent { get; set; }
    }
}
