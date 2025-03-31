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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Could nodes be generated here or in the method below?
            //Their assigned values must be specified in either, database utility?
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Find/think of some way to deal with the user location, how could it be moved? Should it be deleted entirely? Randomly generated on start?
            //In any case, all it should affect is the three top left text boxes unless it can be changed in some way.
        }

        private void btnTesting_Click(object sender, RoutedEventArgs e)
        {
            //Create new civillian reports for *every* node and assign them to them so that lbxReportsC will display them upon click
            //Create a list of alerts of type "General", populate lbxGeneralAlerts with them
            //Create a list of reports of type "Region", populate the 4 seperate region list boxes with them
        }
        private void Region_Click(object sender, RoutedEventArgs e) //dummy
        {
            //On selection of a region (through the right screen), the left screen must choose the correct option from
            //some list of several regions and change its display accordingly.
        }

        private void Node_Click(object sender, RoutedEventArgs e) //dummy
        {
            //Generate OR load already established class objects of both type Responder and type Civillian
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
