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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db; //Container name -> inside CarRentalDb.edmx file -> represents the database object
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();  //initialization
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Select * from TypeOfCars
            //var cars = _db.TypesOfCars.ToList(); //Getting datas from DB and keeping them inside a list 
            //DataGridView must show
            //var cars = _db.TypesOfCars
            //  .Select(q => new {ID = q.id , Name = q.make})
            //.ToList(); //q is every one of the items
            var cars = _db.TypesOfCars
                .Select(q => new {
                    make = q.make, 
                    model = q.model, 
                    vin = q.vin, 
                    year = q.year, 
                    licensePlateNumber = q.licensePlateNumber ,
                    q.id
                })
                .ToList();

            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            gvVehicleList.Columns["Id"].Visible = false;
            //gvVehicleList.Columns[0].HeaderText = "ID";
           // gvVehicleList.Columns[1].HeaderText = "Name";

        }

        private void btnAddNewCar_Click(object sender, EventArgs e)
        {
            AddEditVehicle addEditVehicle = new AddEditVehicle();
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            { 
                //Get selected row id
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //Query db for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

                //launch AddEditVehicle window with data 
                var addEditVehicle = new AddEditVehicle(car);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
           
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    //delete vehicle from table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    make = q.make,
                    model = q.model,
                    vin = q.vin,
                    year = q.year,
                    licensePlateNumber= q.licensePlateNumber,
                    q.id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvVehicleList.Columns["Id"].Visible = false;
        }
    }
}
