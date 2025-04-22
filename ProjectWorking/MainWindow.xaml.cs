using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProjectWorking
{
    public partial class MainWindow : Window
    {
        public string SelectedRegion;
        public string[] Regions = new string[4] { "RegionA", "RegionB", "RegionC", "RegionD" };

        public List<CivillianReports> CivillianReportsSet1 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet2 = new List<CivillianReports>();
        public List<CivillianReports> CivillianReportsSet3 = new List<CivillianReports>();

        CivillianReports Set1Report1 = new CivillianReports(449, (2014, 11, 17), "test"); //CivillianReportsSet1.Add(Set1Report1);
        //CivillianReports Set1Report2 = new CivillianReports(449, (2014, 11, 17), "test");
        //CivillianReports Set1Report3 = new CivillianReports(449, (2014, 11, 17), "test");


        //"civillianreports does not contain a constructor that takes 3 arguments", yes it does???
        //The .add function is also not working.
        //Once fixed, repeat this process with the others, then find a way for btnTesting to pick a random set and display that in the list box

        //The same can be done for responderReports, but far more imporantly is the functionality of them being created by the user's interaction
        //with the report submission section and submission of that information which is then displayed in the text box.
        //I am also unsure if these report instances should be created here or in the btnTesting_click method.


        public MainWindow()
        {
            InitializeComponent();
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Find/think of some way to deal with the user location, how could it be moved? Should it be deleted entirely? Randomly generated on start?
            //In any case, all it should affect is the three top left text boxes unless it can be changed in some way.

            RegionMap.Source = new BitmapImage(new Uri("/images/RegionBlank.png", UriKind.Relative));

            StartingCanvas_loaded();           
            CanvasRegionA_Unloaded();
            CanvasRegionB_Unloaded();
            CanvasRegionC_Unloaded();
            CanvasRegionD_Unloaded();
        }

        private void btnTesting_Click(object sender, RoutedEventArgs e)
        {
            //Possibly choose a random region and node for the user location - affects the top left text boxes

            //Create new civillian reports for *every* node and assign them to them so that lbxReportsC will display them upon click

            //Create a list of alerts of type "General", populate lbxGeneralAlerts with them
            //Create a list of alerts of type "Region", populate the 4 seperate region list boxes with them

            //[Some sort of random select of the civillian reports sets]
            //lbxReportsC.ItemsSource = [the set of civillian reports chosen]

            Random RNGxy = new Random(); //Can this be used instead of hardcoding positions?
            Random Regions0to3 = new Random(); //how to set limits on a random??


            //This system only partially works as it requires the user to interact with 
            //the map before hitting the testing button
#region CanvasSelect
            if (SelectedRegion == null)
            {
                SelectedRegion == Regions[Regions0to3]; //there is definitely an easy solution to doing this
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
            MessageBox.Show($"{xPosition},{yPosition}");
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

            //tbxDestinationRegion.Text = noderegion
            //tbxDestinationName.Text = nodename
            //tbxDestinationCoordinates.Text = nodecoordinates
        }

        private void btnReportSubmission_Click(object sender, RoutedEventArgs e)
        {
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

        }
        public void CanvasRegionA_Unloaded()
        {

        }
        public void CanvasRegionB_Unloaded()
        {

        }
        public void CanvasRegionC_Unloaded()
        {

        }
        public void CanvasRegionD_Unloaded()
        {

        }
        public void StartingCanvas_loaded()
        {

        }
        public void CanvasRegionA_loaded()
        {

        }
        public void CanvasRegionB_loaded()
        {

        }
        public void CanvasRegionC_loaded()
        {

        }
        public void CanvasRegionD_loaded()
        {

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
