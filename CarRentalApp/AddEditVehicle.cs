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
        private readonly CarRentalEntities _db = new CarRentalEntities();

        public AddEditVehicle()
        {
            InitializeComponent();
            lblTitle.Text = "Add new vehicle";
            this.Text = "Add new vehicle";
            isEditMode = false;
            _db = new CarRentalEntities();
        }
         
        public AddEditVehicle(TypesOfCar carToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit vehicle";
            this.Text = "Edit vehicle";
            if (carToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {

            }
            isEditMode = true;
            _db = new CarRentalEntities();
            PopulateFields(carToEdit);
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (isEditMode == true)
            if (isEditMode) 
            {
                var id = int.Parse(lblId.Text);
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                car.Model = tbModel.Text;
                car.Make = tbMake.Text;
                car.VIN = tbVIN.Text;
                car.Year = int.Parse(tbYear.Text);
                car.LicensePlateNumber = tbLicenseNum.Text;

                _db.SaveChanges();
            }
            else
            {
                // Add Code here
                var newCar = new TypesOfCar
                {
                    LicensePlateNumber = tbLicenseNum.Text,
                    Make = tbMake.Text,
                    Model = tbModel.Text,
                    VIN = tbVIN.Text,
                    Year = int.Parse(tbYear.Text)
                };

                _db.TypesOfCars.Add(newCar);
                _db.SaveChanges();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }
    }
}
