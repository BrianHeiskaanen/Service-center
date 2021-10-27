using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service_center
{
    public partial class InfromationAboutOrder : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service center\Service center\Database1.mdf;Integrated Security=True";

        string id, userLogin, service, description, price, employeeLogin, status, sparePartsPrice;
        string isItAccepted, isItCompleted;

        private void InfromationAboutOrder_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 UserLogin FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    userLogin = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Service FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    service = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Description FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    description = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    price = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 EmployeeLogin FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    employeeLogin = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 IsItAccepted FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    isItAccepted = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 IsItCompleted FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    isItCompleted = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 SparePartsPrice FROM Applications WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    sparePartsPrice = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                if(isItAccepted == "3")
                {
                    status = "Заказ отменен";
                }
                else if (isItAccepted == "0")
                {
                    status = "Заказ в обработке";
                    employeeLogin = "Не назначен";
                }
                else if(isItAccepted == "1" && isItCompleted == "0")
                {
                    status = "Заказ принят";
                }
                else if (isItAccepted == "2" && isItCompleted == "0")
                {
                    status = "Заказ выполняется";
                }
                else if (isItCompleted == "1")
                {
                    status = "Заказ готов";
                }
                else
                {
                    status = "Неизвестно";
                }

                textBox1.Text = userLogin;
                textBox2.Text = service;
                richTextBox1.Text = description;
                textBox3.Text = price;
                textBox4.Text = employeeLogin;
                textBox5.Text = status;
                textBox6.Text = sparePartsPrice;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public InfromationAboutOrder(string id)
        {
            InitializeComponent();
            this.id = id;
        }
    }
}
