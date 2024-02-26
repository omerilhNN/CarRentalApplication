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
    public partial class AddEditVehicle : Form
    {
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities _db; //Container name -> inside CarRentalDb.edmx file -> represents the database object
        private bool isEditMode;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;   
            _db = new CarRentalEntities();  
        }
        public AddEditVehicle(TypesOfCars carToEdit, ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            if (carToEdit == null ) 
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(carToEdit);
            }
        }

        private void PopulateFields(TypesOfCars car)
        {
            lblId.Text = car.id.ToString();
            tbMake.Text = car.make;
            tbModel.Text = car.model;
            tbVin.Text = car.vin;
            tbYear.Text = car.year.ToString();
            tbLicensePlateNumber.Text = car.licensePlateNumber;    
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                //Added Validation for make and model
                if (string.IsNullOrWhiteSpace(tbMake.Text) ||
                        string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    MessageBox.Show("Please ensure that you provide a make and a model");
                }
                else
                {
                    //if(isEditMode == true)
                    if (isEditMode)
                    {
                        //Edit Code here
                        var id = int.Parse(lblId.Text);
                        var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);
                        car.model = tbModel.Text;
                        car.make = tbMake.Text;
                        car.vin= tbVin.Text;
                        car.year = int.Parse(tbYear.Text);
                        car.licensePlateNumber = tbLicensePlateNumber.Text;
                       
                    }
                    else
                    {
                        //Added validation for make and model of cars being added

                        // Add Code Here
                        var newCar = new TypesOfCars
                        {
                            licensePlateNumber = tbLicensePlateNumber.Text,
                            make = tbMake.Text,
                            model = tbModel.Text,
                            vin= tbVin.Text,
                            year = int.Parse(tbYear.Text)
                        };

                        _db.TypesOfCars.Add(newCar);

                    }
                    _db.SaveChanges();
                    _manageVehicleListing.PopulateGrid();
                    MessageBox.Show("Operation Completed. Refresh Grid To see Changes");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
