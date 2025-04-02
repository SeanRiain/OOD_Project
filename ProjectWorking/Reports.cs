﻿using System;
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

        public override string ToString()
        {
            return $"Current Status of Area: {Status} || Team: {TeamNumber} || Time Logged: {TimeLogged} || Additional Details: {ResponderMessage}";
        }
    }

    public class CivillianReports
    {
        int SocialSecurityNumber { get; set; }
        DateTime TimeLogged { get; set; }
        string CivillianMessage { get; set; }

        public override string ToString()
        {
            return $"Citzen's SSN: {SocialSecurityNumber} || Time Logged: {TimeLogged} || Report: {CivillianMessage}";
        }
    }
}
