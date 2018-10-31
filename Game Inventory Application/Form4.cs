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
    public partial class Form4 : Form
    {
        Form1 fpublic = null;
        int publicMode = 0;
        //the connection string used for all database connections
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";
        public Form4(Form1 f1, int mode)
        {
            //copy the public form over
            fpublic = f1;
            publicMode = mode;
            InitializeComponent();
            populateComboBox(mode);

        }

        #region PopulateComboBoxes
        private void populateComboBox(int mode)
        {
            //decide based on the mode what to put in
            String table = null;
            if (mode == 1) {
                table = "Languages";
            }if (mode == 2) {
                table = "GameGenre";
            }if (mode == 3) {
                table = "GameConsoles";
            }if (mode == 4) {
                table = "MediumInventory";
            }


            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Select * From " + table +  ";";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            SqlDataReader sqlOut;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            //add the elements to the combobox
            int y = 0;
            sqlOut = cmd.ExecuteReader();

            //loop through the results set
            while (sqlOut.Read())
            {
                String s = sqlOut.GetString(0);
                comboBox1.Items.Add(s);
            }

            cnn.Close();
        }
        #endregion PopulateComboBoxes

    
        private void Form4_Load(object sender, EventArgs e)
        {

        }
        //this button decides what to do upon the user selecting a field;
        private void button1_Click(object sender, EventArgs e)
        {
            //perform a quick check to verify that it is not empty
            if (comboBox1.Text == "") {
                MessageBox.Show("Field is empty, Please Select a Value");
                return;
            }

            //upon clicking this, remove the selected field
            RemoveItem();
            MakePopupMessage();
            RefreshFields();
            this.Close();
        }

        private void RefreshFields()
        {
            fpublic.populateComboBoxes();
        }

        private void MakePopupMessage()
        {
            MessageBox.Show("Value Has Been Removed");
        }

        private void RemoveItem()
        {
            //decide the mode
            String tableName = "";
            String columnName = "";

            if (publicMode == 1)
            {
                tableName = "Languages";
                columnName = "Language";
            }
            if (publicMode == 2)
            {
                tableName = "GameGenre";
                columnName = "Genre";
            }
            if (publicMode == 3)
            {
                tableName = "GameConsoles";
                columnName = "Console";
            }
            if (publicMode == 4)
            {
                tableName = "MediumInventory";
                columnName = "Medium";
            }


            //to begin connect to database
            //connect to the database
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Delete From " + tableName + " Where " + columnName + " =\'" +  comboBox1.Text + "\';";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            commqnd.ExecuteNonQuery();


            cnn.Close();
        }
    }
}
