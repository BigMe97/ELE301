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
using Microsoft.VisualBasic;
using System.IO.Ports;

namespace Kortleser
{
    public partial class Kortleser : Form
    {

        public string MeldingFraSimSim = "";
        static string passcode = "12345";
        static string kortlesesernummer;
        static bool OK = false, Cancel = false;
        static string DigitalO, DigitalI, Thermistor, AnalogInn1, AnalogInn2, Temp1, Temp2;
        static bool open = false, alarm = false, locked = true;

        Socket klientSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        bool ferdig = false;
        bool kontaktMedServer = false;
        byte[] data = new byte[1024];
        string input, stringData;



        public Kortleser()
        {
            InitializeComponent();
            VelgKortleserNummer();
            KobleTilServer();

        }



        private void Kortleser_Load(object sender, EventArgs e)
        {
            if (Cancel)
            {
                Application.Exit();
            }
            this.Text = "Kortleser " + kortlesesernummer;
            OppdaterComPorter();
        }



        private void cbCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            OK = false;
            try
            {
                sp.Close();
                sp.PortName = cbCOMPort.SelectedItem.ToString();
                sp.BaudRate = 9600;
                sp.ReadTimeout = 1000;
                sp.Open();
                Thread.Sleep(1000);
                sp.Write("$R1");         // Be om en melding
                Thread.Sleep(1000);
                sp.Write("$E1");        // Be om melding ved endring av data
                Thread.Sleep(1000);
                sp.Write("$S002");      // Oppdater hvert 2. sekund
            }
            catch (Exception)
            {
                MessageBox.Show("Prøv en annen serieport");
            }
        }




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

                if (txt.Length == 4)
                {
                    tD4.Enabled = true;
                    sp.WriteLine("$O41");

                }
            }
            catch (Exception i)
            {
                txt_KortID.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }
        }

        // Nummer knappene
        private void btn_num_1_Click(object sender, EventArgs e)
        {
            TM_kode('1');
        }

        private void btn_num_2_Click(object sender, EventArgs e)
        {
            TM_kode('2');
        }

        private void btn_num_3_Click(object sender, EventArgs e)
        {
            TM_kode('3');
        }

        private void btn_num_4_Click(object sender, EventArgs e)
        {
            TM_kode('4');
        }

        private void btn_num_5_Click(object sender, EventArgs e)
        {
            TM_kode('5');
        }

        private void btn_num_6_Click(object sender, EventArgs e)
        {
            TM_kode('6');
        }

        private void btn_num_7_Click(object sender, EventArgs e)
        {
            TM_kode('7');
        }

        private void btn_num_8_Click(object sender, EventArgs e)
        {
            TM_kode('8');
        }

        private void btn_num_9_Click(object sender, EventArgs e)
        {
            TM_kode('9');
        }

        private void btn_num_0_Click(object sender, EventArgs e)
        {
            TM_kode('0');
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            TM_kode('C');
        }

        // Veit ikke hva/om vi skal bruke den til noe men det så fint ut
        private void btn_num_HASH_Click(object sender, EventArgs e)
        {
            TM_kode('#');
        }


        private void VelgKortleserNummer()
        {
            while (!OK)
            {
                string txt = Interaction.InputBox("Hvilken kortleser er dette?", "Kortleservelger", "0000");
                try
                {
                    // If user cancels
                    if (txt == "")
                    {
                        Cancel = true;
                        kortlesesernummer = "0000";
                        break;
                    }

                    // Keep the string a number
                    Convert.ToInt32(txt);

                    // Keep the string at 4 digits
                    if (txt.Length == 4)
                    {
                        OK = true;
                        kortlesesernummer = txt;
                    }
                    else { MessageBox.Show("1 Kortlesernummer må være et heltall mellom 0000 og 9999"); }

                }
                catch (Exception)
                {
                    MessageBox.Show("2 Kortlesernummer må være et heltall mellom 0000 og 9999");
                    OK = false;
                }
            }
            OK = false;

        }

        private void OppdaterComPorter()
        {
            string[] alleComPortNavn = SerialPort.GetPortNames();
            foreach (string item in alleComPortNavn)
            {
                cbCOMPort.Items.Add(item);
            }
            if (cbCOMPort.Items.Count > 0) cbCOMPort.SelectedIndex = 0;
        }


        public void TM_kode(char input)
        {
            if (tD4.Enabled == true)
            {
                if (input == 'C')
                {
                    passcode = "12345";
                }
                else if (input == '#')
                {
                    // Do something??
                }
                else
                {
                    switch (passcode.Length)
                    {
                        case 5:
                            passcode = input.ToString();
                            break;
                        case 1:
                            passcode = passcode + input;
                            break;

                        case 2:
                            passcode = passcode + input;
                            break;

                        case 3:
                            passcode = passcode + input;
                            Send();
                            sp.WriteLine("$O40");

                            tD4.Enabled = false;
                            txt_KortID.Text = "";
                            passcode = "12345";
                            break;

                        default:
                            MessageBox.Show("Noe feilet");
                            passcode = "12345";
                            break;
                    }
                }
            }
        }






        // Send forespørsel til Sentral
        public void Send()
        {
            // MessageBox.Show("Koden: " + passcode + "\nMelding fra SimSim: \n" + MeldingFraSimSim);

            if (kontaktMedServer)
            {
                input =  "K" + txt_KortID.Text + "P" + passcode;
                if (!ferdig)
                {
                    SendData(klientSokkel, input, out ferdig);
                    stringData = MottaData(klientSokkel, out ferdig);
                    if (stringData == "godkjent")
                    {
                        Unlock();
                    }
                }
            }
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
                MessageBox.Show("Feil" + e.Message);
                ferdig = true;
            }
            return svar;
        }






        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = sp.ReadExisting();
            if ((data.Length == 65) && (data.IndexOf("$") == 2) && (data.IndexOf("#") == 64))
            {
                if (!OK)
                {
                    MessageBox.Show("Oprettet kommunikasjon med SimSim");
                    OK = true;
                }
                MeldingFraSimSim = data;
                MeldingsTolker();

            }
        }


        private void MeldingsTolker()
        {
            // DigitalI, AnalogInn1, AnalogInn2, Temp1, Temp2;
            DigitalI = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('D') + 1, 8);        // D
            DigitalO = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('E') + 1, 8);        // E
            Thermistor = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('F') + 1, 4);      // F
            AnalogInn1 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('G') + 1, 4);      // G
            AnalogInn2 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('H') + 1, 4);      // H
            Temp1 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('I') + 1, 3);           // I
            Temp2 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('j') + 1, 3);           // I


            // Fordi oppgaven mener at SimSim skal kunne leke tastatur...
            if (Convert.ToInt32(DigitalO.Substring(0, 3)) > 0)
            {

                if (DigitalO[0] == '1')
                {
                    sp.Write("$O00");
                    TM_kode('0');
                }
                if (DigitalO[1] == '1')
                {
                    sp.Write("$O10");
                    TM_kode('1');
                }
                if (DigitalO[2] == '1')
                {
                    sp.Write("$O20");
                    TM_kode('2');
                }
                if (DigitalO[3] == '1')
                {
                    sp.Write("$30");
                    TM_kode('3');
                }
            }


            // Vise om døra er Låst
            if (DigitalO[5] == '1')
            {
                if (!locked)
                {
                    pbLocked.Image = global::Kortleser.Properties.Resources.locked;
                    locked = true;
                }
                sp.WriteLine("$O60");
            }
            else if (DigitalO[5] == '0')
            {
                if (locked)
                {
                    pbLocked.Image = global::Kortleser.Properties.Resources.unlocked;
                    locked = false;
                }
            }

            // Vise om døra er åpen
            if (DigitalO[6] == '1' && !locked)
            {
                if (!open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Open;
                    open = true;
                }
            }
            else if (DigitalO[6] == '0')
            {
                if (open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Closed;
                    open = false;
                    sp.WriteLine("$O51");
                }
            }

            if (Convert.ToInt32(AnalogInn1) > 500)
            {
                AlarmPå();
            }
            else
            {
                ResetAlarm();
            }

            // Vise om alarmen er aktivert
            if (DigitalI[7] == '1')
            {
                AlarmPå();
            }
            else if (DigitalI[7] == '0')
            {
                ResetAlarm();
            }


        }


        // Alarm!!!
        private void AlarmPå()
        {
            sp.Write("$O71");
            pbAlarm.Image = global::Kortleser.Properties.Resources.alarm;
            alarm = true;
        }

        // Sjekk om alarmen kan resettes
        private void ResetAlarm()
        {
            if ((Convert.ToInt32(AnalogInn1) < 500 && DigitalI[7] == 0))
            {
                pbAlarm.Image = null;
                sp.Write("$O70");
            }
        }

        private void Unlock()
        {
            sp.Write("$O50");
        }



        private void tD4_Tick(object sender, EventArgs e)
        {
            sp.Write("$O40");
            tD4.Enabled = false;
            txt_KortID.Text = "";
        }


        public void KobleTilServer()
        {
            try
            {
                klientSokkel.Connect(serverEP);    // blokkerende metode
                kontaktMedServer = true;
            }
            catch (SocketException e)
            {
                MessageBox.Show("Feil" + e.Message);
                kontaktMedServer = false;
            }

        }

        private void Kortleser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Cancel)
            {
                var result = MessageBox.Show("Er du sikker på at du vil fjerne denne kortleseren?", "Fjerne kortleser " + kortlesesernummer.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    // cancel the closure of the form.
                    e.Cancel = true;
                }
                else if(kontaktMedServer)
                {
                    klientSokkel.Shutdown(SocketShutdown.Both);
                    klientSokkel.Close();
                }
            }
        }




        
    }
}
