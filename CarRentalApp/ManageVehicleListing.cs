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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db;

        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            // SELECT * FROM TypeCars
            //var cars = _db.TypesOfCars.ToList();

            // SELECT Id as CarID, name as CarName from TypesOfCars

            /*
            var cars = _db.TypesOfCars.
                Select(q => new { CarID = q.Id, CarName = q.Make }).
                ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[0].HeaderText = "ID";
            gvVehicleList.Columns[1].HeaderText = "NAME";
        
            */

            // old version

            /*
            var cars = _db.TypesOfCars
                .Select(q => new 
                { 
                    Make = q.Make, 
                    Model = q.Model, 
                    VIN = q.VIN, 
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber ,
                    Id = q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            gvVehicleList.Columns[5].Visible = false;
            //gvVehicleList.Columns[0].HeaderText = "ID";
            //gvVehicleList.Columns[1].HeaderText = "NAME";
            */

            try
            {
                PopulateGrid();
            }
            catch ( Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // New function to populate Grid. Can be called anytime we need a grid refresh
        private void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            // Hide the column for ID. Changed from the hard coded column value to the name,
            // to make it mor dynamic.
            gvVehicleList.Columns["Id"].Visible = false;
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try { 
                // get id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;


                // query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                // launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.ShowDialog();
            addEditVehicle.MdiParent = this.MdiParent;
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try {
            // get id of select row
            var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

            // query database for record
            var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

            // delete vehicle from table
            _db.TypesOfCars.Remove(car);
            _db.SaveChanges();

            gvVehicleList.Refresh();
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // SImple Refresh Option
            PopulateGrid();
            gvVehicleList.Update();
            gvVehicleList.Refresh();
        }
    }
}
