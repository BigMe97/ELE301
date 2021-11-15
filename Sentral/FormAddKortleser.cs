using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sentral
{
    public partial class FormAddKortleser : Form
    {
        Database DB;


        public FormAddKortleser(Database db)
        {
            InitializeComponent();
            DB = db;
        }

        private void txt_KortleserID_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_KortleserID.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_KortleserID.Text = txt.Substring(0, 4);
                    txt_KortleserID.SelectionStart = 4;
                }
            }
            catch (Exception i)
            {
                txt_KortleserID.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }
        }



        private void txt_From_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_From.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_From.Text = txt.Substring(0, 4);
                    txt_From.SelectionStart = 4;
                }
            }
            catch (Exception i)
            {
                txt_From.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }

        }




        private void txt_To_TextChanged(object sender, EventArgs e)
        {
            string txt = txt_To.Text;

            try
            {
                // Keep the string a number
                if (txt.Length > 0) { Convert.ToInt32(txt); }

                // Keep the string at 4 digits
                if (txt.Length > 4)
                {
                    txt_To.Text = txt.Substring(0, 4);
                    txt_To.SelectionStart = 4;
                }
            }
            catch (Exception i)
            {
                txt_To.Text = txt.Substring(0, txt.Length - 1);
                MessageBox.Show(i.Message);
            }

        }



        private void btn_LeggInn_Click(object sender, EventArgs e)
        {
            DB.QueryKortleser(txt_KortleserID.Text, txt_From.Text, txt_To.Text);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
