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
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities _db;

        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add new Record";
            this.Text = "Add new Record";
            isEditMode = false;
            _db = new CarRentalEntities();
        }

        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Record";
            this.Text = "Edit Record";
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
            dtReturned.Value = (DateTime)recordToEdit.DateReturned;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblRecordId.Text = recordToEdit.id.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Select * from TypesOfCar
            //var cars = carRentalEntities.TypesOfCars.ToList();

            var cars = _db.TypesOfCars
                .Select(q => new { ID = q.Id, Name = q.Make + " " + q.Model })
                .ToList();

            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "Id";
            cbTypeOfCar.DataSource = cars;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                // MessageBox.Show($"Thank you for renting {tbCustomerName.Text}");

                string customerName = tbCustomerName.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);

                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter missing data.\n\r";
                }


                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal date selection.\n\r";
                }


                //if(isValid == true)
                if (isValid)
                {
                    if (isEditMode)
                    {
                        var id = int.Parse(lblRecordId.Text);
                        var rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                        rentalRecord.CustomerName = customerName;
                        rentalRecord.DateRented = dateOut;
                        rentalRecord.DateReturned = dateIn;
                        rentalRecord.Cost= (decimal)cost;
                        rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;

                        if (!isEditMode)
                            _db.CarRentalRecords.Add(rentalRecord);

                        _db.SaveChanges();

                        MessageBox.Show($"Customer Name: {customerName}\n\r" +
                            $"Date Rented: {dateOut}\n\r" +
                            $"Datel Returned: {dateIn}\n\r" +
                            $"Cost: {cost}\n\r " +
                            $"Car Type: {carType}\n\r" +
                            $"THANK YOU FOR YOUR BUSINESS");
                        Close();
                    }
                    else
                    {
                        var rentalRecord = new CarRentalRecord();
                        rentalRecord.CustomerName = customerName;
                        rentalRecord.DateRented = dateOut;
                        rentalRecord.DateReturned = dateIn;
                        rentalRecord.Cost = (decimal)cost;
                        rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;

                        _db.CarRentalRecords.Add(rentalRecord);
                        _db.SaveChanges();

                        MessageBox.Show($"Customer Name: {customerName}\n\r" +
                            $"Date Rented: {dateOut}\n\r" +
                            $"Datel Returned: {dateIn}\n\r" +
                            $"Cost: {cost}\n\r " +
                            $"Car Type: {carType}\n\r" +
                            $"THANK YOU FOR YOUR BUSINESS");
                    }
                    Close();
                }
                else
                {
                    isValid = false;
                    MessageBox.Show(errorMessage);
                }

            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                //throw;
            }

            
        }

        private void dtRented_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
