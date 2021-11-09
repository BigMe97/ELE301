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
        static string PIN = "12345";
        static string kortlesernummer;
        static bool OK = false;
        static bool Cancel = false;
        static string DigitalO;
        static string DigitalI;
        static string Thermistor;
        static string AnalogInn1;
        static string AnalogInn2;
        static string Temp1;
        static string Temp2;
        static bool open = false;
        static bool alarm = false;
        static bool locked = true;
        string sisteAdgang = "Ingen forsøk";

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
        }

        /// <summary>
        /// Åpner et vindu man kan velge kortleserID
        /// </summary>
        private void VelgKortleserNummer()
        {
            while (!OK)
            {
                string Kortlesernummer = Interaction.InputBox("Hvilken kortleser er dette?", "Kortleservelger", "0000");
                try
                {
                    if (Kortlesernummer == "") //If user cancels
                    {
                        Cancel = true;
                        kortlesernummer = "0000";
                        break;
                    }

                    Convert.ToInt32(Kortlesernummer); //Keep the string a number

                    if (Kortlesernummer.Length == 4) //Keep the string at 4 digits
                    {
                        OK = true;
                        kortlesernummer = Kortlesernummer;
                    }
                    else
                    {
                        MessageBox.Show("Kortlesernummer må være et heltall mellom 0000 og 9999");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Kortlesernummer må være et heltall mellom 0000 og 9999");
                    OK = false;
                }
            }
            OK = false;
        }


        private void Kortleser_Load(object sender, EventArgs e)
        {
            if (Cancel)
            {
                Application.Exit();
            }
            this.Text = "Kortleser " + kortlesernummer;
            OppdaterComPorter();
        }

        /// <summary>
        /// Oppdaterer innholdet i ComboBox'en med COMporter
        /// </summary>
        private void OppdaterComPorter()
        {
            string[] alleComPortNavn = SerialPort.GetPortNames();
            foreach (string item in alleComPortNavn)
            {
                cbCOMPort.Items.Add(item);
            }
            if (cbCOMPort.Items.Count > 0) cbCOMPort.SelectedIndex = 0;
        }

        /// <summary>
        /// Endrer inntastet PINkode basert på input fra SimSim og tastaturet i Kortleser
        /// </summary>
        /// <param name="input"></param>
        public void PIN_innlesning(char input)
        {
            if (tD4.Enabled == true)
            {
                if (input == 'C')
                {
                    PIN = "     ";
                }
                else if (input == '#')
                {
                    //Gjør ingenting
                }
                else
                {
                    switch (PIN.Length)
                    {
                        case 5:
                            PIN = input.ToString();
                            break;

                        case 1:
                            PIN = PIN + input;
                            break;

                        case 2:
                            PIN = PIN + input;
                            break;

                        case 3:
                            PIN = PIN + input;
                            SendAdgangForesporsel();
                            sp.WriteLine("$O40"); //Skriver utgang 4 lav siden vi har brukt under 45 sek
                            tD4.Enabled = false; //Stopper timeren for 45 sek
                            txt_KortID.Text = "";
                            PIN = "     ";
                            break;

                        default:
                            MessageBox.Show("Noe feilet");
                            PIN = "     ";
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Sender adgangsforespørsel og mottar svar fra Sentralen
        /// </summary>
        public void SendAdgangForesporsel()
        {
            if (kontaktMedServer)
            {
                input = "Forespørsel: " + txt_KortID.Text + ',' + PIN + ',' + kortlesernummer;
                if (!ferdig)
                {
                    SendDataTilSentral(klientSokkel, input, out ferdig); //Sender kortnummer og PINkode til Sentralen
                    stringData = MottaDataFraSentral(klientSokkel, out ferdig); //Svarer om dette ligger i databasen vår
                    sisteAdgang = Convert.ToString(DateTime.Now) + ", " + txt_KortID.Text + ", " + stringData;
                    if (stringData == "godkjent")
                    {
                        Unlock();
                    }
                }
            }
        }

        /// <summary>
        /// Sender en melding til Sentralen
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dataSomSendes"></param>
        /// <param name="ferdig"></param>
        static void SendDataTilSentral(Socket s, string dataSomSendes, out bool ferdig)
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

        /// <summary></summary>
        /// <param name="s"></param>
        /// <param name="ferdig"></param>
        /// <returns>Melding fra Sentral</returns>
        static string MottaDataFraSentral(Socket s, out bool ferdig)
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

        /// <summary>
        /// Ber SimSim låse opp døren
        /// </summary>
        private void Unlock()
        {
            sp.Write("$O50");
        }


        /// <summary>
        /// Setter alarmen på og sender melding om alarm til Sentralen
        /// </summary>
        /// <param name="melding"></param>
        private void AlarmPå(string melding)
        {
            sp.Write("$O71");
            pbAlarm.Image = global::Kortleser.Properties.Resources.alarm;
            string AlarmMelding = $"Alarm,{kortlesernummer},{melding},{sisteAdgang.Split(',')[1]}";
            SendDataTilSentral(klientSokkel, AlarmMelding, out ferdig);
            alarm = true;
        } //Sjekk denne*
        private void ResetAlarm()
        {
            if ((Convert.ToInt32(AnalogInn1) < 500) && (DigitalI[7] == 0))
            {
                sp.Write("$O70");
                pbAlarm.Image = null;
                alarm = false;
            }
        } //Sjekk denne
        private void tD4_Tick(object sender, EventArgs e)
        {
            sp.Write("$O40");
            tD4.Enabled = false;
            txt_KortID.Text = "";
        } //Sjekk denne


        /// <summary>
        /// Kjører når man er på vei til å lukke programmet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kortleser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Cancel)
            {
                var result = MessageBox.Show("Er du sikker på at du vil fjerne denne kortleseren?", "Fjerne kortleser " + kortlesernummer.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    // cancel the closure of the form.
                    e.Cancel = true;
                }
                else if (kontaktMedServer)
                {
                    klientSokkel.Shutdown(SocketShutdown.Both);
                    klientSokkel.Close();
                }
            }
        } //Sjekk denne



        //KNAPPER OG TEKSTBOKS
        
        
        
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

        private void btn_num_1_Click(object sender, EventArgs e)
        {
            PIN_innlesning('1');
        }

        private void btn_num_2_Click(object sender, EventArgs e)
        {
            PIN_innlesning('2');
        }

        private void btn_num_3_Click(object sender, EventArgs e)
        {
            PIN_innlesning('3');
        }

        private void btn_num_4_Click(object sender, EventArgs e)
        {
            PIN_innlesning('4');
        }

        private void btn_num_5_Click(object sender, EventArgs e)
        {
            PIN_innlesning('5');
        }

        private void btn_num_6_Click(object sender, EventArgs e)
        {
            PIN_innlesning('6');
        }

        private void btn_num_7_Click(object sender, EventArgs e)
        {
            PIN_innlesning('7');
        }

        private void btn_num_8_Click(object sender, EventArgs e)
        {
            PIN_innlesning('8');
        }

        private void btn_num_9_Click(object sender, EventArgs e)
        {
            PIN_innlesning('9');
        }

        private void btn_num_0_Click(object sender, EventArgs e)
        {
            PIN_innlesning('0');
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            PIN_innlesning('C');
        }

        private void btn_num_HASH_Click(object sender, EventArgs e) // Veit ikke hva/om vi skal bruke den til noe men det så fint ut
        {
            PIN_innlesning('#');
        }

        private void btnSisteAdgang_Click(object sender, EventArgs e) //brukergrensesnitt som gir muligheten til å se siste adgangsforespørsel og Sentralens svar på den
        {
            txtSisteAdgang.Text = sisteAdgang;
        }

    

        /// <summary>
        /// Kjører når data fra COMporten 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e) //Kjører når seriellporten får en ny melding
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


        /// <summary>
        /// Tolker meldingen fra SimSim
        /// </summary>
        private void MeldingsTolker() //Tolker meldingen seriellporten får fra SimSim
        {
            DigitalI = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('D') + 1, 8);        // D
            DigitalO = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('E') + 1, 8);        // E
            Thermistor = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('F') + 1, 4);      // F
            AnalogInn1 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('G') + 1, 4);      // G
            AnalogInn2 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('H') + 1, 4);      // H
            Temp1 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('I') + 1, 3);           // I
            Temp2 = MeldingFraSimSim.Substring(MeldingFraSimSim.IndexOf('j') + 1, 3);           // I

            if (Convert.ToInt32(DigitalO.Substring(0, 3)) > 0) //Fordi oppgaven sier at SimSim skal kunne funke som tastatur
            {
                if (DigitalO[0] == '1')
                {
                    sp.Write("$O00");
                    PIN_innlesning('0');
                }
                if (DigitalO[1] == '1')
                {
                    sp.Write("$O10");
                    PIN_innlesning('1');
                }
                if (DigitalO[2] == '1')
                {
                    sp.Write("$O20");
                    PIN_innlesning('2');
                }
                if (DigitalO[3] == '1')
                {
                    sp.Write("$30");
                    PIN_innlesning('3');
                }
            }

            if (DigitalO[5] == '1') //Viser om døra er Låst
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

            if (DigitalO[6] == '1' && !locked) //Vise om døra er åpen
            {
                if (!open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Open;
                    tAlarm.Enabled = true;
                    open = true;
                }
            }
            else if (DigitalO[6] == '0')
            {
                if (open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Closed;
                    tAlarm.Enabled = false;
                    open = false;
                    sp.WriteLine("$O51");
                }
            }

            if (Convert.ToInt32(AnalogInn1) > 500) //Her har noen BRYTET INN!
            {
                AlarmPå("Dør brutt opp");
            }
            else
            {
                ResetAlarm();
            }

            if (DigitalO[7] == '1') //Viser om alarmen er aktivert
            {
                AlarmPå("Utløst med inngang på SimSim");
            }
            else if (DigitalO[7] == '0')
            {
                ResetAlarm();
            }


        } //Sjekk alarmdel her

        /// <summary>
        /// Utløser alarmen da døren har stått åpen for lenge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tAlarm_Tick(object sender, EventArgs e)
        {
            AlarmPå("Dør har stått åpen for lenge");
        }

        /// <summary>
        /// Når man bytter COMport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                sp.Write("$R1");        //Be om en melding
                Thread.Sleep(1000);
                sp.Write("$E1");        //Be om melding ved endring av data
                Thread.Sleep(1000);
                sp.Write("$S002");      //Oppdater hvert 2. sekund
            }
            catch (Exception)
            {
                MessageBox.Show("Prøv en annen serieport");
            }
        }

    }
}
