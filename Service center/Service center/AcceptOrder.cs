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
    public partial class AcceptOrder : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service center\Service center\Database1.mdf;Integrated Security=True";

        public AcceptOrder()
        {
            InitializeComponent();

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> idApplication = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM Applications WHERE IsItAccepted=0", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					idApplication.Add(Convert.ToString(reader.GetInt32(0)));
				}
				reader.Close();
			}

			foreach (var item in idApplication)
			{
				comboBox1.Items.Add(item);
			}

			List<String> employeeLogin = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT Login FROM Employee", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					employeeLogin.Add(reader.GetString(0));
				}
				reader.Close();
			}

			foreach (var item in employeeLogin)
			{
				comboBox2.Items.Add(item);
			}

			connection.Close();
		}

        private void button1_Click(object sender, EventArgs e)
        {
			if(comboBox1.Text == "" || comboBox2.Text == "")
            {
				MessageBox.Show("Не все поля заполнены", "Ошибка принятия заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
            else
            {
				SqlConnection connection = new SqlConnection(sql);
				connection.Open();

				MessageBox.Show("Заказ принят", "Успешное принятие заказа", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				try
                {
					using (SqlCommand cmd = new SqlCommand("Update Applications Set EmployeeLogin = @EmployeeLogin Where id = @id", connection))
					{
						cmd.Parameters.AddWithValue("@EmployeeLogin", comboBox2.Text);
						cmd.Parameters.AddWithValue("@id", comboBox1.Text);
						cmd.ExecuteNonQuery();
					}

					using (SqlCommand cmd = new SqlCommand("Update Applications Set IsItAccepted = @IsItAccepted Where id = @id", connection))
					{
						cmd.Parameters.AddWithValue("@IsItAccepted", "1");
						cmd.Parameters.AddWithValue("@id", comboBox1.Text);
						cmd.ExecuteNonQuery();
					}
				}
				catch
				{
					MessageBox.Show("Ошибка принятия заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
					Close();
				}
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
			Form infromationAboutOrder = new InfromationAboutOrder(comboBox1.Text);
			infromationAboutOrder.ShowDialog();
		}

        private void button3_Click(object sender, EventArgs e)
        {
			if (comboBox1.Text == "")
			{
				MessageBox.Show("Заказ не выбран", "Ошибка отмена заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				SqlConnection connection = new SqlConnection(sql);
				connection.Open();

				MessageBox.Show("Заказ отменен", "Успешная отмена заказа", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				try
                {
					using (SqlCommand cmd = new SqlCommand("Update Applications Set IsItAccepted = @IsItAccepted Where id = @id", connection))
					{
						cmd.Parameters.AddWithValue("@IsItAccepted", "3");
						cmd.Parameters.AddWithValue("@id", comboBox1.Text);
						cmd.ExecuteNonQuery();
					}
				}
				catch
				{
					MessageBox.Show("Ошибка отмены заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
					Close();
				}
			}
		}
    }
}
