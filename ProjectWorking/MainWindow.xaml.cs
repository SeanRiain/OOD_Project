﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

//Please note, GenAI (xAI) was used in this project, however the vast majority of the code it generated - outside of small syntax corrections -
//has been replaced, edited or commented out with the DrawEliplse method being the only remaining section that contains important code I could not write myself.
//Main examples of commented out AI code can be found at lines //317, 399, and 532

//It should be noted that the second to last version of this project will contain substantially more comments than the first.
//These comments include notes to self, logical questions, reminders etc. I have edited them out of the final submission for the sake of brevity.

namespace ProjectWorking
{
    public partial class MainWindow : Window
    {
        public string SelectedRegion;
        public string[] Regions = new string[4] {"RegionA", "RegionB", "RegionC", "RegionD"};
        string[] AlertTypes = new string[4] { "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" };
        string[] TeamOptions = new string[7]
        {
            "Teams In Transit", "All Teams", "Teams in Large Convoys", "Isolated Teams","Rescue Teams", "Teams With Firefighting Support", "Aid Teams"
        };
        public string[] BlockNames = new string[12] 
        {
         "Akhmedov Street", "Aliev Street", "Adiyev Street", "Turishcheva Street", "Arsamakov Street", "Troshev Street", "Agayev Street",
         "Sarkisov Street", "Karatsuba Street", "Bakaev Street", "Yefimova Street", "Ozdoyev Street"
        };
        public string[] RegionAlertTypes = new string[7]
        {"Electricity Grid Damage", "Water Supply Issues", "No Phone Signal", "Roads Blocked", "Flooding Damage", "Major Earthquake Damage", "Unrest/Active Riot"};

        string[] PossibleMessagesR = new string[12] 
        {
            "Poor connection will result in inconsistent general and regional alerts for the time being, all teams will be updated once/if connection becomes reliable.",
            "Connection issues resolved, all updates should be recieved on either end at once until something changes.",

            "As per negotiations, it is safe for all teams to approach DESIGNATED combatants until further notice.", 
            "Due to diplomatic failures, it is presently considered a risk for any team to approach any combatant. If approached or fired upon retreat immediately.",

            "Widespread electricty blackouts are being reported by emergency workers and civillians, excercise caution and utilize low light equipment if working into the night.", 
            "Issues with the main power grid have been resolved, blackouts should not be occuring outside of isolated incidents - report any electricty grid issues encountered.",

            "Firefighting support currently available for any team that requires it.",
            "Firefighting support currently unavailable - a reminder to NOT attempt to intervene in significant fire related emergencies unless specifically trained.",

            "Armed escort currently available for any team that requires it.",
            "Armed escort support currently unavailable - a reminder to NOT attempt to intervene in emergencies of civil unrest or looting alone.",

            "All quakes are predicted to have stopped, anything felt from this point on should only be a tremor and its considered relatively safe to operate.",
            "Unpredictable earthquakes are still occuring area-wide, do not attempt to travel under ANY circumstances and do not assist the public unless clearly safe to do so."
        };
        string[] PossibleRegionMessages = new string[12]
        {
            "Poor connection will result in inconsistent regional alerts for the time being, all teams in the area will be updated once/if connection becomes reliable.",
            "Local connection issues resolved, all updates should be recieved on either end at once until something changes.",

            "As per negotiations, it is safe for all teams to approach DESIGNATED combatants deployed in listed region until further notice.",
            "Due to diplomatic failures, it is presently considered a risk for teams in the listed region to approach any combatant present. If approached or fired upon retreat immediately.",

            "Local electricty blackouts are being reported by emergency workers and civillians, excercise caution and utilize low light equipment if working into the night.",
            "Issues with the local power grid have been resolved, blackouts should not be occuring outside of isolated incidents - report any electricty grid issues encountered.",

            "Firefighting support currently available in listed region for any team that requires it.",
            "Firefighting support locally unavailable - a reminder to NOT attempt to intervene in significant fire related emergencies unless specifically trained.",

            "Armed escort currently available in listed region for any team that requires it.",
            "Armed escort support currently unavailable regionally - a reminder to NOT attempt to intervene in emergencies of civil unrest or looting alone.",

            "Quakes are predicted to have stopped in the regional area, anything felt from this point on should only be a tremor and its considered relatively safe to operate.",
            "Quakes of significant scale are predicted and reported to be ongoing in the area, shelter in place."
        };

        string[] PossibleMessagesC = new string[12]
        {
            "Hey people in this area have had trouble connecting to our side of the app, is there something wrong?",
            "From what we can tell our messages are going throuhg properly now.",

            "Theres been soldiers moving through our neighbourhood for the last few hours, they seemed friendly and relaxed.",
            "this neighbourhood has been occupied by the enemy and none of us are allowed to leave our houses! i dont think its safe for emergency workers to enter",

            "Our access to power has been really bad recently, power outages every few hours. Nobody has been able to check the block's energy station but messing around with home power grids doesnt help.",
            "Power supply seems stable here - Ive heard reports from a friend who had to leave the suburbs that they were in total blackout so I thought I'd log that we're fine for the time being.",

            "Putting it on record that ambulances and fire crews have been showing up pretty quick after we've called over the last few weeks, thank god that got sorted.",
            "People here havent heard from an emergency worker in days! What the hell is going on? Responses were bad since this mess started but now we're getting radio silence! Help us!",

            "Seen cops and our soldiers in the neighbourhood recently, hope they're doing alright.",
            "for gods sakes somebody get the police and emergency lines sorted out we havent seen any in WEEKS! violence and looting is getting really bad we need them or the army here yesterday!!!",

            "Havent felt any termors for the past day, unless something is predicted officially i think we're safe from quakes for now",
            "Getting serious quakes every few minutes! Unless you can send helicopters dont come here!! Sheltering in place"
        };


        public Canvas ActiveCanvas;

        private List<Node> Nodes = new List<Node>(); //List of the Node class

        //for creating responder reports
        string status = null;
        public int TeamNumber = 0;
        public string Coordindates = null;
        public string Message = null;

        //Random number generators
        public Random Regionrng = new Random();
        public Random Regionrng2 = new Random();
        public Random Userrng = new Random();
        public Random Userrng2 = new Random();
        public Random ReportsCrng = new Random();
        public Random ReportsRrng = new Random();
        public Random AlertsGrng = new Random();
        public Random AlertsRrng = new Random();
        public Random TeamNorng = new Random();
        public Random TeamTyperng = new Random();
        public Random UserLocationrng = new Random();
        public Random Messagerng = new Random();
        public Random SSNrng = new Random();



        public List<CivillianReports> CivillianReportsSet1 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet2 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet3 = new List<CivillianReports>();

        private List<ResponderReports> ResponderReports = new List<ResponderReports>();

        public List<GeneralAlerts> GeneralAlertsSet1 = new List<GeneralAlerts>();
        public List<GeneralAlerts> GeneralAlertsSet2 = new List<GeneralAlerts>();
        public List<GeneralAlerts> GeneralAlertsSet3 = new List<GeneralAlerts>();

        //public List<RegionAlerts>[] RegionAlerts = new List<RegionAlerts>//[4] {new List<RegionAlerts>(), new List<RegionAlerts>(), new List<RegionAlerts>(), new List<RegionAlerts>() };
        public List<RegionAlerts> RegionAlertsRegionA = new List<RegionAlerts>();
        public List<RegionAlerts> RegionAlertsRegionB = new List<RegionAlerts>();
        public List<RegionAlerts> RegionAlertsRegionC = new List<RegionAlerts>();
        public List<RegionAlerts> RegionAlertsRegionD = new List<RegionAlerts>();


        public MainWindow()
        {
            InitializeComponent();

            // Initialize CivillianReports sets

            CivillianReportsSet1.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 10, 17), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet1.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 10, 18), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet1.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 10, 19), PossibleMessagesC[Messagerng.Next(0, 12)]));

            CivillianReportsSet2.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 11, 10), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet2.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 11, 11), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet2.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2014, 11, 12), PossibleMessagesC[Messagerng.Next(0, 12)]));

            CivillianReportsSet3.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2015, 12, 20), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet3.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2015, 12, 21), PossibleMessagesC[Messagerng.Next(0, 12)]));
            CivillianReportsSet3.Add(new CivillianReports(SSNrng.Next(0, 999), new DateTime(2015, 12, 22), PossibleMessagesC[Messagerng.Next(0, 12)]));


            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));


            RegionAlertsRegionA.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionA.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionA.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

            RegionAlertsRegionB.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionB.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionB.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

            RegionAlertsRegionC.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionC.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionC.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

            RegionAlertsRegionD.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionD.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));
            RegionAlertsRegionD.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));

        }





        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //random region on start
            SelectedRegion = Regions[Regionrng.Next(0, Regions.Length)]; //goes through the regions array start to finish to select a region upon startup
            RegionMap.Source = new BitmapImage(new Uri($"/images/{SelectedRegion}.png", UriKind.Relative)); //this works as the regions in the region array have the same name
            //as the images that are equvilant to them
            tbxRegionDisplayed.Text = SelectedRegion;


            //random user location on start
            tbxCurrentRegion.Text = SelectedRegion;
            tbxNearestUserNode.Text = BlockNames[UserLocationrng.Next(0, 12)];
            tbxUserCoordinates.Text = $"{Userrng.Next(0, 150)},{Userrng.Next(0, 150)}"; 
                                                            
            BlankCanvas_Unloaded();
            CanvasRegionA_Unloaded();
            CanvasRegionB_Unloaded();
            CanvasRegionC_Unloaded();
            CanvasRegionD_Unloaded();

            RegionSelect(); //Required to generate initial set of nodes and to support if statements below.

            if (ActiveCanvas == CanvasRegionA)
            {
                CanvasRegionA_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (ActiveCanvas == CanvasRegionB)
            {
                CanvasRegionB_loaded();

                CanvasRegionA_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (ActiveCanvas == CanvasRegionC)
            {
                CanvasRegionC_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionA_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (ActiveCanvas == CanvasRegionD)
            {
                CanvasRegionD_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionA_Unloaded();
            }

            var CReportSetsWindowLoaded = new List<List<CivillianReports>> { CivillianReportsSet1, CivillianReportsSet2, CivillianReportsSet3 };
            lbxReportsC.ItemsSource = CReportSetsWindowLoaded[ReportsCrng.Next(CReportSetsWindowLoaded.Count)];

            var GAlertsSetsWindowLoaded = new List<List<GeneralAlerts>> { GeneralAlertsSet1, GeneralAlertsSet2, GeneralAlertsSet3 };
            lbxGeneralAlerts.ItemsSource = GAlertsSetsWindowLoaded[AlertsGrng.Next(GAlertsSetsWindowLoaded.Count)];

            lbxRegionAlerts1.ItemsSource = RegionAlertsRegionA;
            lbxRegionAlerts2.ItemsSource = RegionAlertsRegionB;
            lbxRegionAlerts3.ItemsSource = RegionAlertsRegionC;
            lbxRegionAlerts4.ItemsSource = RegionAlertsRegionD;

        }





        //Method to clear everything on screen and simulates the scenario process
        private void btnTesting_Click(object sender, RoutedEventArgs e)
        {

            Nodes.Clear(); //Clear the list of currently recorded nodes

            //random region on click
            SelectedRegion = Regions[Regionrng2.Next(0, Regions.Length)];
            tbxRegionDisplayed.Text = SelectedRegion; //Exact same process as above
            RegionMap.Source = new BitmapImage(new Uri($"/images/{SelectedRegion}.png", UriKind.Relative));


            //random user location on click
            tbxCurrentRegion.Text = SelectedRegion;
            tbxNearestUserNode.Text = BlockNames[UserLocationrng.Next(0, 12)]; 
            tbxUserCoordinates.Text = $"{Userrng2.Next(0, 150)},{Userrng2.Next(0, 150)}"; 




            // Randomly choose one of the CivillianReports sets
            //Adding one of the lists of civillian reports to the civillian reports list box
            var CReportSets = new List<List<CivillianReports>> { CivillianReportsSet1, CivillianReportsSet2, CivillianReportsSet3 };
            lbxReportsC.ItemsSource = CReportSets[ReportsCrng.Next(CReportSets.Count)];
            //a var is similar to a list without a name
            //"the source of content for this box is the version of this var that is affected by a random number from this list of numbers that is equivilant to several list objects"




            //Clear and draw nodes
            BlankCanvas.Children.Clear(); 
            CanvasRegionA.Children.Clear();
            CanvasRegionB.Children.Clear();
            CanvasRegionC.Children.Clear();
            CanvasRegionD.Children.Clear();

            #region machine code node drawing
            //hard code positions for drawing elipse -irrelevant due to regionselect()
            //double[,] NodePositions = new double[,]
            //{
            //    { 10, 50 },
            //    { 50, 100 },
            //    { 100, 150 },
            //    { 150, 200 }
            //};
            //for (int counter = 0; counter < NodePositions.GetLength(0); counter++) //As long as the counter is less than the amount of elements in the array do the following
            //{
            //    DrawEllipse(NodePositions[counter, 0], NodePositions[counter, 1], 50, 50); //this line NEEDS to be refactored or at least re-explained
            //}
            #endregion

            // Generate alerts
            GeneralAlertsSet1.Clear(); //Clear out any items previously loaded to these lists 
            GeneralAlertsSet2.Clear();
            GeneralAlertsSet3.Clear();

            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet1.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));

            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet2.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));

            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));
            GeneralAlertsSet3.Add(new GeneralAlerts(TeamOptions[TeamTyperng.Next(0, 7)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleMessagesR[Messagerng.Next(0, 12)], DateTime.Now));

            //Adding one of the lists of general alerts to the general alerts list box
            var GAlertsSets = new List<List<GeneralAlerts>> { GeneralAlertsSet1, GeneralAlertsSet2, GeneralAlertsSet3 };
            lbxGeneralAlerts.ItemsSource = GAlertsSets[AlertsGrng.Next(GAlertsSets.Count)];
            //note that this is the same process as with choosing a random civillian reports set


            //Generate new instances of RegionAlerts and randomly decide on which list box they should go in.
            //This should mean that seperate lists of RegionAlerts arent necessary, however that creates difficulty with making sure theres unique content in each box.

            #region machine code region alerts population
            /*
            for (int counter = 0; counter < 4; counter++)
            {
                RegionAlerts[counter].Clear(); //Clear all regionalerts sections once the counter value matches 
                RegionAlerts[counter].Add(new RegionAlerts(Regions[counter].Replace("Region", ""),
                AlertTypes[AlertsRrng.Next(0, 4)], $"{RegionAlertTypes}, In Region: {Regions[counter]}", DateTime.Now)); //This should be targeted as a core "rewrite area"

                switch (counter)
                {
                    case 0: lbxRegionAlerts1.ItemsSource = RegionAlerts[counter]; break;
                    case 1: lbxRegionAlerts2.ItemsSource = RegionAlerts[counter]; break;
                    case 2: lbxRegionAlerts3.ItemsSource = RegionAlerts[counter]; break;
                    case 3: lbxRegionAlerts4.ItemsSource = RegionAlerts[counter]; break;
                }
            }*/
            #endregion

            switch (SelectedRegion)
            {
                case "RegionA": RegionAlertsRegionA.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17)));; break;
                case "RegionB": RegionAlertsRegionB.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17))); ; break;
                case "RegionC": RegionAlertsRegionC.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17))); ; break;
                case "RegionD": RegionAlertsRegionD.Add(new RegionAlerts(Regions[Regionrng.Next(0, 4)], AlertTypes[AlertsGrng.Next(0, 4)], PossibleRegionMessages[Messagerng.Next(0, 12)], new DateTime(2014, 10, 17))); ; break;
            }

            lbxRegionAlerts1.ItemsSource = RegionAlertsRegionA;
            lbxRegionAlerts2.ItemsSource = RegionAlertsRegionB;
            lbxRegionAlerts3.ItemsSource = RegionAlertsRegionC;
            lbxRegionAlerts4.ItemsSource = RegionAlertsRegionD;

            #region logical code and planning
            //Possibly choose a random region and node for the user location - affects the top left text boxes

            //Create new civillian reports for *every* node and assign them to them so that lbxReportsC will display them upon click

            //Create a list of alerts of type "General", populate lbxGeneralAlerts with them
            //Create a list of alerts of type "Region", populate the 4 seperate region list boxes with them

            //[Some sort of random select of the civillian reports sets]
            //lbxReportsC.ItemsSource = [the set of civillian reports chosen]
            #endregion
            #region RegionSelect Original Version
            ///* if (SelectedRegion == null)
            // {
            //     SelectedRegion == Regions[Regionrng.Next(0, 4)]; //there is definitely an easy solution to doing this.
            // } */
            //if (SelectedRegion == Regions[0])
            //{
            //    DrawEllipse(10, 50, 50, 50); // x, y, width, height
            //    DrawEllipse(50, 100, 50, 50);
            //    DrawEllipse(100, 150, 50, 50);
            //    DrawEllipse(150, 200, 50, 50);
            //}
            //if (SelectedRegion == Regions[1])
            //{
            //    DrawEllipse(10, 50, 50, 50); // x, y, width, height
            //    DrawEllipse(50, 100, 50, 50);
            //    DrawEllipse(100, 150, 50, 50);
            //    DrawEllipse(150, 200, 50, 50);
            //}

            ////make a method of this and use it twice
            //if (SelectedRegion == Regions[2])
            //{
            //    DrawEllipse(10, 50, 50, 50); // x, y, width, height
            //    DrawEllipse(50, 100, 50, 50);
            //    DrawEllipse(100, 150, 50, 50);
            //    DrawEllipse(150, 200, 50, 50);
            //}
            //if (SelectedRegion == Regions[3])
            //{
            //    DrawEllipse(10, 50, 50, 50); // x, y, width, height
            //    DrawEllipse(50, 100, 50, 50);
            //    DrawEllipse(100, 150, 50, 50);
            //    DrawEllipse(150, 200, 50, 50);

            //    //Each node (each line) has a x,y position.
            //    //I need to find a way to communicate with that using the coordinates text box in the report submission section.
            //}



            #endregion

            RegionSelect();
        }

        private void RegionSelect()
        {
            switch (SelectedRegion)
            {
                case "RegionA": DrawEllipse(10, 50, 50, 50); DrawEllipse(50, 100, 50, 50); DrawEllipse(100, 150, 50, 50); DrawEllipse(150, 200, 50, 50); ActiveCanvas = CanvasRegionA; break;
                case "RegionB": DrawEllipse(10, 50, 50, 50); DrawEllipse(50, 100, 50, 50); DrawEllipse(100, 150, 50, 50); DrawEllipse(150, 200, 50, 50); ActiveCanvas = CanvasRegionB; break;
                case "RegionC": DrawEllipse(10, 50, 50, 50); DrawEllipse(50, 100, 50, 50); DrawEllipse(100, 150, 50, 50); DrawEllipse(150, 200, 50, 50); ActiveCanvas = CanvasRegionC; break;
                case "RegionD": DrawEllipse(10, 50, 50, 50); DrawEllipse(50, 100, 50, 50); DrawEllipse(100, 150, 50, 50); DrawEllipse(150, 200, 50, 50); ActiveCanvas = CanvasRegionD; break;
            }
        }



        public void mdClickMap(object sender, MouseButtonEventArgs MousePointer) //no errors but section at bottom should be reworked
        {
            BlankCanvas_Unloaded();
            //Unload the canvas with the starting content when any region is clicked

            var map = sender as Image; //"Map" is equal to the image property of the code line that this method/event is associated with
            var position = MousePointer.GetPosition(map); //"Position" is equal to the x,y position of the mouse pointer relevant to the object interactable with it

            int xPosition = (int)position.X;
            //this int, is equal to an int version of the value of the position variable
            //which gets it values from the GetPosition function applied to the dimensions of the map
            int yPosition = (int)position.Y;

            if (xPosition < 75 && yPosition < 52) //If this can be done in a switch statement it should be.
            {
                //rectRegion.image = regionA
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionA.png", UriKind.Relative));
                SelectedRegion = Regions[0];

                CanvasRegionA_loaded(); //load the canvas the unique set of nodes is on

                //unload the others
                CanvasRegionB_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (xPosition > 75 && yPosition < 52)
            {
                //rectRegion.image = regionB
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionB.png", UriKind.Relative));
                SelectedRegion = Regions[1];
                CanvasRegionB_loaded();

                CanvasRegionA_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (xPosition < 75 && yPosition > 52)
            {
                //rectRegion.image = regionC
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionC.png", UriKind.Relative));
                SelectedRegion = Regions[2];
                CanvasRegionC_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionA_Unloaded();
                CanvasRegionD_Unloaded();
            }
            else if (xPosition > 75 && yPosition > 52)
            {
                //rectRegion.image = regionD
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionD.png", UriKind.Relative));
                SelectedRegion = Regions[3];
                CanvasRegionD_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionA_Unloaded();
            }

            //RegionMap.Source = new BitmapImage(new Uri($"/images/{SelectedRegion}.png", UriKind.Relative));
            //tbxRegionDisplayed.Text = SelectedRegion;
            //alt way to do it

            MessageBox.Show($"{xPosition},{yPosition}");

            //redraw
            BlankCanvas.Children.Clear();
            CanvasRegionA.Children.Clear();
            CanvasRegionB.Children.Clear();
            CanvasRegionC.Children.Clear();
            CanvasRegionD.Children.Clear();

            #region machine code node positioning
            //redundant due to RegionSelect()
            /* double[,] NodePositions = new double[,] //This should be targeted as a core "rewrite area"
             {
                 { 10, 50 }, { 50, 100 }, { 100, 150 }, { 150, 200 } //these are x,y positions
             };
             for (int counter = 0; counter < NodePositions.GetLength(0); counter++) //As long as the counter is less than the amount of elements in the array do the following
             {
                 DrawEllipse(NodePositions[counter, 0], NodePositions[counter, 1], 10, 10); //Reassess this logic here
             } */
            #endregion
        }

        private void btnMarkAsDestination_Click(object sender, RoutedEventArgs e) //This should be fine
        {

            tbxDestinationRegion.Text = SelectedRegion; //The text of this box is equal to the currently established "SelectedRegion" at the time the button is pressed.
            tbxDestinationName.Text = tbxSelectedAreaName.Text;
            tbxDestinationCoordinates.Text = tbxSelectedCoordinates.Text;
        }

        private void btnReportSubmission_Click(object sender, RoutedEventArgs e) //Method is correct
        {
            status = null;
            TeamNumber = TeamNorng.Next(100, 999);
            Coordindates = tbxCoordinatesReport.Text;
            Message = tbxReportMessage.Text;

            if (rbRed.IsChecked == true)
            {
                status = rbRed.Content.ToString();
            }
            else if (rbOrange.IsChecked == true)
            {
                status = rbOrange.Content.ToString();
            }
            else if (rbGreen.IsChecked == true)
            {
                status = rbGreen.Content.ToString();
            }
            else if (rbBlack.IsChecked == true)
            {
                status = rbBlack.Content.ToString();
            }
            else if (status == null)
            {
                MessageBox.Show("Please select a status.");
                return;
            }
            else 
            {
                MessageBox.Show("Error, please try again.");
                return;
            }

            #region Other Report Submission Attempts
            //string Status = ActiveRadioButton.Content.ToString(); //= rbRed.IsChecked == true ? "In Need of Assistance" : rbOrange.IsChecked == true ? "Situation Being Managed" : 
            //rbGreen.IsChecked == true ? "Situation De-Escalated" : rbBlack.IsChecked == true ? "DANGER" : "Unknown";

            //switch (Status) //The exact process of what needs to be detected and displayed as a result of that detection needs to be figured out here as both attempts are messy
            //{
            //    //case rbRed.IsChecked: rbRed = ActiveRadioButton; 
            //    case "- In Need of Assistance":
            //        rbRed = ActiveRadioButton;
            //        //ActiveRadioButton = rbRed;

            //        rbRed.IsChecked = true;
            //        break;

            //    case "Situation Being Managed":
            //        rbOrange = ActiveRadioButton;
            //        rbOrange.IsChecked = true;
            //        break;

            //    case "Situation De-Escalated":
            //        rbGreen = ActiveRadioButton;
            //        rbGreen.IsChecked = true;
            //        break;
            //    //Is this method better?
            //    case "DANGER":
            //        rbBlack = ActiveRadioButton;
            //        rbBlack.IsChecked = true;
            //        break;
            //}
            #endregion


            var Report = new ResponderReports(status, TeamNumber, DateTime.Now, Message);
            //[this] = to a responder report with its constructer's arguiments equal to the variables declared above
            //which in turn are equal to the values passed into the text boxes in the main app
            ResponderReports.Add(Report); //add that custom report to the list of reports
            lbxReportsR.ItemsSource = ResponderReports; //display it in the text box

            tbxSelectedNodeStatus.Text = status;
            tbxMessageDisplay.Text = Message;

            //Update node color
            foreach (var node in Nodes)
            {
                #region switch version
                /* SingleNode.Ellipse.Fill = Status switch
                {
                    "In Need of Assistance" => Brushes.Red,
                    "Situation Being Managed" => Brushes.Orange,
                    "Situation De-Escalated" => Brushes.Green,
                    "DANGER" => Brushes.Black,
                }; */
                #endregion

                if (status == rbRed.Content.ToString()) 
                {
                    node.Ellipse.Fill = Brushes.Red;
                }
                else if (status == rbOrange.Content.ToString())
                {
                    node.Ellipse.Fill = Brushes.Orange;
                }
                else if (status == rbGreen.Content.ToString())
                {
                    node.Ellipse.Fill = Brushes.Green;
                }
                else if (status == rbBlack.Content.ToString())
                {
                    node.Ellipse.Fill = Brushes.Black;
                }
                else
                {
                    node.Ellipse.Fill = Brushes.White;
                }
                tbxSelectedAreaName.Text = node.Name; 
                tbxSelectedAreaName.Text = node.Coordinates;

            }
            #region logical notes
            //Read which radio button was selected
            //Read the coordinates inputted
            //Read the custom message - if any

            ////Assign a status based on the radio button to the node specified by the coordinates
            ////Assign the custom message to that node

            ////Create a Responder report class object with the above values + the current time
            ////Change the node color based upon the radio button selection
            ////Populate the lbxReportsR with that class

            #endregion
        }


        private void DrawEllipse(double x, double y, double width, double height) //This should be targeted as a core "rewrite area"
        {
            Ellipse ellipse = new Ellipse
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.White // Set to Transparent if you only want an outline
            };

            // Set position using Canvas coordinates
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);

            // Add ellipse to Canvas (on top of the image)

            string region = SelectedRegion;

            if (region == "RegionA")
                ActiveCanvas = CanvasRegionA;
            else if (region == "RegionB")
                ActiveCanvas = CanvasRegionB;
            else if (region == "RegionC")
                ActiveCanvas = CanvasRegionC;
            else if (region == "RegionD")
                ActiveCanvas = CanvasRegionD;
            else
                ActiveCanvas = BlankCanvas;



            ActiveCanvas.Children.Add(ellipse);

            var SingleNode = new Node
            {
                Region = SelectedRegion,
                Name = $"Node_{Nodes.Count + 1}",
                Coordinates = $"{x},{y}", 
                Ellipse = ellipse
            };
            Nodes.Add(SingleNode);

            ellipse.MouseLeftButtonDown += (s, e) => //this line and formatting was done by machine code
            {
                tbxSelectedAreaName.Text = SingleNode.Name;
                tbxSelectedCoordinates.Text = SingleNode.Coordinates;
                tbxSelectedNodeStatus.Text = "";
                tbxMessageDisplay.Text = "";

                var CivillianReport = new CivillianReports(ReportsCrng.Next(100, 999), DateTime.Now, $"Report for {SingleNode.Name}");
                CivillianReportsSet1.Add(CivillianReport);
                lbxReportsC.ItemsSource = null;
                lbxReportsC.ItemsSource = CivillianReportsSet1;

                var ResponderReport = new ResponderReports("", ReportsRrng.Next(1, 10), DateTime.Now, $"Report for {SingleNode.Name}");
                ResponderReports.Add(ResponderReport);
                lbxReportsR.ItemsSource = null;
                lbxReportsR.ItemsSource = ResponderReports;
            };
        }
        #region Canvas' Unloaded
        public void BlankCanvas_Unloaded()
        {
            BlankCanvas.Visibility = Visibility.Collapsed;
        }
        public void CanvasRegionA_Unloaded()
        {
            CanvasRegionA.Visibility = Visibility.Collapsed;

        }
        public void CanvasRegionB_Unloaded()
        {
            CanvasRegionB.Visibility = Visibility.Collapsed;

        }
        public void CanvasRegionC_Unloaded()
        {
            CanvasRegionC.Visibility = Visibility.Collapsed;
        }
        public void CanvasRegionD_Unloaded()
        {
            CanvasRegionD.Visibility = Visibility.Collapsed;

        }
        #endregion

        #region Canvas' Loaded
        public void BlankCanvas_loaded()
        {
            BlankCanvas.Visibility = Visibility.Visible;

        }
        public void CanvasRegionA_loaded()
        {
            CanvasRegionA.Visibility = Visibility.Visible;

        }
        public void CanvasRegionB_loaded()
        {
            CanvasRegionB.Visibility = Visibility.Visible;

        }
        public void CanvasRegionC_loaded()
        {
            CanvasRegionC.Visibility = Visibility.Visible;

        }
        public void CanvasRegionD_loaded()
        {
            CanvasRegionD.Visibility = Visibility.Visible;

        }
        #endregion

        #region logical notes and planning
        //Each node needs to be assigned unique coordinate value in the x,y format e.g "18,79"

        //Primary functionality is dependent on an 'on_click' method for the nodes in the top images.
        //This should generate consistent values in the 3 text boxes directly below the left image.
        //Should the "mark as destination" button be clicked while a node is selected
        //the values in the 3 top right text boxes should match the values in the aformentioned 3 text boxes and stay like that until they are manually changed.

        //This should also generate a random series of two seperate class objects in the report log boxes.
        //("responder report" and "civillian report")

        //A "confirm information submission" button should be added near the report information section
        //Once this is pressed, the node with matching coordinate info to those specified in report info section
        //should change color based upon which radio button in the report info section was selected.

        //Additionally, it should create a new object of the "responder report" class.
        //This will decide what is displayed in the "status" section of the responder log box (how will that have to be divided?).
        //The "team number" section of the log will be statically generated (for now).
        //the "time" section can be generated as the current time quite easily I believe.
        //the "message" section is determined by what was entered into the "custom message" box
        //This object should be displayed in the left list box in the report log section.

        //Master button called "btnTesting".
        //Intended to randomly generate some or all of the currently static values
        //such as starting node status', the user location, the general alerts and the region alerts.
        #endregion
    }
}
