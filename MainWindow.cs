using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApplication
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddRentalRecord();
            addRentalRecord.MdiParent = this;   
            addRentalRecord.Show();
        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vehicleListing = new ManageVehicleListing(); //Create an object for ManageVehicleListing form and set its parent to the MainWindow
            vehicleListing.MdiParent = this;
            vehicleListing.Show();  
        }
    }
}
