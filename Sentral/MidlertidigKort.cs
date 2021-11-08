using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentral
{
    class MidlertidigKort : Kort
    {
        public DateTime
            _gyldigFra,
            _gyldigTil;

        public MidlertidigKort(string kortID, Database database) : base(kortID, database)
        {
            string[] msg = _database.GetValidTimeOfCard(KortID).ToArray();
            _gyldigFra = DateTime.Parse(msg[0]);
            _gyldigTil = DateTime.Parse(msg[1]);
        }

        public MidlertidigKort(string kortID, DateTime gyldigFra, DateTime gyldigTil, Database database) : base(kortID, database)
        {
            _gyldigFra = gyldigFra;
            _gyldigTil = gyldigTil;
        }

        public MidlertidigKort(string kortID, string gyldigFra, string gyldigTil, Database database) : base(kortID, database)
        {
            _gyldigFra = DateTime.Parse(gyldigFra);
            _gyldigTil = DateTime.Parse(gyldigTil);
        }

        public override bool IsStillValid()
        {
            DateTime now = DateTime.Now;
            return _gyldigTil >= now || _gyldigFra <= now;
        }
    }
}
