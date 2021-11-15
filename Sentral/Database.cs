using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Npgsql;

namespace Sentral
{
    public class Database
    {
        private bool _connected = false;
        public bool Connected {get => _connected;}


        public NpgsqlConnection con;
        public NpgsqlCommand cmd;


        /// <summary>
        /// Initialises an object to handle Database interaction
        /// </summary>
        /// <param name="host">hostname of database server Ie. ip-address or uri</param>
        /// <param name="userName">username of user for logging into database</param>
        /// <param name="password">password of user for logging into database</param>
        /// <param name="database">name of database on server to use</param>
        public Database(string host, string userName, string password, string database)
        {
            //establish connection
            con = new NpgsqlConnection($"Host={host};Username={userName};Password={password};Database={database}");

            //establish queryobject
            cmd = new NpgsqlCommand(); //it seems to understand that "con" is the connection object

            //connecting to database
            Connect();
            //Task.Run(() => { Connect(); }); //if needed to be run async
        }


        /// <summary>
        /// establishing connection for database
        /// </summary>
        private void Connect()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                _connected = true;
            }
            catch (Exception)
            {
                _connected = false;
            }
        }


        /// <summary>
        /// executes a query but return rows affected instead of values returned
        /// </summary>
        /// <param name="query">query to send</param>
        /// <returns></returns>
        public int NonQuery(string query)
        {
            if (!_connected) Connect();

            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// queries the database for values
        /// </summary>
        /// <param name="query">the query to send</param>
        /// <returns></returns>
        public string Query(string query)
        {
            if (!_connected) Connect();

            cmd.CommandText = query;
            string data = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    data = $"{rdr.GetDateTime(0),0} {rdr.GetDateTime(1),0} {rdr.GetInt16(2),0}";
                }
            }
            return data;
        }






        // Brukerspørringer
        //**********************************************************************************************************

        /// <summary>
        /// Queries the database for values from Bruker
        /// </summary>
        /// <param name="KortID">the query to send</param>
        /// <returns>List of string with [0]Gyldig_Fra, [1]Gyldig_Til, [2]Pin or NULL</returns>
        public string[] QueryBruker(string KortID)
        {
            if (!_connected) Connect();
            if (QExistKortID(KortID))
            {
                cmd.CommandText = "SELECT Gyldig_Fra, Gyldig_Til, PIN FROM Bruker WHERE KortID = " + KortID;
                string data = "";
                using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        data = $"{rdr.GetDateTime(0),0},{rdr.GetDateTime(1),0},{rdr.GetInt16(2),0}";
                    }
                }
                return data.Split(',');
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Queries the database if card exists in the database
        /// </summary>
        /// <param name="KortDuVilSeOmFinnes"></param>
        /// <returns>
        /// TRUE if card exists in DB 
        /// False if card does not exist in DB
        /// </returns>
        public bool QExistKortID(string KortDuVilSeOmFinnes)
        {
            if (!_connected) Connect();

            cmd.CommandText = "SELECT KortID FROM Bruker WHERE KortID = " + KortDuVilSeOmFinnes;
            string data = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    data = $"{rdr.GetInt16(0),0}";
                }
            }
            if (data.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }




        // Rapport queries
        //**********************************************************************************************************

        /// <summary>
        /// Creates a report of all Brukere in DB
        /// </summary>
        public void QReportBrukere(string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "SELECT * FROM Bruker ORDER BY Fornavn, Etternavn;";
            Bruker User;
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15} {rdr.GetName(1),15} {rdr.GetName(2),20} {rdr.GetName(5),5}");
                    while (rdr.Read())
                    {
                        User = new Bruker($"{ rdr.GetInt16(5),5}", db);
                        try
                        {
                            User.FirstName = $"{rdr.GetString(0),15}";
                        }
                        catch (Exception)
                        { User.FirstName = "           NULL"; }


                        try
                        {
                            User.SurName = $"{rdr.GetString(1),15}";
                        }
                        catch (Exception)
                        { User.SurName = "           NULL"; }


                        try
                        {
                            User.Email = $"{rdr.GetString(2),20}";
                        }
                        catch (Exception)
                        { User.Email = "                NULL"; }

                        sw.WriteLine(User.ToString());

                    }
                    sw.Close();
                }
            }
            

        }

        /// <summary>
        /// Creates a report of all Adganger between given timestamps
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Slutt"></param>
        public void QReportAdganger(DateTime Start, DateTime Slutt, string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15}");
                    while (rdr.Read())
                    {
                        

                        sw.WriteLine();

                    }
                    sw.Close();
                }
            }
        }


        /// <summary>
        /// Creates a report of all not approved instances in Adganger on one Kortleser
        /// </summary>
        /// <param name="KortleserID"></param>
        public void QReportIkkeGodkjent(string KortleserID, string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15}");
                    while (rdr.Read())
                    {


                        sw.WriteLine();

                    }
                    sw.Close();
                }
            }
        }


        /// <summary>
        /// Creates a report of every Bruker with 10 or more failed Adganger within an hour
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Slutt"></param>
        public void QReportBrukereMedTiFeiledeForsok(DateTime Start, DateTime Slutt, string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "";
            Bruker User;
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15} {rdr.GetName(1),15} {rdr.GetName(2),20} {rdr.GetName(5),5}");
                    while (rdr.Read())
                    {
                        User = new Bruker($"{ rdr.GetInt16(5),5}", db);
                        try
                        {
                            User.FirstName = $"{rdr.GetString(0),15}";
                        }
                        catch (Exception)
                        { User.FirstName = "           NULL"; }


                        try
                        {
                            User.SurName = $"{rdr.GetString(1),15}";
                        }
                        catch (Exception)
                        { User.SurName = "           NULL"; }


                        try
                        {
                            User.Email = $"{rdr.GetString(2),20}";
                        }
                        catch (Exception)
                        { User.Email = "                NULL"; }

                        sw.WriteLine(User.ToString());

                    }
                    sw.Close();
                }
            }
        }


        /// <summary>
        /// Creates a report of all Alarmer between given timestamps
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Slutt"></param>
        public void QReportAlarmer(DateTime Start, DateTime Slutt, string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15}");
                    while (rdr.Read())
                    {


                        sw.WriteLine();

                    }
                    sw.Close();
                }
            }
        }


        /// <summary>
        /// Creates a report of forst and last Adgang for one particular Kortleser
        /// </summary>
        /// <param name="KortleserID"></param>
        public void QReportBrukstid(string KortleserID, string path, Database db)
        {
            if (!_connected) Connect();
            cmd.CommandText = "";
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{rdr.GetName(0),15}");
                    while (rdr.Read())
                    {


                        sw.WriteLine();

                    }
                    sw.Close();
                }
            }
        }




        public string[] QueryKortlesere()
        {
            if (!_connected) Connect();

            cmd.CommandText = "SELECT KortleserID FROM Kortleser;";
            string data = null;
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    data = data + $"{rdr.GetInt16(0),0},";
                    
                }
            }
            return data.Split(',');
        }





        // Other queries
        //**********************************************************************************************************

        /// <summary>
        /// Inserts alarm event in DB
        /// </summary>
        /// <param name="alarmType"></param>
        /// <param name="kortleserID"></param>
        public void QueryAlarm(string alarmType, string kortleserID)
        {
            if (!_connected) Connect();
            cmd.CommandText = $"INSERT INTO Alarmer (KortleserID, DatoTid, Alarm) VALUES ({kortleserID}, '{DateTime.Now}', '{alarmType}')";
            cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Inserts Adgang Event in DB
        /// </summary>
        /// <param name="kortID"></param>
        /// <param name="kortleserID"></param>
        /// <param name="godkjent"></param>
        public void QueryAdgang(string kortID, string kortleserID, bool godkjent)
        {
            if (!_connected) Connect();
            cmd.CommandText = $"INSERT INTO Adganger (KortID, KortleserID, DatoTid, Godkjent) VALUES ({kortID}, {kortleserID}, '{DateTime.Now}', {godkjent})";
            cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Inserts new Kortleser into DB
        /// </summary>
        /// <param name="kortleserID"></param>
        /// <param name="romFra"></param>
        /// <param name="romTil"></param>
        public void QueryKortleser(string kortleserID, string romFra, string romTil)
        {
            if (!_connected) Connect();
            try
            {
                cmd.CommandText = $"INSERT INTO Kortleser (KortleserID, FraRom, TilRom) VALUES ({kortleserID}, {romFra}, {romTil})";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            
        }



        /// <summary>
        /// custom query for getting the Gyldig_Fra and Gyldig_Til values using KortID
        /// </summary>
        /// <param name="kortID">card id to use</param>
        /// <returns>Ienumerable of strings in the format [Gyldig_Fra, Gyldig_Til] </string></returns>
        public IEnumerable<string> GetValidTimeOfCard(string kortID)
        {
            return Query("SELECT Gyldig_Fra, Gyldig_Til FROM Bruker WHERE KortID = " + kortID).Split(',');
        }



    }
}
