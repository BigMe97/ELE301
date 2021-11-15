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

        public string KortID { get => _kortID; set {_kortID = value; } }
        public string Email { get => _email; set { _email = value; } }
        public string FirstName { get => _firstname; set { _firstname = value; } }
        public string SurName { get => _surname; set { _surname = value; } }


        protected Database _database;


        public Bruker(string kortID, Database database)
        {
            _database = database;
            _kortID = kortID;
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



        public override string ToString()
        {
            return $"{_firstname},{_surname},{_email},{_kortID}";
        }



        /// <summary>
        /// Authorises card and pin from DB
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>TRUE if pin is correct</returns>
        public bool Authorise(string pin)
        {
            string[] data = _database.QueryBruker(_kortID);
            if ((Convert.ToDateTime(data[0]) < DateTime.Now) && (Convert.ToDateTime(data[1]) > DateTime.Now))
            {
                return (Convert.ToInt16(data[2]) == Convert.ToInt16(pin));
            }
            else
            {
                return false;
            }
        }




    }
}
