using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sentral
{
    public partial class Form1 : Form
    {
        static byte[] data = new byte[1024];
        static string mottattTekst; 
        static string tekstSomSkalSendes;
        static bool setStart = false, setSlutt = false, TilkobletDataBase = false;

        Socket lytteSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);



        Database DB;


        public Form1()
        {
            InitializeComponent();


            //initialisere WebSocket
            ThreadPool.QueueUserWorkItem(new_Klient);
            lytteSokkel.Bind(serverEP);
            lytteSokkel.Listen(10);


            string
                host = "158.37.32.244",
                userName = "h583404",
                password = "pass",
                database = "h583404";
            DB = new Database(host, userName, password, database);
        }





       
        // Serverfunksjoner
        // ***********************************************************************************************************************


        
        /// <summary>
        /// mottar ny klient request
        /// </summary>
        /// <param name="o">klientobjekt</param>
        private void new_Klient(object o)
        {
            try
            {
                Socket kommSokkel = lytteSokkel.Accept(); // blokkerende metode
                ThreadPool.QueueUserWorkItem(Klienttraad, kommSokkel);
            }
            catch (Exception)
            {

            }

        }



        /// <summary>
        /// håndtere klienttråden
        /// </summary>
        /// <param name="o">klentobjektet</param>
        private void Klienttraad(object o)
        {
            Socket kommSokkel = o as Socket;
            bool ferdig = false;
            IPEndPoint klientEP = (IPEndPoint)kommSokkel.RemoteEndPoint;

            while (!ferdig)
            {
                mottattTekst = MottaData(kommSokkel, out ferdig);
                if (!ferdig)
                {
                    MessageBox.Show(mottattTekst);
                                                                                            // Verifiser melding
                    tekstSomSkalSendes = HandleCardAuthorisation(mottattTekst);

                    SendData(kommSokkel, tekstSomSkalSendes, out ferdig);
                }
            }
            MessageBox.Show("Forbindelsen med {0} er brutt", Convert.ToString(klientEP.Address));
            kommSokkel.Close();
        }



        /// <summary>
        /// sender data til klient
        /// </summary>
        /// <param name="s">socket til å brukest til å sende</param>
        /// <param name="dataSomSendes">data som sendest til socket</param>
        /// <param name="ferdig">boolean ut om den er ferdig å sende</param>
        static void SendData(Socket s, string dataSomSendes, out bool ferdig)
        {
            try
            {
                s.Send(Encoding.ASCII.GetBytes(dataSomSendes));
                ferdig = false;
            }
            catch (Exception unntak)
            {
                Console.WriteLine("Feil" + unntak.Message);
                ferdig = true;
            }
        }



        /// <summary>
        /// nåndterer mottatt data
        /// </summary>
        /// <param name="s">socket objekt</param>
        /// <param name="ferdig">ut verdi for å sjekke om den er ferdig</param>
        /// <returns>streng av motatt data</returns>
        static string MottaData(Socket s, out bool ferdig)
        {
            byte[] data = new byte[1024];
            string svar = " ";

            try
            {
                int antallMottatt = s.Receive(data);
                svar = Encoding.ASCII.GetString(data, 0, antallMottatt);
                ferdig = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Feil" + e.Message);
                ferdig = true;
            }
            return svar;
        }



        /// <summary>
        /// authorizes if a card is valid and pin typed is correct
        /// </summary>
        /// <param name="kortID">card id to use to verify</param>
        /// <param name="typedPinKode">typed pin code to check if is correct</param>
        /// <returns>true if authorisation was valid</returns>
        public bool AuthorizeCard(string kortID, string typedPinKode)
        {
            Bruker bruker = new Bruker(kortID, DB);
            return bruker.Kort.Authorise(typedPinKode) && bruker.Kort.IsStillValid();
        }



        /// <summary>
        /// Handles authorization of Card login
        /// </summary>
        /// <param name="melding">message from "card reader with keypad"</param>
        /// <returns>godkjent or underkjent depending on wheather the card was authorizsed</returns>
        public string HandleCardAuthorisation(string melding)
        {
            string kortID = melding.Substring(melding.IndexOf('K')+1,4); //matten på denne meldingen har eg ikkje sjekka, vensligst se over om dette stemmer
            string pin = melding.Substring(melding.IndexOf('P')+1, 4); // har kje sjekka matten her heller, bruke bare det som er skrevet tidligere

            return AuthorizeCard(kortID, pin) ? "Godkjent" : "underkjent";
        }








        // GUI Delen
        // ***********************************************************************************************************************


        //har noe funksjon som kan dekke mye av det som er skrevet her men har ikke implementert det enda


        /// <summary>
        /// Form handle text change
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void txt_KortID_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_KortID.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_KortID.Text = txt.Substring(0, 4);
                    txt_KortID.SelectionStart = 4;
                }
            }
            catch (Exception i)
            {
                txt_KortID.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }
        }



        /// <summary>
        /// Form handle text change
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void txt_Pin_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_Pin.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_Pin.Text = txt.Substring(0, 4);
                    txt_Pin.SelectionStart = 4;
                }
            }
            catch (Exception i)
            {
                txt_KortID.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }
        }



        /// <summary>
        /// Form handle button click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void txt_DatoStart_Click(object sender, EventArgs e)
        {
            setStart = true;
            Calendar.Visible = true;
            Calendar.TabStop = true;
            if (txt_DatoStart.Text.Length > 4)
            {
                try
                {
                    Calendar.SelectionStart = Convert.ToDateTime(txt_DatoStart.Text);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Calendar.SelectionStart = DateTime.Today;
            }

        }



        /// <summary>
        /// Form handle button click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void txt_DatoSlutt_Click(object sender, EventArgs e)
        {
            setSlutt = true;
            Calendar.Visible = true;
            Calendar.TabStop = true;
            if (txt_DatoSlutt.Text.Length > 4)
            {
                try
                {
                    Calendar.SelectionEnd = Convert.ToDateTime(txt_DatoSlutt.Text);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Calendar.SelectionEnd = DateTime.Today;
            }
        }



        /// <summary>
        /// Form handle popup window
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (setStart)
            {
                txt_DatoStart.Text = Convert.ToString(Calendar.SelectionStart);
                setStart = false;
            }
            else if (setSlutt)
            {
                txt_DatoSlutt.Text = Convert.ToString(Calendar.SelectionStart);
                setSlutt = false;
            }
            Calendar.Visible = false;
        }



        /// <summary>
        /// Form handle popup window
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void Calendar_Leave(object sender, EventArgs e)
        {
            Calendar.Visible = false;
        }



        /// <summary>
        /// Form handle button click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void btn_LeggInn_Click(object sender, EventArgs e)
        {
            if ((txt_KortID.Text.Length == 4) && (txt_Pin.Text.Length == 4))
            {
                try
                {
                    DB.NonQuery($"INSERT INTO bruker (KortID, PIN) VALUES({txt_KortID.Text},{txt_Pin.Text});");
                }
                catch (Exception)
                {
                    MessageBox.Show("Dette kortet er allerede lagt inn");
                }
                
                if (txt_Fornavn.Text != "")
                {
                    DB.NonQuery($"UPDATE Bruker SET Fornavn = '{txt_Fornavn.Text}' WHERE KortID = {txt_KortID.Text};");
                }
                if (txt_Etternavn.Text != "")
                {
                    DB.NonQuery($"UPDATE Bruker SET Etternavn = '{txt_Etternavn.Text}' WHERE KortID = {txt_KortID.Text};");
                }
                if (txt_Epost.Text != "")
                {
                    DB.NonQuery($"UPDATE Bruker SET EPost = '{txt_Epost.Text}' WHERE KortID = {txt_KortID.Text};");
                }
                if (txt_DatoStart.Text != "")
                {
                    DB.NonQuery($"UPDATE Bruker SET Gyldig_Fra = '{txt_DatoStart.Text}' WHERE KortID = {txt_KortID.Text};");
                }
                if (txt_DatoSlutt.Text != "")
                {
                    DB.NonQuery($"UPDATE Bruker SET Gyldig_Til = '{txt_DatoSlutt.Text}' WHERE KortID = {txt_KortID.Text};");
                }
            }
            else
            {
                MessageBox.Show("Kort-ID og PIN-kode må være inntastet og ha lengde 4");
            }
        }



        /// <summary>
        /// Form handle button click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void btn_Fjern_Click(object sender, EventArgs e)
        {
            if ((txt_KortID.Text.Length == 4) && (txt_Pin.Text.Length == 4))
            {
                string data = DB.Query("SELECT KortID FROM Bruker WHERE KortID = " + txt_KortID.Text);
                if (data == txt_KortID.Text)
                {
                    DB.NonQuery($"DELETE FROM Bruker WHERE kortID = {txt_KortID.Text}");
                    MessageBox.Show("Bruker fjernet fra database");

                }
            }
        }



        /// <summary>
        /// Form handle button click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void btn_Nullstill_Click(object sender, EventArgs e)
        {
            Calendar.SelectionStart = DateTime.Today;
            Calendar.SelectionEnd = DateTime.Today;
            txt_Fornavn.Text = "";
            txt_Etternavn.Text = "";
            txt_Epost.Text = "";
            txt_DatoStart.Text = "";
            txt_DatoSlutt.Text = "";
            txt_KortID.Text = "";
            txt_Pin.Text = "";
        }




        // Rapport GUI delen
        // ***********************************************************************************************************************


        public string Create(string query)
        {
            return "";
        }












        // Avslutt
        // ***********************************************************************************************************************
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Er du sikker på at du vil stenge sentralen?", "Steng Sentralen ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            }
            else
            {
                lytteSokkel.Close();

            }
        }
    }
}
