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
    public partial class Form1 : Form
    {
        Boolean isEntryMode = true;
        Boolean updateIsYes = false;
        //if running on a different machine, change this string
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";
                                        
        public Form1()
        {
            InitializeComponent();
        }

        //needs to be removed
        private void entryModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //in before doing anything, verify that all fields are filled
            if (allFieldsFilled() == false) {
                return;
            }

            //now check for game legitimacy
            int isLegit = 1;
            if(radioButton2.Checked == true)
            {
                isLegit = -1;
            }

            //declare a new game object and this game will be given a unique id
            Game g1 = new Game(textBox1.Text,comboBox2.Text,comboBox3.Text,comboBox4.Text,comboBox5.Text, comboBox1.Text, isLegit);

            //connect to the database
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            try {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                
            } catch 
            (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "INSERT INTO GamesInventory (ID,GAMETITLE,GAMELANGUAGE,GAMEGENRE,GAMECONSOLE,GAMEMEDIUM,GAMECONDITION,ISLEGIT,ADDRESS) VALUES" +
                "(\'" + g1.getId() + "\'," +
                "\'" + g1.getTitle() + "\'," +
                "\'" + g1.getLanguage() + "\'," +
                "\'" + g1.getGenre() + "\'," +
                "\'" + g1.getConsole() + "\'," +
                "\'" + g1.getMedium() + "\'," +
                "\'" + g1.getCondition() + "\'," +
                  "\'" + g1.getLegit() + "\'," +
                  "\'" + "NULL" + "\');";

            SqlCommand commqnd = new SqlCommand(query, cnn);
            commqnd.ExecuteNonQuery();
            //now generate the query to add

            cnn.Close();

            //send a message indicating the game was added to the database
            MessageBox.Show("Game Added To Database");
        }

        //this function verifys that all fields are filled in the 
        private bool allFieldsFilled()
        {
            if (textBox1.Text == "") {
                MessageBox.Show("Game Title Field is Empty");
                return false;
            }
            if (comboBox1.Text == "") {
                MessageBox.Show("Game Language Field is Empty");
                return false;
            }
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Game Console Field is Empty");
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Upon loading the form, populate the combo boxes with data from the database
            populateComboBoxes();
        }

        #region ComboBoxPopulation
        public void populateComboBoxes()
        {
            //before populating clear all of the combo boxes
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            comboBox3.Items.Clear();
            //now actually poupulate the combo boxes
            populateLanguageBox();
            populateGenreBox();
            populateConsoleBox();
            populateInventoryBox();

            
        }
        private void populateConsoleBox()
        {
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

            String query = "Select * From GameConsoles;";
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

            cnn.Close();
        }

        private void populateLanguageBox()
        {
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

            String query = "Select * From Languages;";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            SqlDataReader sqlOut;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            //add the elements to the combobox
            sqlOut = cmd.ExecuteReader();

            //loop through the results set
            while (sqlOut.Read()) {
                String s = sqlOut.GetString(0);
                comboBox1.Items.Add(s);
            }
            
            cnn.Close();
        }

        private void populateGenreBox()
        {
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Select * From GameGenre;";
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
                comboBox5.Items.Add(s);
            }
            cnn.Close();
        }

        private void populateInventoryBox()
        {
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

            String query = "Select * From MediumInventory;";
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
                comboBox3.Items.Add(s);
            }

            cnn.Close();
        }


        #endregion ComboBoxPopulation
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            updateIsYes = true;
            //set the id items to visible
            textBox3.Visible = true;
            label10.Visible = true;
            button3.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            updateIsYes = false;
            //set the id items to visible
            textBox3.Visible = false;
            label10.Visible = false;
            button3.Visible = false;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            //repopulate the fields after updating
        }

        #region Form3 Functions
        //this set of 4 functions are for the 4 fields that need updating
        private void addToLanguageListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this,1);
            f3.Show();
            RefreshFields();
        }
        private void addToGenreListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this,2);
            f3.Show();
            RefreshFields();
        }
        private void addToConsoleListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this,3);
            f3.Show();
            RefreshFields();
        }
        private void addToMediumListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this,4);
            f3.Show();
            RefreshFields();
        }
        //this function refreshes the combo boxes on the form 1
        //after the database has been updated it refreshes all of
        //the fields by getting the values from the database
        //and then populating the fields with the data
        private void RefreshFields()
        {
          //  throw new NotImplementedException();
        }




        #endregion Form3 Functions

        #region RemoveLanguageFucntions
        private void removeLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this, 1);
            f4.Show();
        }
        private void removeGenreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this, 2);
            f4.Show();
        }
        private void removeConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this, 3);
            f4.Show();
        }
        private void removeMediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this, 4);
            f4.Show();
        }



        #endregion RemoveLanguageFunctions

        //this function clears all of the attributes on the input page
        private void button2_Click(object sender, EventArgs e)
        {
            //set all of the element fields to empty
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            textBox1.Text = "";

        }

        //now setup to update an application
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") {
                MessageBox.Show("Text Box is Empty");
                return;
            }
            if (foundinDatabase() == false) {
                MessageBox.Show("Id Not Found");
                return;
            }

            //then go on to open up a new form
            Form5 f5 = new Form5(textBox3.Text);
            f5.Show();
            return;
        }

        //this function returns a true if the id is found in the database
        //and then update
        private bool foundinDatabase()
        {
            String id = textBox3.Text;

            //now query the database
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

            String query = "Select * From GamesInventory Where Id = " + id + ";";
            SqlCommand commqnd = new SqlCommand(query, cnn);
            SqlDataReader sqlOut;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            //add the elements to the combobox
            sqlOut = cmd.ExecuteReader();

            //loop through the results set
            int count = 0;
            while (sqlOut.Read())
            {
                count++;
            }

            cnn.Close();

            if (count == 1) {
                return true;
            }

            return false;
        }
    }
}
