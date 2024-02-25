using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApplication
{
    public partial class AddEditRentalRecord : Form
    {
        private readonly CarRentalEntities _db;
        private bool isEditMode;
        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Rental";
            this.Text = "Add New Rental";
            isEditMode = false;
            _db = new CarRentalEntities();  
        }
        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
            }
        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
             tbCustomerName.Text = recordToEdit.CustomerName;
             dtRented.Value = (DateTime)recordToEdit.DateRented;
             dtReturn.Value = (DateTime)recordToEdit.DateReturned;
             tbCost.Text = recordToEdit.Cost.ToString();
             lblRecordId.Text = recordToEdit.id.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SELECT * from TypesOfCars
            //Query the data from DB and keep it in the list
            //var cars = carRentalEntities.TypesOfCars.ToList();
            var cars = _db.TypesOfCars
                .Select(q => new { id = q.id, name = q.make + " " + q.model }).ToList();

            cbTypeCar.DisplayMember = "Name"; //Set display member to Name
            cbTypeCar.ValueMember = "id"; // set store the value which is id
            cbTypeCar.DataSource = cars; // initialize it with cars
        }



        private void dateTimeReturn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturn.Value;
                double cost = Convert.ToDouble(tbCost.Text);
                var carType = cbTypeCar.SelectedItem.ToString();
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter the missing data.\n\r";
                }
                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection\n\r";
                }

                if ((!string.IsNullOrWhiteSpace(customerName) || !string.IsNullOrWhiteSpace(carType) || dateOut < dateIn))
                    isValid = true;
                //if(isValid == true)
                if (isValid)
                {
                    //Declare an object of the record to be added
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode)
                    {
                        //If in edit mode, then get the ID and retrieve the record from the database and place
                        //the result in the record object
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecord.FirstOrDefault(q => q.id == id);
                    }
                    //Populate record object with values from the form 
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeCar.SelectedValue;
                    //If not in edit mode, then add the record object to the database
                    if (!isEditMode)
                        _db.CarRentalRecord.Add(rentalRecord);
                    //Save Changes made to the entity
                    _db.SaveChanges();
                    //TODO: Manage rental record
                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: {cost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"THANK YOU FOR YOUR BUSINESS");

                    Close();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw; // end the program
            }
            
            
        }

        private void dateTimeRented_ValueChanged(object sender, EventArgs e)
        {

        }
        private void costText_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbTypeCar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
