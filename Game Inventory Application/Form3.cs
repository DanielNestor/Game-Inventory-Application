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
    public partial class Form3 : Form
    {
        public int formModePub = -1;
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";
        Form1 fpublic = null;
        public Form3(Form1 f1,int formMode)
        {
            //set the public form to f1
            fpublic = f1;
            InitializeComponent();
            //based on the form mode, it will decide what the form looks like
            if (formMode == 1)
            {
                this.Text = "Add New Language";
            }
            if (formMode == 2) {
                this.Text = "Add New Genre";
            }
            if (formMode == 3)
            {
                this.Text = "Add New Console";
            }
            if (formMode == 4)
            {
                this.Text = "Add New Media";
            }
            formModePub = formMode;
         
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //upon being click decide which database to insert into
        private void button1_Click(object sender, EventArgs e)
        {
            //check to see if the input box is empty, if so then return
            if (textBox1.Text == "") {
                MessageBox.Show("Field is Empty");
                return;
            }
            //also check to see if the field already exists
            //if it does, then return an error
            if (fieldAlreadyExists()) {
                MessageBox.Show("Value already exists in Database please try another value");
                return;
            }

            //decide the mode
            String tableName = "";
            String columnName = "";

            if (formModePub == 1) {
                tableName = "Languages";
                columnName = "Language";
                //now verify that the length is appropriate
                if (textBox1.Text.Length > 29) {
                    MessageBox.Show("Maximum Lenguth of Field is 30 Characters, Please provide shorter string");
                    return;
                }

            }
            if (formModePub == 2) {
                tableName = "GameGenre";
                columnName = "Genre";
                if (textBox1.Text.Length > 15)
                {
                    MessageBox.Show("Maximum Lenguth of Field is 15 Characters, Please provide shorter string");
                    return;
                }
            }
            if (formModePub == 3)
            {
                tableName = "GameConsoles";
                columnName = "Console";
                if (textBox1.Text.Length > 15)
                {
                    MessageBox.Show("Maximum Lenguth of Field is 15 Characters, Please provide shorter string");
                    return;
                }
            }
            if (formModePub == 4) {
                tableName = "MediumInventory";
                columnName = "Medium";
                if (textBox1.Text.Length > 15)
                {
                    MessageBox.Show("Maximum Lenguth of Field is 15 Characters, Please provide shorter string");
                    return;
                }
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

            String query = "Insert Into " + tableName + " Values (\'" + textBox1.Text + "\');";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            commqnd.ExecuteNonQuery();

        
            cnn.Close();

            //display a message to verify that it was added, then clear the word from the
            //box
            MessageBox.Show(textBox1.Text + " Has Been Added to the Database");
            textBox1.Text = "";
            //remember to repopulate the combo boxes after you are done
            //updating
            fpublic.populateComboBoxes();

            //close out if it completes
            this.Close();
        }

        //this function checks to see if what has been sent in already appears in the database
        private bool fieldAlreadyExists()
        {


            return false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
