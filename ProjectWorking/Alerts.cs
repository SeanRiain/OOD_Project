using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWorking
{
    public class GeneralAlerts
    {
        public string TargetRecipients { get; set; }

        //public string[] NatureOfAlert = new string[4] { "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" };

        public string NatureOfAlert { get; set; }

        public string GeneralAlertMessage { get; set; }
        public DateTime TimeSent { get; set; }

        public GeneralAlerts(string TargetRecipientsCon, string NatureOfAlertCon, string GeneralAlertMessageCon, DateTime TimeSentCon)
        {
            TargetRecipients = TargetRecipientsCon;
            NatureOfAlert = NatureOfAlertCon;
            GeneralAlertMessage = GeneralAlertMessageCon;
            TimeSent = TimeSentCon;
        }
        public override string ToString()
        {
            return $"Alert Addressed to: {TargetRecipients} || Alert Category: {NatureOfAlert} || Additional Details: {GeneralAlertMessage} || Time Issued: {TimeSent}";
        }
    }

    public class RegionAlerts
    {
        //string[] Regions = new string[4] { "A", "B", "C", "D" } ;

        string[] NatureOfAlert = new string[4] { "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" };

        public string Region { get; set; }
        //public string NatureOfAlert { get; set; }

        string RegionAlertMessage { get; set; }

        DateTime TimeSent { get; set; }

        public RegionAlerts(string RegionCon, string NatureOfAlertCon, string MessageCon, DateTime TimeSentCon)
        {
            //if (!Regions.Contains(region)) throw new ArgumentException("Invalid Region");
            //if (!NatureOfAlerts.Contains(natureOfAlert)) throw new ArgumentException("Invalid NatureOfAlert");

            //Region = RegionCon;
            //NatureOfAlert = NatureOfAlertCon;

            RegionAlertMessage = MessageCon;
            TimeSent = TimeSentCon;
        }

        public override string ToString()
        {
            return $"All Teams In Region: {Region} || Alert Category: {NatureOfAlert} || Additional Details: {RegionAlertMessage} || Time Issued: {TimeSent}";
        }
    }
}
