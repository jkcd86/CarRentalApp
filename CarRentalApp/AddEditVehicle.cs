using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;

        public AddEditVehicle()
        {
            InitializeComponent();
            lblTitle.Text = "Add new vehicle";
            isEditMode = false;
        }

        public AddEditVehicle(TypesOfCar carToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit vehicle";
            isEditMode = true;
            PopulateFields(carToEdit);
        }

        private void PopulateFields(TypesOfCar car)
        {
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
