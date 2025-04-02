using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWorking
{
    public class GeneralAlerts
    {
        string TargetRecipients { get; set; }
        //string NatureOfAlert { get; set; }
        string[] NatureOfAlert = new string[4] { "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" };

        string GeneralAlertMessage { get; set; }

        DateTime TimeSent { get; set; }
        public override string ToString()
        {
            return $"Alert Addressed to: {TargetRecipients} || Alert Category: {NatureOfAlert} || Additional Details: {GeneralAlertMessage} || Time Issued: {TimeSent}";
        }
    }

    public class RegionAlerts
    {
        string[] Region = new string[4] { "A", "B", "C", "D" } ;

        string[] NatureOfAlert = new string[4] { "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" };

        string RegionAlertMessage { get; set; }

        DateTime TimeSent { get; set; }

        public override string ToString()
        {
            return $"All Teams In Region: {Region} || Alert Category: {NatureOfAlert} || Additional Details: {RegionAlertMessage} || Time Issued: {TimeSent}";
        }
    }
}
