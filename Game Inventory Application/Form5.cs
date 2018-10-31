using System;
using System.Collections;
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
    public partial class Form5 : Form
    {
        String publicID = "";
        Hashtable TableLookup = new Hashtable();
        Boolean comboBox1Filled = false;
        //if running on a different machine, change this string
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";
        public Form5(String s)
        {
            //proceed to populate the hashtable

            publicID = s;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //in the case that the button 1 text asks to submit then update
            if (button1.Text =="Submit") {
                updateTable();
            }


            //check to see if the first combo box is filled
            if (comboBox1.Text != "")
            {
                button1.Text = "Update";
                comboBox1Filled = true;
            } else if (comboBox1.Enabled == false) {
                //do nothing
            }
            else {
                MessageBox.Show("Empty Field, Please Select Something");
                comboBox1Filled = false;
                return;
            }



            //if the first combo box is filled lock it
            comboBox1.Enabled = false;
 
            if (comboBox1.Text == "Title")
            {
                textBox1.Visible = true;
                label2.Visible = true;
                return;
            }
            else if (comboBox1.Text == "Legitamacy")
            {
                textBox1.Visible = true;
                label2.Visible = true;
                populateField();
                button1.Text = "Submit";
            }
            else if (comboBox1.Text == "Console" || comboBox1.Text == "Medium" || comboBox1.Text == "Condition" || comboBox1.Text == "Language" || comboBox1.Text ==  "Genre")
            {
                label2.Visible = true;
                comboBox2.Visible = true;
                populateField();
                button1.Text = "Submit";
            }
            else
            {
                comboBox2.Visible = true;
                label2.Visible = true;
                return;
            }
                

           

            //verify that the fields are selected


        
        }

        //this function updates the table whenever
        //it is selected
        private void updateTable()
        {

            string newValue = null;

            //based on the type of the 
            if (comboBox1.Text == "Language" || comboBox1.Text == "Genre" || comboBox1.Text == "Console") {
                newValue = comboBox2.Text;
            }
            if (comboBox1.Text == "Legitimacy") {
                if (comboBox2.Text == "Legit")
                {
                    newValue = "1";
                }
                else {
                    newValue = "0";
                }
            }
            //to begin connect to database
            //connect to the database
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Update GamesInventory SET " + getColumnName() + " = \'" + newValue + "\' WHERE ID = " + publicID + ";";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            SqlDataReader sqlOut;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            //execute the query
            sqlOut = cmd.ExecuteReader();


            MessageBox.Show("Table Updated Successfully");
            this.Close();
        }

        private string getColumnName()
        {
            if (comboBox1.Text == "Title")
            {
                return "GAMETITLE";
            }
            else if (comboBox1.Text == "Legitimacy") {
                return "ISLEGIT";
            }
            else if ( comboBox1.Text == "Language")
            {
                return "GAMELANGUAGE";
            }
            else if (comboBox1.Text == "Console")
            {
                return "GAMECONSOLE";
            }
            else if (comboBox1.Text == "Medium")
            {
                return "GAMEMEDIUM";
            }
            else if (comboBox1.Text == "Genre")
            {
                return "GAMEGENRE";
            }
            else if (comboBox1.Text == "Condition")
            {
                return "GAMECONDITION";
            }
            else
            {
                comboBox2.Visible = true;
                label2.Visible = true;
                return "null";
            }

        }

        //populate field 
        private void populateField()
        {

            //in the case of condition there is no need to query anything
            if (comboBox1.Text == "Condition") {
                comboBox2.Items.Add("Excellent");
                comboBox2.Items.Add("Fair");
                comboBox2.Items.Add("Poor");
                comboBox2.Items.Add("Unplayable");
                return;
            }
            //same thing for legitimacy
            if (comboBox1.Text == "Legitimacy") {
                comboBox2.Items.Add("Legit");
                comboBox2.Items.Add("Illegit");
                return;
            }
           
            //to begin connect to database
            //connect to the database
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Select * From " + getTableName() +";";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            SqlDataReader sqlOut;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            //add the elements to the combobox
            sqlOut = cmd.ExecuteReader();

            //loop through the results set
            while (sqlOut.Read())
            {
                String s = sqlOut.GetString(0);
                comboBox2.Items.Add(s);
            }

            
        }

        private string getTableName()
        {
            if (comboBox1.Text == "Console") {
                return "GameConsoles";
            }
            if (comboBox1.Text == "Language") {
                return "Languages";
            }
            if (comboBox1.Text == "Medium") {
                return "MediumInventory";
            }
            if (comboBox1.Text == "Genre")
            {
                return "GameGenre";
            }

            return "";
        }

      //  private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
