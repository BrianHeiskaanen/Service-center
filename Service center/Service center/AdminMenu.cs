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
    public partial class AdminMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service center\Service center\Database1.mdf;Integrated Security=True";

        SqlDataAdapter adapter, adapter1, adapter2, adapter3, adapter4 = null;

        public AdminMenu()
        {
            InitializeComponent();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Сервисный центр\nРазработчик: Гимпель Дмитрий Сергеевич\nНомер группы: 38ТП", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form registration = new Registration(false);
            registration.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteEmployee = new DeleteEmployee();
            deleteEmployee.ShowDialog();
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form addingService = new AddingService();
            addingService.ShowDialog();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form acceptOrder = new AcceptOrder();
            acceptOrder.ShowDialog();
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form deleteService = new DeleteService();
            deleteService.ShowDialog();
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form addingSparePart = new AddingSparePart();
            addingSparePart.ShowDialog();
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form deleteSpareParts = new DeleteSpareParts();
            deleteSpareParts.ShowDialog();
        }

        private void карточкаКлинтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form client_sCard = new Client_sCard();
            client_sCard.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet1.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet1.GetChanges(DataRowState.Modified);
                adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
                adapter.Update(tempDataSet, "Users");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet2.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet2.GetChanges(DataRowState.Modified);
                adapter1.UpdateCommand = new SqlCommandBuilder(adapter1).GetUpdateCommand();
                adapter1.Update(tempDataSet, "Employee");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet3.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet3.GetChanges(DataRowState.Modified);
                adapter2.UpdateCommand = new SqlCommandBuilder(adapter2).GetUpdateCommand();
                adapter2.Update(tempDataSet, "Applications");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet4.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet4.GetChanges(DataRowState.Modified);
                adapter3.UpdateCommand = new SqlCommandBuilder(adapter3).GetUpdateCommand();
                adapter3.Update(tempDataSet, "Services");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.database1DataSet5.HasChanges(DataRowState.Modified)) return;
                DataSet tempDataSet = database1DataSet5.GetChanges(DataRowState.Modified);
                adapter4.UpdateCommand = new SqlCommandBuilder(adapter4).GetUpdateCommand();
                adapter4.Update(tempDataSet, "SpareParts");
            }
            catch
            {
                MessageBox.Show("Вы уже обновили данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(textBox8.Text, true);
            personalInformation.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(textBox1.Text, false);
            personalInformation.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox2.Text);
            infromationAboutOrder.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form informationAboutService = new InformationAboutService(textBox3.Text);
            informationAboutService.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form informationAboutSparePart = new InformationAboutSparePart(textBox4.Text);
            informationAboutSparePart.ShowDialog();
        }

        private void карточкаМастераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form employee_sCard = new Employee_sCard();
            employee_sCard.ShowDialog();
        }

        private void справкаРемонтовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form repairHelp = new RepairHelp();
            repairHelp.ShowDialog();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet5.SpareParts". При необходимости она может быть перемещена или удалена.
            this.sparePartsTableAdapter.Fill(this.database1DataSet5.SpareParts);

            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet4.Services". При необходимости она может быть перемещена или удалена.
            //this.servicesTableAdapter.Fill(this.database1DataSet4.Services);
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services; ", connection))
            {
                adapter3 = new SqlDataAdapter(cmd);
                adapter3.Fill(this.database1DataSet4.Services);
                dataGridView4.DataSource = this.database1DataSet4.Services;
            }

            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet3.Applications". При необходимости она может быть перемещена или удалена.
            //this.applicationsTableAdapter.Fill(this.database1DataSet3.Applications);
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                adapter2.Fill(this.database1DataSet3.Applications);
                dataGridView3.DataSource = this.database1DataSet3.Applications;
            }

            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet2.Employee". При необходимости она может быть перемещена или удалена.
            this.employeeTableAdapter.Fill(this.database1DataSet2.Employee);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet1.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.database1DataSet1.Users);

            connection.Close();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            this.database1DataSet5.SpareParts.Clear();
            this.database1DataSet4.Services.Clear();
            this.database1DataSet3.Applications.Clear();
            this.database1DataSet2.Employee.Clear();
            this.database1DataSet1.Users.Clear();

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(this.database1DataSet1.Users);
                dataGridView1.DataSource = this.database1DataSet1.Users;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(this.database1DataSet2.Employee);
                dataGridView2.DataSource = this.database1DataSet2.Employee;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                adapter2.Fill(this.database1DataSet3.Applications);
                dataGridView3.DataSource = this.database1DataSet3.Applications;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services; ", connection))
            {
                adapter3 = new SqlDataAdapter(cmd);
                adapter3.Fill(this.database1DataSet4.Services);
                dataGridView4.DataSource = this.database1DataSet4.Services;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM SpareParts; ", connection))
            {
                adapter4 = new SqlDataAdapter(cmd);
                adapter4.Fill(this.database1DataSet5.SpareParts);
                dataGridView5.DataSource = this.database1DataSet5.SpareParts;
            }

            connection.Close();
        }
    }
}
