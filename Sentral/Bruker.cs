using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentral
{
    class Bruker
    {
        private Kort _kort;
        public Kort Kort { get => _kort; }

        private string
            _email,
            _firstname,
            _surname;

        public string Email { get => _email; }
        public string FirstName { get => _firstname; }
        public string SurName { get => _surname; }



        protected Database _database;

        public Bruker(string kortID, Database database)
        {
            _database = database;

            string[] msg =
                _database
                .Query("SELECT Gyldig_Fra, Gyldig_Til, PIN, Fornavn, Etternavn, EPost FROM Bruker WHERE KortID = " + kortID)
                .Split(',');

            if (msg[0] != "" && msg[1] != "")
            {
                _kort = new MidlertidigKort(msg[2], msg[0], msg[1], _database);
            }
            else
            {
                _kort = new Kort(msg[2], _database);
            }

            _firstname = msg[3];
            _surname = msg[4];
            _email = msg[5];
        }
        
        public Bruker(string kortID, string epost, string fornavn, string etternavn, Database database)
        {
            _database = database;

            string[] msg =
                _database
                .Query("SELECT Gyldig_Fra, Gyldig_Til, PIN FROM Bruker WHERE KortID = " + kortID)
                .Split(',');

            if (msg[0] != "" && msg[1] != "")
            {
                _kort = new MidlertidigKort(msg[2], msg[0], msg[1], _database);
            }
            else
            {
                _kort = new Kort(msg[2], _database);
            }

            _firstname = fornavn;
            _surname = etternavn;
            _email = epost;
        }

        public Bruker(Kort kort, string epost, string fornavn, string etternavn, Database database)
        {
            _database = database;
            _kort = kort;
            _firstname = fornavn;
            _surname = etternavn;
            _email = epost;
        }
    }
}
