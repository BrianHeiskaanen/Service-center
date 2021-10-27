using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service_center
{
    public partial class EmployeeMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service center\Service center\Database1.mdf;Integrated Security=True";

        SqlDataAdapter adapter, adapter1 = null;
        DataTable table, table1 = null;

        string id, employeeLogin;

        public EmployeeMenu(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Сервисный центр\nРазработчик: Гимпель Дмитрий Сергеевич\nНомер группы: 38ТП", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EmployeeMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void личнаяИнформацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(id, false);
            personalInformation.ShowDialog();
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string password;

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Password FROM Employee WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                password = cmd.ExecuteScalar().ToString();
            }

            Form passwordChange = new PasswordChange(password, id, false);
            passwordChange.ShowDialog();

            connection.Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addingSparePart = new AddingSparePart();
            addingSparePart.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteSpareParts = new DeleteSpareParts();
            deleteSpareParts.ShowDialog();
        }

        private void обработатьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string employeeLogin;

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Login FROM Employee WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                employeeLogin = cmd.ExecuteScalar().ToString();
            }

            connection.Close();

            Form orderProcessing = new OrderProcessing(employeeLogin);
            orderProcessing.ShowDialog();
        }

        private void закончитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form completeOrder = new CompleteOrder(employeeLogin);
            completeOrder.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox1.Text);
            infromationAboutOrder.ShowDialog();
        }

        private void EmployeeMenu_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.SpareParts". При необходимости она может быть перемещена или удалена.
            this.sparePartsTableAdapter.Fill(this.database1DataSet.SpareParts);

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Login FROM Employee WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                employeeLogin = cmd.ExecuteScalar().ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE EmployeeLogin=@EmployeeLogin; ", connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeLogin", employeeLogin);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            connection.Close();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            this.database1DataSet.SpareParts.Clear();

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE EmployeeLogin=@EmployeeLogin; ", connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeLogin", employeeLogin);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM SpareParts; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(this.database1DataSet.SpareParts);
                dataGridView2.DataSource = this.database1DataSet.SpareParts;
            }

            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form informationAboutSparePart = new InformationAboutSparePart(textBox8.Text);
            informationAboutSparePart.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet.GetChanges(DataRowState.Modified);
                adapter1.UpdateCommand = new SqlCommandBuilder(adapter1).GetUpdateCommand();
                adapter1.Update(tempDataSet, "SpareParts");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            if (checkBox1.Checked)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE EmployeeLogin=@EmployeeLogin AND IsItCompleted=@IsItCompleted; ", connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeLogin", employeeLogin);
                    cmd.Parameters.AddWithValue("@IsItCompleted", "0");
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE EmployeeLogin=@EmployeeLogin; ", connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeLogin", employeeLogin);
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }

            connection.Close();
        }
    }
}
