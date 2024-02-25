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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _db; //Container name -> inside CarRentalDb.edmx file -> represents the database object
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var record = _db.CarRentalRecord.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    //delete vehicle from table
                    _db.CarRentalRecord.Remove(record);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            addRentalRecord.MdiParent = this.MdiParent;
            addRentalRecord.Show();
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();

            }catch(Exception ex)
            {

            }
        }

        private void PopulateGrid()
        {
            var records = _db.CarRentalRecord.Select(q => new
            {
                Customer = q.CustomerName,
                DateOut = q.DateRented,
                DateIn = q.DateReturned,
                Id = q.id,
                Cost = q.Cost,
                Car = q.TypesOfCars.make + " " + q.TypesOfCars.model
            });
            gvRecordList.DataSource = records;
            gvRecordList.Columns["Date In"].HeaderText = "Date in";
            gvRecordList.Columns["Date Out"].HeaderText = "Date out";
            gvRecordList.Columns["Id"].Visible = false;
           
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {

            try
            {
                //Get selected row id
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //Query db for record
                var record = _db.CarRentalRecord.FirstOrDefault(q => q.id == id);

                ////launch AddEditVehicle window with data 
                var addEditRentalRecord = new AddEditRentalRecord(record);
                addEditRentalRecord.MdiParent = this.MdiParent;
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
