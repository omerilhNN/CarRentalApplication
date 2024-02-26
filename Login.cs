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
    public partial class Login : Form
    {
        private readonly CarRentalEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text; 

                var user = _db.Users.FirstOrDefault(q=> q.username == username && q.password == password);  
                if(user == null)
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    var mainWindow = new MainWindow(this);  
                    mainWindow.Show();
                    Hide(); // Don't use close in order to preventing closing the App before MainWindow shows
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"Something went wrong. Please try again!\n Error: {ex.Message}");
            
            }

        }
    }
}
