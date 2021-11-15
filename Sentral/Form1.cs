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
        static string[] mottattTekst; 
        static string tekstSomSkalSendes;
        static bool setStart = false, 
            setSlutt = false, 
            TilkobletDataBase = false;

        Socket lytteSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);



        public Database DB;


        public Form1()
        {
            InitializeComponent();
            cb_Rapporter.SelectedIndex = 0;

            //initialisere WebSocket
            ThreadPool.QueueUserWorkItem(FinnNyeKortlesere);
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
        private void FinnNyeKortlesere(object o)
        {
            try
            {
                while (true)
                {
                    Socket kommSokkel = lytteSokkel.Accept();
                    ThreadPool.QueueUserWorkItem(TraadSomMottarFraKortleser, kommSokkel);           // Start ny tråd for ny kortleser
                    MessageBox.Show("Koblet til en kortleser");
                }
            }
            catch (Exception){  }
        }



        /// <summary>
        /// håndtere klienttråden
        /// </summary>
        /// <param name="o">klentobjektet</param>
        private void TraadSomMottarFraKortleser(object o)
        {
            Socket kommSokkel = o as Socket;
            bool ferdig = false;
            IPEndPoint klientEP = (IPEndPoint)kommSokkel.RemoteEndPoint;

            while (!ferdig)
            {
                mottattTekst = MottaDataFraKortleser(kommSokkel, out ferdig).Split(',');
                switch (mottattTekst[0])
                {
                    case "Adgang": // Ny adgangs forespørsel
                        if (!DB.QExistKortID(mottattTekst[1]))          // Lag ny bruker som er gått ut må dato hvis kortet ikke finnes i DB
                        {
                            DB.NonQuery($"INSERT INTO Bruker (KortID, PIN, Gyldig_Til) VALUES ({mottattTekst[1]}, 0000, '01.01.2000 00:00:00');");
                        }
                        if (!ferdig)
                        {   
                            if (AutoriserAdgang(mottattTekst[1], mottattTekst[2]))
                            {
                                MessageBox.Show("Adgang godkjent");
                                tekstSomSkalSendes = "godkjent";
                                DB.QueryAdgang(mottattTekst[1], mottattTekst[3], true);
                            }
                            else
                            {
                                MessageBox.Show("Adgang IKKE godkjent");
                                tekstSomSkalSendes = "underkjent";
                                DB.QueryAdgang(mottattTekst[1], mottattTekst[3], false);
                            }
                            SendDataTilbakeTilKortleser(kommSokkel, tekstSomSkalSendes, out ferdig);
                        }

                        break;
                    case "Alarm":   // Alarm som skal registreres i DB
                        DB.QueryAlarm(mottattTekst[2], mottattTekst[1]);
                        break;
                    case "Hei":     // Ny kortleser som skal registreres i DB
                        DB.QueryKortleser(mottattTekst[1], mottattTekst[2], mottattTekst[3]);
                        break;
                    default:
                        break;
                }
            }
            MessageBox.Show("Forbindelsen med " +  Convert.ToString(klientEP.Address) + " er brutt");
            kommSokkel.Close();
        }



        /// <summary>
        /// sender data til klient
        /// </summary>
        /// <param name="s">socket til å brukest til å sende</param>
        /// <param name="dataSomSendes">data som sendest til socket</param>
        /// <param name="ferdig">boolean ut om den er ferdig å sende</param>
        static void SendDataTilbakeTilKortleser(Socket s, string dataSomSendes, out bool ferdig)
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
        /// Håndterer mottatt data fra en kortleser
        /// </summary>
        /// <param name="s">socket objekt</param>
        /// <param name="ferdig">ut verdi for å sjekke om den er ferdig</param>
        /// <returns>streng av motatt data</returns>
        static string MottaDataFraKortleser(Socket s, out bool ferdig)
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
        public bool AutoriserAdgang(string kortID, string typedPinKode)
        {
            Bruker bruker = new Bruker(kortID, DB);
            if (DB.QExistKortID(bruker.KortID))
            {
                return bruker.Authorise(typedPinKode);
            }
            else return false;
        }










        // GUI Delen
        // ***********************************************************************************************************************

        /// <summary>
        /// code to execute when text has been changed in KortID text field
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// code to execute when text has been changed in Pin text field
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// code to execute when DatoStart button has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// code to execute when DatoSlutt button has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// code to execute when Date is selected from Calendar sub-form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// code to execute when exiting Calendar sub-form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_Leave(object sender, EventArgs e)
        {
            Calendar.Visible = false;
        }





        /// <summary>
        /// code to execute when LeggInn button has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
                    if (DB.QExistKortID(txt_KortID.Text))
                    {
                        MessageBox.Show("Dette kortet er allerede lagt inn");
                    }
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
        /// code to execute when Fjern button has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>

        private void btn_Fjern_Click(object sender, EventArgs e)
        {
            if ((txt_KortID.Text.Length == 4))
            {
                if (DB.QExistKortID(txt_KortID.Text))
                {
                    DB.NonQuery($"DELETE FROM Bruker WHERE kortID = {txt_KortID.Text}");
                    MessageBox.Show("Bruker fjernet fra database");
                }
            }
        }





        /// <summary>
        /// code to execute when Nullstill button has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>

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








        // Rapport delen
        // ***********************************************************************************************************************

        private void btn_Rapport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog ofd = new SaveFileDialog();
                ofd.Filter = "Text Files | *.txt";
                ofd.ShowDialog();
                string Path = ofd.FileName;
                switch (cb_Rapporter.SelectedItem.ToString())
                {
                    case "Brukere":
                        DB.QReportBrukere(Path, DB);
                        break;
                    case "Adgangslogg":
                        DB.QReportAdganger(Convert.ToDateTime(txt_DatoStart.Text), Convert.ToDateTime(txt_DatoSlutt.Text), Path, DB);
                        break;
                    case "Ikke-godkjente adganger på en kortleser":
                        DB.QReportIkkeGodkjent(cb_Kortlesere.SelectedItem.ToString(), Path, DB);
                        break;
                    case "Over 10 ikke-godkjente forsøk på en time":
                        DB.QReportBrukereMedTiFeiledeForsok(Convert.ToDateTime(txt_DatoStart.Text), Convert.ToDateTime(txt_DatoSlutt.Text), Path, DB);
                        break;
                    case "Alarmer":
                        DB.QReportAlarmer(Convert.ToDateTime(txt_DatoStart.Text), Convert.ToDateTime(txt_DatoSlutt.Text), Path, DB);
                        break;
                    case "Første og siste adgang for en kortleser":
                        DB.QReportBrukstid(cb_Kortlesere.SelectedItem.ToString(), Path, DB);
                        break;

                    default:
                        break;
                }
            }
            catch(Exception)
            {

            }
        }

        private void cb_Rapporter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_Rapporter.SelectedItem.ToString())
            {
                case "Brukere":
                    KortlereseVisible(false);
                    l_RapportInfo.Visible = false;

                    break;
                case "Adgangslogg":
                    KortlereseVisible(false);
                    l_RapportInfo.Visible = true;
                    break;
                case "Ikke-godkjente adganger på en kortleser":
                    KortlereseVisible(true);
                    l_RapportInfo.Visible = false;
                    break;
                case "Over 10 ikke-godkjente forsøk på en time":
                    KortlereseVisible(false);
                    l_RapportInfo.Visible = true;
                    break;
                case "Alarmer":
                    KortlereseVisible(false);
                    l_RapportInfo.Visible = true;
                    break;
                case "Første og siste adgang for en kortleser":
                    KortlereseVisible(true);
                    l_RapportInfo.Visible = false;
                    break;

                default:
                    KortlereseVisible(false);
                    l_RapportInfo.Visible = false;
                    break;
            }
        }

        public string Create(string query)
        {
            return "";
        }


        /// <summary>
        /// Opens a Form where you can add Kortleser to the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBtnLeggTilKortleser_Click(object sender, EventArgs e)
        {
            Form AddKortleser = new FormAddKortleser(DB);
            AddKortleser.ShowDialog();
        }




        /// <summary>
        /// Updates items in cb_Kortlesere and makes it visible or hides it
        /// </summary>
        /// <param name="Vis"></param>
        public void  KortlereseVisible(bool Vis)
        {
            if (Vis)
            {
                cb_Kortlesere.Items.Clear();
                foreach (var Kortleser in DB.QueryKortlesere())
                {
                    cb_Kortlesere.Items.Add(Kortleser);
                }
                l_Kortleser.Visible = true;
                cb_Kortlesere.Visible = true;
            }
            else
            {
                l_Kortleser.Visible = false;
                cb_Kortlesere.Visible = false;
            }
        }









        // Avslutt
        // ***********************************************************************************************************************
        /// <summary>
        /// code to execute when Main form is closing
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
