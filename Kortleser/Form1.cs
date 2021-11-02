using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO.Ports;

namespace Kortleser
{
    public partial class Kortleser : Form
    {
        // public SerialPort sp = new SerialPort();
        public string MeldingFraSimSim = "";
        static string passcode = "12345";
        static string kortlesesernummer;
        static bool OK = false, Cancel = false;
        static string DigitalO, DigitalI, Thermistor, AnalogInn1, AnalogInn2, Temp1, Temp2;
        static bool open = false, alarm = false, locked = true;
        

        public Kortleser()
        {
            InitializeComponent();
            VelgKortleserNummer();

        }



        private void Kortleser_Load(object sender, EventArgs e)
        {
            if (Cancel)
            {
                Application.Exit();
            }
            OppdaterComPorter();
        }



        private void cbCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sp.Close();
                sp.PortName = cbCOMPort.SelectedItem.ToString();
                sp.BaudRate = 9600;
                sp.ReadTimeout = 1000;
                sp.Open();
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
            for (int i = 0; i < alleComPortNavn.Length; i++)
            {
                cbCOMPort.Items.Add(alleComPortNavn[i]);
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
                            Send(passcode);
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
        public void Send(string message)
        {
            MessageBox.Show("Koden: " + message + "\nMelding fra SimSim: \n" + MeldingFraSimSim);
        }



        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = sp.ReadExisting();
            // MessageBox.Show(data);
            if ((data.Length == 65) && (data.IndexOf("$") == 2) && (data.IndexOf("#") == 64))
            {
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

            // Vise om døra er åpen
            if(DigitalO[6] == '1')
            {
                if (!open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Open;
                    open = true;
                }
            }
            else if(DigitalO[6] == '0')
            {
                if (open)
                {
                    pbDoor.Image = global::Kortleser.Properties.Resources.Door_Closed;
                    open = false;
                }
            }
            
        }










        private void tD4_Tick(object sender, EventArgs e)
        {
            sp.Write("$O40");
            tD4.Enabled = false;
            txt_KortID.Text = "";
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
            }
        }





    }
}
