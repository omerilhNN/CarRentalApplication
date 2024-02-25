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

        private readonly CarRentalEntities _db; //Container name -> inside CarRentalDb.edmx file -> represents the database object
        private bool isEditMode;
        public AddEditVehicle()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _db = new CarRentalEntities();  
        }
        public AddEditVehicle(TypesOfCars carToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            if(carToEdit == null ) 
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
            if (isEditMode)
            {
                //Edit code -> there is a car existing 
                var id = int.Parse(lblId.Text); 
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);  //Returns the first element that satisfies the condition else Return default
                car.model = tbModel.Text;
                car.make = tbMake.Text;
                car.vin = tbVin.Text;
                car.year = int.Parse(tbYear.Text);
                car.licensePlateNumber = tbLicensePlateNumber.Text;

                _db.SaveChanges();
            }
            else
            {
                //Adding a new car code here
                var newCar = new TypesOfCars
                {
                    licensePlateNumber = tbLicensePlateNumber.Text,
                    make = tbMake.Text,
                    model = tbModel.Text,
                    vin = tbVin.Text,
                    year = int.Parse(tbYear.Text)
                };
                //Add it to DB
                _db.TypesOfCars.Add(newCar);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
