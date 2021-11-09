using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentral
{
    class Bruker
    {
        private string _kortID;

        private string
            _email,
            _firstname,
            _surname,
            _pin;

        public string KortID { get => _kortID; }

        public string Email { get => _email; }
        public string FirstName { get => _firstname; }
        public string SurName { get => _surname; }


        protected Database _database;

        public Bruker(string kortID, Database database)
        {
            _database = database;

            string[] msg = _database.QueryBruker(kortID);
        }

        public Bruker(string kortID, string epost, string fornavn, string etternavn, Database database)
        {
            _database = database;

            string[] msg = _database.Query(kortID).Split(',');


            _firstname = fornavn;
            _surname = etternavn;
            _email = epost;
        }

        public Bruker(string kort, string pin, string epost, string fornavn, string etternavn, Database database)
        {
            _database = database;
            _kortID = kort;
            _firstname = fornavn;
            _surname = etternavn;
            _email = epost;
        }


        /// <summary>
        /// Authorises card and pin from DB
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>TRUE if pin is correct</returns>
        public bool Authorise(string pin)
        {
            string[] data = _database.QueryBruker(_kortID);
            if ((Convert.ToDateTime(data[0]) < DateTime.Today) && (Convert.ToDateTime(data[1]) > DateTime.Today))
            {
                return (data[2] == pin);
            }
            else
            {
                return false;
            }
        }
    }
}
