using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Inventory_Application
{

   

    class Game
    {
        //data for the game class
        int gameId = 0;
        String gameTitle = "";
        String gameGenre = "";
        String gameConsole = "";
        String gameMedium = "";
        String gameCondition = "";
        String gameLanguage = "";
        int isLegit = 1;
        String address = "Location if I decide to use it";
        public String connetionString = "Data Source=DESKTOP-GN9HIE4;Initial Catalog=InventoryDatabase;Integrated Security=True;Pooling=False";

        public Game(String gTtl,String gCnsl,String gMed,String gCond, String gGen, String gLang, int legit) {
            gameTitle = gTtl;
            gameId = generateGameId();
            gameConsole = gCnsl;
            gameMedium = gMed;
            gameCondition = gCond;
            gameGenre = gGen;
            gameLanguage = gLang;
            isLegit = legit;
        }

        //this function generates a random game id, 
        private int generateGameId()
        {
             
            //to begin generate a random number between 1 and 1million
            int newId = generateId();
            while (checkDatabase(newId) == false) {
                newId = generateId();
            }

            return newId;
        }

        private int generateId()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000000);
            return randomNumber;
        }

        //this function checks the data to verify that the existing
        //id does not exist in the database returns true
        private bool checkDatabase(int newId)
        {
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch
          (Exception ex)
            { MessageBox.Show("Can not open connection ! "); }

            String query = "Select * From GamesInventory WHERE ID = \'"+ newId + "\';";
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
            if (count > 0) {
                return false;
            }
            return true;
        }
        #region  accessors
        public String getMedium() {
            return gameMedium;
        }
        public String getCondition() {
            return gameCondition;
        }
        public String getLanguage()
        {
            return gameLanguage;
        }
        public String getConsole()
        {
            return gameConsole;
        }
        public String getGenre()
        {
            return gameGenre;
        }

        public String getTitle()
        {
            return gameTitle;
        }

        public int getId()
        {
            return gameId;
        }

        public int getLegit() {
            return isLegit;
        }
        #endregion accessors


        #region modifiers


        #endregion modifiers

    }


}
