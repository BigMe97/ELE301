﻿using System;
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
        static bool setStart = false, setSlutt = false;

        Socket lytteSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

        public Form1()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(new_Klient, 1);
            lytteSokkel.Bind(serverEP);
            lytteSokkel.Listen(10);
        }



        private void new_Klient(object o)
        {
                Console.WriteLine("Venter på en klient ...");
                Socket kommSokkel = lytteSokkel.Accept(); // blokkerende metode

                ThreadPool.QueueUserWorkItem(Klienttraad, kommSokkel);
        }

        private void Klienttraad(object o)
        {
            Socket kommSokkel = o as Socket;
            bool ferdig = false;
            IPEndPoint klientEP = (IPEndPoint)kommSokkel.RemoteEndPoint;

            string hilsen = "Velkommen til en enkel testserver";
            SendData(kommSokkel, hilsen, out ferdig);
            while (!ferdig)
            {
                mottattTekst = MottaData(kommSokkel, out ferdig);
                if (!ferdig)
                {
                    MessageBox.Show(mottattTekst);
                    tekstSomSkalSendes = Svar(mottattTekst);
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


        public string Svar(string melding)
        {
            string svar = "godkjent";


            return svar;
        }


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





        // GUI Delen
        // ****************************************************************************

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

        private void btn_LeggInn_Click(object sender, EventArgs e)
        {

        }

        private void Calendar_Leave(object sender, EventArgs e)
        {
            Calendar.Visible = false;
        }
    }
}
