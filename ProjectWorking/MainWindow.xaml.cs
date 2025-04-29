using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProjectWorking
{
    public partial class MainWindow : Window
    {
        public string SelectedRegion;
        public string[] Regions = new string[4] { "RegionA", "RegionB", "RegionC", "RegionD" };
        private List<Node> Nodes = new List<Node>();
        
        //Random number generators
        public Random Regionrng = new Random();
        public Random Userrng = new Random();
        Random Regionrng2 = new Random();
        Random Userrng2 = new Random();
        Random ReportsCrng = new Random();
        Random AlertsGrng = new Random();
        Random AlertsRrng = new Random();
        Random TeamNorng = new Random();



        public List<CivillianReports> CivillianReportsSet1 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet2 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet3 = new List<CivillianReports>();

        private List<ResponderReports> ResponderReports = new List<ResponderReports>(); //Does this need/can it have multiple sets

        private List<GeneralAlerts> GeneralAlertsSet1 = new List<GeneralAlerts>();
        private List<GeneralAlerts> GeneralAlertsSet2 = new List<GeneralAlerts>();
        private List<GeneralAlerts> GeneralAlertsSet3 = new List<GeneralAlerts>();
 
        private List<RegionAlerts>[] RegionAlerts = new List<RegionAlerts>[4] { new List<RegionAlerts>(), new List<RegionAlerts>(), new List<RegionAlerts>(), new List<RegionAlerts>() };


        //CivillianReports Set1Report1 = new CivillianReports(449, (2014, 11, 17), "test"); //CivillianReportsSet1.Add(Set1Report1);
        //CivillianReports Set1Report2 = new CivillianReports(449, (2014, 11, 17), "test");
        //CivillianReports Set1Report3 = new CivillianReports(449, (2014, 11, 17), "test");

        //Once fixed, repeat this process with the others, then find a way for btnTesting to pick a random set and display that in the list box

        //The same can be done for responderReports, but far more imporantly is the functionality of them being created by the user's interaction
        //with the report submission section and submission of that information which is then displayed in the text box.
        //I am also unsure if these report instances should be created here or in the btnTesting_click method.


        public MainWindow()
        {
            InitializeComponent();

            // Initialize CivillianReports sets
            //This is the proper syntax for adding a class object to a list of the same type
            //[List].add(new [class object type](however many properties it has));
            CivillianReportsSet1.Add(new CivillianReports(449, new DateTime(2014, 10, 17), "Test Report 1"));
            CivillianReportsSet1.Add(new CivillianReports(450, new DateTime(2014, 10, 18), "Test Report 2"));
            CivillianReportsSet1.Add(new CivillianReports(451, new DateTime(2014, 10, 19), "Test Report 3"));

            CivillianReportsSet2.Add(new CivillianReports(452, new DateTime(2014, 11, 10), "Test Report 4"));
            CivillianReportsSet2.Add(new CivillianReports(453, new DateTime(2014, 11, 11), "Test Report 5"));
            CivillianReportsSet2.Add(new CivillianReports(454, new DateTime(2014, 11, 12), "Test Report 6"));

            CivillianReportsSet3.Add(new CivillianReports(455, new DateTime(2015, 12, 20), "Test Report 7"));
            CivillianReportsSet3.Add(new CivillianReports(456, new DateTime(2015, 12, 21), "Test Report 8"));
            CivillianReportsSet3.Add(new CivillianReports(457, new DateTime(2015, 12, 22), "Test Report 9"));

        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Find/think of some way to deal with the user location, how could it be moved? Should it be deleted entirely? Randomly generated on start?
            //In any case, all it should affect is the three top left text boxes unless it can be changed in some way.

            //random region on start
            SelectedRegion = Regions[Regionrng.Next(0, Regions.Length)]; //goes through the regions array start to finish to select a region upon startup
            RegionMap.Source = new BitmapImage(new Uri($"/images/{SelectedRegion}.png", UriKind.Relative)); //this works as the regions in the region array have the same name
            //as the images that are equvilant to them
            tbxRegionDisplayed.Text = SelectedRegion;


            //random user location on start
            tbxCurrentRegion.Text = SelectedRegion;
            tbxNearestUserNode.Text = "Node1"; //can this be randomized?
            tbxUserCoordinates.Text = $"{Userrng.Next(0, 150)},{Userrng.Next(0, 150)}"; //change to correct x,y limits later

            StartingCanvas_loaded(); //incorrect now that theres a random starting region? Maybe I should duplicate the region if statements up here?          
            CanvasRegionA_Unloaded();
            CanvasRegionB_Unloaded();
            CanvasRegionC_Unloaded();
            CanvasRegionD_Unloaded();
        }

        private void btnTesting_Click(object sender, RoutedEventArgs e)
        {

            Nodes.Clear(); //Clear the list of currently recorded nodes

            //random region on click
            SelectedRegion = Regions[Regionrng2.Next(0, Regions.Length)];
            tbxRegionDisplayed.Text = SelectedRegion; //Exact same process as above
            RegionMap.Source = new BitmapImage(new Uri($"/images/{SelectedRegion}.png", UriKind.Relative));

            //random user location on click
            tbxCurrentRegion.Text = SelectedRegion;
            tbxNearestUserNode.Text = "Node1"; //can this be randomized?
            tbxUserCoordinates.Text = $"{Userrng2.Next(0, 150)},{Userrng2.Next(0, 150)}"; //change to correct x,y limits later

            // Randomly choose one of the CivillianReports sets
         

            //Adding one of the lists of civillian reports to the civillian reports list box
            var CReportSets = new List<List<CivillianReports>> { CivillianReportsSet1, CivillianReportsSet2, CivillianReportsSet3 }; 
            //a var is similar to a list without a name
            lbxReportsC.ItemsSource = CReportSets[ReportsCrng.Next(CReportSets.Count)];

            //Adding one of the lists of general alerts to the general alerts list box

            var GAlertsSets = new List<List<GeneralAlerts>> { GeneralAlertsSet1, GeneralAlertsSet2, GeneralAlertsSet3 };
            lbxGeneralAlerts.ItemsSource = GAlertsSets[AlertsGrng.Next(GAlertsSets.Count)];

            //Clear and draw nodes
            StartingCanvas.Children.Clear(); //Again possibly irrelevent now
            CanvasRegionA.Children.Clear();
            CanvasRegionB.Children.Clear();
            CanvasRegionC.Children.Clear();
            CanvasRegionD.Children.Clear();

            double[,] NodePositions = new double[,]
            {
                { 10, 50 }, { 50, 100 }, { 100, 150 }, { 150, 200 }
            };
            for (int counter = 0; counter < NodePositions.GetLength(0); counter++) //As long as the counter is less than the amount of elements in the array do the following
            {
                DrawEllipse(NodePositions[counter, 0], NodePositions[counter, 1], 50, 50);
            }

            // Generate alerts
            GeneralAlertsSet1.Clear(); //Clear out any items previously loaded to these lists 
            GeneralAlertsSet2.Clear();
            GeneralAlertsSet3.Clear();
            string[] AlertTypes = NatureOfAlert //{ "Keep Viligant", "Maintain Contact", "Warning", "EVACUATE" }; //restated as I couldnt use the one from the external class
            GeneralAlertsSet1.Add(new GeneralAlerts("All Teams", AlertTypes[AlertsGrng.Next(0, 4)], "System maintenance at 10 PM", DateTime.Now));
            GeneralAlertsSet1.Add(new GeneralAlerts("Command Center", AlertTypes[AlertsGrng.Next(0, 4)], "Weather warning issued", DateTime.Now));
            lbxGeneralAlerts.ItemsSource = GeneralAlertsSet1;

            

            for (int counter = 0; counter < 4; counter++) 
            {
                RegionAlerts[counter].Clear();
                RegionAlerts[counter].Add(new RegionAlerts(Regions[counter].Replace("Region", ""), AlertTypes[AlertsRrng.Next(0, 4)],$"Incident reported in Region {Regions[counter]}", DateTime.Now));
                switch (counter)
                {
                    case 0: lbxRegionAlerts1.ItemsSource = RegionAlerts[counter]; break;
                    case 1: lbxRegionAlerts2.ItemsSource = RegionAlerts[counter]; break;
                    case 2: lbxRegionAlerts3.ItemsSource = RegionAlerts[counter]; break;
                    case 3: lbxRegionAlerts4.ItemsSource = RegionAlerts[counter]; break;
                }
            }

            //Possibly choose a random region and node for the user location - affects the top left text boxes

            //Create new civillian reports for *every* node and assign them to them so that lbxReportsC will display them upon click

            //Create a list of alerts of type "General", populate lbxGeneralAlerts with them
            //Create a list of alerts of type "Region", populate the 4 seperate region list boxes with them

            //[Some sort of random select of the civillian reports sets]
            //lbxReportsC.ItemsSource = [the set of civillian reports chosen]

            //Random RNGxy = new Random(); //Can this be used instead of hardcoding positions?
            //Random Regions0to3 = new Random(); //how to set limits on a random??


            //This system only partially works as it requires the user to interact with 
            //the map before hitting the testing button
#region CanvasSelect
            if (SelectedRegion == null)
            {
                SelectedRegion == Regions[Regionrng.Next(0, 4)]; //there is definitely an easy solution to doing this
            }
            if (SelectedRegion == Regions[0])
            {
                DrawEllipse(10, 50, 50, 50); // x, y, width, height
                DrawEllipse(50, 100, 50, 50);
                DrawEllipse(100, 150, 50, 50); 
                DrawEllipse(150, 200, 50, 50);
            }
            if (SelectedRegion == Regions[1])
            {
                DrawEllipse(10, 50, 50, 50); // x, y, width, height
                DrawEllipse(50, 100, 50, 50);
                DrawEllipse(100, 150, 50, 50);
                DrawEllipse(150, 200, 50, 50);
            }
            if (SelectedRegion == Regions[2])
            {
                DrawEllipse(10, 50, 50, 50); // x, y, width, height
                DrawEllipse(50, 100, 50, 50);
                DrawEllipse(100, 150, 50, 50);
                DrawEllipse(150, 200, 50, 50);
            }
            if (SelectedRegion == Regions[3])
            {
                DrawEllipse(10, 50, 50, 50); // x, y, width, height
                DrawEllipse(50, 100, 50, 50);
                DrawEllipse(100, 150, 50, 50);
                DrawEllipse(150, 200, 50, 50);

                //Each node (each line) has a x,y position.
                //I need to find a way to communicate with that using the coordinates text box in the report submission section.
            }

            #endregion
        }

        public void mdClickMap(object sender, MouseButtonEventArgs MousePointer)
        {
            //string[] Regions = new string[4] { "RegionA", "RegionB", "RegionC", "RegionD" };
            //string SelectedRegion;

            StartingCanvas_Unloaded(); 
            //Unload the canvas with the starting content when any region is clicked

            var map = sender as Image; //"Map" is equal to the image property of the code line that this method/event is associated with
            var position = MousePointer.GetPosition(map); //"Position" is equal to the x,y position of the mouse pointer relevant to the object interactable with it

            int xPosition = (int)position.X;
            //this int, is equal to an int version of the value of the position variable
            //which gets it values from the GetPosition function applied to the dimensions of the map
            int yPosition = (int)position.Y;

            if (xPosition < 75 && yPosition < 52)
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
            if (xPosition > 75 && yPosition < 52)
            {
                //rectRegion.image = regionB
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionB.png", UriKind.Relative));
                SelectedRegion = Regions[1];
                CanvasRegionB_loaded();

                CanvasRegionA_Unloaded();
                CanvasRegionC_Unloaded();
                CanvasRegionD_Unloaded();
            }
            if (xPosition < 75 && yPosition > 52)
            {
                //rectRegion.image = regionC
                RegionMap.Source = new BitmapImage(new Uri("/images/RegionC.png", UriKind.Relative));
                SelectedRegion = Regions[2];
                CanvasRegionC_loaded();

                CanvasRegionB_Unloaded();
                CanvasRegionA_Unloaded();
                CanvasRegionD_Unloaded();
            }
            if (xPosition > 75 && yPosition > 52)
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
            StartingCanvas.Children.Clear();
            CanvasRegionA.Children.Clear();
            CanvasRegionB.Children.Clear();
            CanvasRegionC.Children.Clear();
            CanvasRegionD.Children.Clear();

            double[,] NodePositions = new double[,]
            {
                { 10, 50 }, { 50, 100 }, { 100, 150 }, { 150, 200 }
            };
            for (int counter = 0; counter < NodePositions.GetLength(0); counter++) //As long as the counter is less than the amount of elements in the array do the following
            {
                DrawEllipse(NodePositions[counter, 0], NodePositions[counter, 1], 50, 50);
            }


        }

        private void Node_Click(object sender, RoutedEventArgs e) //dummy
        {
            //Generate - OR load already established - class objects of both type Responder and type Civillian
            //Populate their respective list boxes with these objects - can they be saved or must they be randomly generated every time?
            //The three text boxes ***UNDER*** the left image should be affected by this click
        }

        private void btnMarkAsDestination_Click(object sender, RoutedEventArgs e)
        {
            //Get the node that is currently selected and read its information
            //Get the id of the region the node is in
            //Get its name
            //Get its coordinates

            tbxDestinationRegion.Text = SelectedRegion;
            tbxDestinationName.Text = tbxSelectedAreaName.Text;
            tbxDestinationCoordinates.Text = tbxSelectedCoordinates.Text;
        }

        private void btnReportSubmission_Click(object sender, RoutedEventArgs e)
        {
            Button ActiveRadioButton = new Button();

            string Status; //= rbRed.IsChecked == true ? "In Need of Assistance" : rbOrange.IsChecked == true ? "Situation Being Managed" : 
            //rbGreen.IsChecked == true ? "Situation De-Escalated" : rbBlack.IsChecked == true ? "DANGER" : "Unknown";

            switch (Status)
            {
                //case rbRed.IsChecked: rbRed = ActiveRadioButton; 
                case "In Need of Assistance":
                    rbRed = ActiveRadioButton;
                    rbRed.IsChecked = true;
                    break;

                case "Situation Being Managed":
                    rbOrange = ActiveRadioButton;
                    rbOrange.IsChecked = true;
                    break;

                case "Situation De-Escalated":
                    rbGreen = ActiveRadioButton;
                    rbGreen.IsChecked = true;
                    break;
                    //Is this method better?
                case "DANGER":
                    rbBlack = ActiveRadioButton;
                    rbBlack.IsChecked = true;
                    break;
            }

            string Coordindates = tbxCoordinatesReport.Text;
            string Message = tbxReportMessage.Text;
            int TeamNumber = TeamNorng.Next(100, 999);



            var Report = new ResponderReports(Status, TeamNumber, DateTime.Now, Message); 
            //[this] = to a responder report with its constructer's arguiments equal to the variables declared above
            //which in turn are equal to the values passed into the text boxes in the main app
            ResponderReports.Add(Report); //add that custom report to the list of reports
            lbxReportsR.ItemsSource = ResponderReports; //display it in the text box

            tbxSelectedNodeStatus.Text = Status;
            tbxMessageDisplay.Text = Message;

            //Update node color
            foreach (var node in Nodes)
            {
                /* SingleNode.Ellipse.Fill = Status switch
                {
                    "In Need of Assistance" => Brushes.Red,
                    "Situation Being Managed" => Brushes.Orange,
                    "Situation De-Escalated" => Brushes.Green,
                    "DANGER" => Brushes.Black,
                }; */

                if (Status == "In Need of Assistance")
                {
                    node.Ellipse.Fill = Brushes.Red;
                }
                if (Status == "Situation Being Managed")
                {
                    node.Ellipse.Fill = Brushes.Orange;
                }
                if (Status == "Situation De-Escalated")
                {
                    node.Ellipse.Fill = Brushes.Green;
                }
                if (Status == "DANGER")
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

            //Read which radio button was selected
            //Read the coordinates inputted
            //Read the custom message - if any

            ////Assign a status based on the radio button to the node specified by the coordinates
            ////Assign the custom message to that node

            ////Create a Responder report class object with the above values + the current time
            ////Change the node color based upon the radio button selection
            ////Populate the lbxReportsR with that class
        }


        private void DrawEllipse(double x, double y, double width, double height)
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
            StartingCanvas.Children.Add(ellipse); //Change this to "active canvas" or some equivilant
        }

        public void StartingCanvas_Unloaded()
        {
            StartingCanvas.Visibility = Visibility.Collapsed;
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


        public void StartingCanvas_loaded()
        {
            StartingCanvas.Visibility = Visibility.Visible;

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
    }
}
