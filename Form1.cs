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
    public partial class Form1 : Form
    {
        private readonly CarRentalEntities carRentalEntities;    
        public Form1()
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //SELECT * from TypesOfCars
            //Query the data from DB and keep it in the list
            var cars = carRentalEntities.TypesOfCars.ToList();
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

                if (isValid == true)
                {
                    MessageBox.Show($"Customer name: {customerName}\n\r" +
                                   $"Date Rented: {dateOut}\n\r" +
                                   $"Date Returned: {dateIn}\n\r" +
                                   $"Car Type: {carType}\n\r" +
                                   $"Cost: {cost}\n\r" +
                                   $"Thanks for choosing us!"
                                   );
                }else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch(Exception ex)
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
