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
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Find/think of some way to deal with the user location, how could it be moved? Should it be deleted entirely? Randomly generated on start?
        }

        private void btnTesting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMarkAsDestination_Click(object sender, RoutedEventArgs e)
        {
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

            //Create a Responder report class object with the above values + the current time
            //Change the node color based upon the radio button selection
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

        //A change im considering is a master button called "Simulate".
        //If implemented, it would randomly generate some or all of the currently static values
        //such as starting node status', the user location, the general alerts and the region alerts.
    }
}
