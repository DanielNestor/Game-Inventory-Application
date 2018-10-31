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

namespace Game_Inventory_Application
{
    public partial class Form2 : Form
    {
        //database connection information
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";

        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        #region displayArea
        //do not do anything upon clicking this
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion displayArea

        //upon clicking the search button this function is called
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") {
                MessageBox.Show("Field is Empty Please Enter Something");
                return;
            }
            if (comboBox1.Text == "") {
                MessageBox.Show("Combo Box is empty, Please Select something");
                return;
            }

            String converted = convertToColumnName();
            //generate the query string
            String queryString = "Select * From GamesInventory  Where " +
                converted + " = \'" + textBox1.Text + "\';";
                


          
           

            DataTable tempDataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connetionString)) {
                using (SqlCommand cmd = new SqlCommand(queryString,con)) {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    //now display the output
                    tempDataTable.Load(reader);
                }
            }

            //now actually output the datatable
            dataGridView1.DataSource = tempDataTable;
        }

        private string convertToColumnName()
        {
            if (comboBox1.Text == "Name") {
                return "GAMETITLE";
            }
            if (comboBox1.Text == "Language")
            {
                return "GAMELANGUAGE";
            }
            if (comboBox1.Text == "Genre")
            {
                return "GAMEGENRE";
            }
            if (comboBox1.Text == "Medium")
            {
                return "GAMEMEDIUM";
            }
            return null;
        }
    }
}
