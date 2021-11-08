using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentral
{
    class Kort
    {

        private string _kortID;
        public string KortID { get => _kortID; }
        protected Database _database;

        public Kort
            (string kortID, Database database)
        {
            _database = database;
            _kortID = kortID;
        }

        public bool Authorise(string pin)
        {
            string data = _database.Query("SELECT PIN FROM Bruker WHERE KortID = " + _kortID);
            if (data == null)
            {
                return false;
            }
            else
            {
                return pin == data;
            }
        }

        public virtual bool IsStillValid()
        {
            return true;
        }
    }
}
