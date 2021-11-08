using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace Sentral
{
    class Database
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
        public Database
            (
            string host,
            string userName,
            string password,
            string database
            )
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
                cmd.Connection = con; //hei magnus eg fant ut kor cmd blei bundet opp mot con :p
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
