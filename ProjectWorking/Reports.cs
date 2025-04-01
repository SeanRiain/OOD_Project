using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWorking
{
    public class ResponderReports
    {
        string[] Status = new string[4] { "In Need of Assistance", "Situation Being Managed", "Situation De-Escalated", "Danger" };
        int TeamNumber { get; set; }
        DateTime TimeLogged { get; set; }
        string ResponderMessage { get; set; }

    }

    public class CivillianReports
    {
        int SocialSecurityNumber { get; set; }
        DateTime TimeLogged { get; set; }
        string CivillianMessage { get; set; }

    }
}
