using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;

namespace RSS
{
    public partial class Form1 : Form
    {
        string stringConnection = "Data Source=93.94.145.55;Initial Catalog=RSS;User Id = RSS;Password = RSS;";
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            string query = "USE RSS;SELECT Employees.Id,Employees.Name,Employees.Active,Salaries.Datetime FROM Employees LEFT JOIN Salaries ON Employees.Id = Salaries.Id";
            query += " WHERE Employees.Name like '%" + textBox1.Text + "%' ";
            if (!checkBox1.Checked)
                query += " and Employees.Active = 1 ";
            if (comboBox1.SelectedIndex == 0)
                query += ";SELECT Employees.Id, avg(Salaries.Salary) as Salar  FROM Employees LEFT JOIN Salaries ON Employees.Id = Salaries.Id  WHERE Employees.Name like '%" + textBox1.Text + "%' ";
            else query += ";SELECT Employees.Id, max(Salaries.Salary) as Salar  FROM Employees LEFT JOIN Salaries ON Employees.Id = Salaries.Id WHERE Employees.Name like '%" + textBox1.Text + "%' ";
            if (!checkBox1.Checked)
                query += " AND Employees.Active = 1 ";
            query += " GROUP BY Employees.Id ORDER BY Salar DESC";
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                    if (ds.Tables[1].Rows[i][1].ToString() == "")
                        ds.Tables[1].Rows[i][1] = 0;
                ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns["Id"] };
                ds.Tables[1].Merge(ds.Tables[0]);
                dataGridView1.DataSource = ds.Tables[1];
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    if (Convert.ToInt16(dataGridView1[1, i].Value) < 20000)
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
            }
        }
    }
}
