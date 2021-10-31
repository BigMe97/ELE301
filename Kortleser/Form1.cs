using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kortleser
{
    public partial class Kortleser : Form
    {
        static string passcode = "12345";



        public Kortleser()
        {
            InitializeComponent();
        }

        private void txt_Kortlesernummer_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_Kortlesernummer.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_Kortlesernummer.Text = txt.Substring(0, 4);
                    txt_Kortlesernummer.SelectionStart = 4;
                    // throw new InvalidOperationException("Kortnummer kan bare være 4 tegn");

                }

            }
            //catch (InvalidExpressionException not_int)
            //{
            //    txt_Kortlesernummer.Text = txt.Substring(0, txt.Length-1);
            //    MessageBox.Show(not_int.Message);
            //}
            catch (Exception i)
            {
                txt_Kortlesernummer.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
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
                    // throw new InvalidOperationException("Kortnummer kan bare være 4 tegn");

                }

            }
            //catch (InvalidExpressionException not_int)
            //{
            //    txt_KortID.Text = txt.Substring(0, txt.Length - 1);
            //    MessageBox.Show(not_int.Message);
            //}
            catch (Exception i)
            {
                txt_KortID.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }
        }



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


        // Veit ikke hva/om vi skal bruke den til noe
        private void btn_num_HASH_Click(object sender, EventArgs e)
        {
            TM_kode('#');
        }


        static void TM_kode(char input)
        {
            switch (passcode.Length)
            {
                case 5:
                    passcode = input.ToString();
                    break;
                case 1:
                    passcode = passcode+input;
                    break;

                case 2:
                    passcode = passcode + input;
                    break;

                case 3:
                    passcode = passcode + input;
                    Send(passcode);
                    passcode = "12345";
                    break;

                default:
                    MessageBox.Show("Noe feilet");
                    passcode = "12345";
                    break;
            }

        }


        static void Send(string message)
        {
            MessageBox.Show(message);
        }

        private void Kortleser_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Programmet vil avslutte...");
        }
    }
}
