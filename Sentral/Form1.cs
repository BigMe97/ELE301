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
using Npgsql;

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

        static string cs = "Host=158.37.32.244;Username=h583404;Password=pass;Database=h583404";        // Kobler til sql serveren til skolen
        NpgsqlConnection con = new NpgsqlConnection(cs);
        

        //Opprette "spørreobjekt"
        NpgsqlCommand cmd = new NpgsqlCommand();
        NpgsqlDataReader rdr;


        public Form1()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(new_Klient, 1);
            ThreadPool.QueueUserWorkItem(KobleTilSQL);
            lytteSokkel.Bind(serverEP);
            lytteSokkel.Listen(10);
            
            
        }

        // Initialiser SQL
        // ***********************************************************************************************************************
        private void KobleTilSQL(object o)
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                // btn_LeggInn.Enabled = true;                          // Thread problemer
                // btn_Fjern.Enabled = true;
                TilkobletDataBase = true;
                MessageBox.Show("Koblet til databasen\n");
            }
            catch (Exception e)
            {
                MessageBox.Show("Kunne ikke koble til databasen\n" + e.Message);
            }
        }





   
        // Serverfunksjoner
        // ***********************************************************************************************************************
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
                    tekstSomSkalSendes = Verifiser(mottattTekst);

                    SendData(kommSokkel, tekstSomSkalSendes, out ferdig);
                }
            }
            MessageBox.Show("Forbindelsen med {0} er brutt", Convert.ToString(klientEP.Address));
            kommSokkel.Close();
        }







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








        public string Verifiser(string melding)
        {
            string svar, data = null;
            DateTime GyldigFra, GyldigTil;
            int PIN, KortID, VPIN;
            KortID = Convert.ToInt16(melding.Substring(melding.IndexOf('K')+1,4));
            PIN = Convert.ToInt16(melding.Substring(melding.IndexOf('P')+1, 4));
            cmd.CommandText = "SELECT Gyldig_Fra, Gyldig_Til, PIN FROM Bruker WHERE KortID = " + KortID;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                data = $"{rdr.GetDateTime(0),0} {rdr.GetDateTime(1),0} {rdr.GetInt16(2),0}";
                
            }
            rdr.Close();
            if (data == null)
            {
                svar = "underkjent";
            }
            else
            {
                MessageBox.Show(data.Substring(40));
                VPIN = Convert.ToInt16(data.Substring(40));
                GyldigFra = Convert.ToDateTime(data.Substring(0, 10));
                GyldigTil = Convert.ToDateTime(data.Substring(11, 10));


                if ((PIN == VPIN) && (DateTime.Today > GyldigFra) && (DateTime.Today < GyldigTil))
                {
                    svar = "godkjent";
                }
                else
                {
                    svar = "underkjent";
                }
            }
            return svar;
        }








        





        // GUI Delen
        // ***********************************************************************************************************************

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




        private void Calendar_Leave(object sender, EventArgs e)
        {
            Calendar.Visible = false;
        }





        private void btn_LeggInn_Click(object sender, EventArgs e)
        {
            if ((txt_KortID.Text.Length == 4) && (txt_Pin.Text.Length == 4))
            {
                try
                {
                    cmd.CommandText = string.Format("INSERT INTO bruker (KortID, PIN) VALUES({0},{1});", txt_KortID.Text, txt_Pin.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dette kortet er allerede lagt inn");
                }
                
                if (txt_Fornavn.Text != "")
                {
                    cmd.CommandText = string.Format("UPDATE Bruker SET Fornavn = '{0}' WHERE KortID = {1};", txt_Fornavn.Text, txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txt_Etternavn.Text != "")
                {
                    cmd.CommandText = string.Format("UPDATE Bruker SET Etternavn = '{0}' WHERE KortID = {1};", txt_Etternavn.Text, txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txt_Epost.Text != "")
                {
                    cmd.CommandText = string.Format("UPDATE Bruker SET EPost = '{0}' WHERE KortID = {1};", txt_Epost.Text, txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txt_DatoStart.Text != "")
                {
                    cmd.CommandText = string.Format("UPDATE Bruker SET Gyldig_Fra = '{0}' WHERE KortID = {1};", txt_DatoStart.Text, txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txt_DatoSlutt.Text != "")
                {
                    cmd.CommandText = string.Format("UPDATE Bruker SET Gyldig_Til = '{0}' WHERE KortID = {1};", txt_DatoSlutt.Text, txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                }

            }
            else
            {
                MessageBox.Show("Kort-ID og PIN-kode må være inntastet og ha lengde 4");

            }
        }
        







        private void btn_Fjern_Click(object sender, EventArgs e)
        {
            if ((txt_KortID.Text.Length == 4) && (txt_Pin.Text.Length == 4))
            {
                string data = null;
                cmd.CommandText = "SELECT KortID FROM Bruker WHERE KortID = " + txt_KortID.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data = $"{rdr.GetInt16(0),0}";

                }
                rdr.Close();
                if (data == txt_KortID.Text)
                {
                    cmd.CommandText = string.Format("DELETE FROM Bruker WHERE kortID = {0}", txt_KortID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bruker fjernet fra database");

                }
            }
        }







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
