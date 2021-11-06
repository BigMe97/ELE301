
namespace Sentral
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_LeggInn = new System.Windows.Forms.Button();
            this.txt_For_Navn = new System.Windows.Forms.TextBox();
            this.txt_EtterNavn = new System.Windows.Forms.TextBox();
            this.txt_Epost = new System.Windows.Forms.TextBox();
            this.txt_KortID = new System.Windows.Forms.TextBox();
            this.txt_DatoStart = new System.Windows.Forms.TextBox();
            this.txt_DatoSlutt = new System.Windows.Forms.TextBox();
            this.txt_Pin = new System.Windows.Forms.TextBox();
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_LeggInn
            // 
            this.btn_LeggInn.Location = new System.Drawing.Point(606, 77);
            this.btn_LeggInn.Name = "btn_LeggInn";
            this.btn_LeggInn.Size = new System.Drawing.Size(101, 36);
            this.btn_LeggInn.TabIndex = 8;
            this.btn_LeggInn.Text = "Legg til";
            this.btn_LeggInn.UseVisualStyleBackColor = true;
            this.btn_LeggInn.Click += new System.EventHandler(this.btn_LeggInn_Click);
            // 
            // txt_For_Navn
            // 
            this.txt_For_Navn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_For_Navn.Location = new System.Drawing.Point(132, 82);
            this.txt_For_Navn.Name = "txt_For_Navn";
            this.txt_For_Navn.Size = new System.Drawing.Size(254, 26);
            this.txt_For_Navn.TabIndex = 1;
            this.txt_For_Navn.Click += new System.EventHandler(this.Calendar_Leave);
            // 
            // txt_EtterNavn
            // 
            this.txt_EtterNavn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EtterNavn.Location = new System.Drawing.Point(132, 142);
            this.txt_EtterNavn.Name = "txt_EtterNavn";
            this.txt_EtterNavn.Size = new System.Drawing.Size(254, 26);
            this.txt_EtterNavn.TabIndex = 2;
            this.txt_EtterNavn.Click += new System.EventHandler(this.Calendar_Leave);
            // 
            // txt_Epost
            // 
            this.txt_Epost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Epost.Location = new System.Drawing.Point(132, 202);
            this.txt_Epost.Name = "txt_Epost";
            this.txt_Epost.Size = new System.Drawing.Size(254, 26);
            this.txt_Epost.TabIndex = 3;
            this.txt_Epost.Click += new System.EventHandler(this.Calendar_Leave);
            // 
            // txt_KortID
            // 
            this.txt_KortID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_KortID.Location = new System.Drawing.Point(132, 262);
            this.txt_KortID.Name = "txt_KortID";
            this.txt_KortID.Size = new System.Drawing.Size(71, 26);
            this.txt_KortID.TabIndex = 4;
            this.txt_KortID.Click += new System.EventHandler(this.Calendar_Leave);
            this.txt_KortID.TextChanged += new System.EventHandler(this.txt_KortID_TextChanged);
            // 
            // txt_DatoStart
            // 
            this.txt_DatoStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DatoStart.Location = new System.Drawing.Point(132, 322);
            this.txt_DatoStart.Name = "txt_DatoStart";
            this.txt_DatoStart.Size = new System.Drawing.Size(191, 26);
            this.txt_DatoStart.TabIndex = 5;
            this.txt_DatoStart.Click += new System.EventHandler(this.txt_DatoStart_Click);
            // 
            // txt_DatoSlutt
            // 
            this.txt_DatoSlutt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DatoSlutt.Location = new System.Drawing.Point(359, 322);
            this.txt_DatoSlutt.Name = "txt_DatoSlutt";
            this.txt_DatoSlutt.Size = new System.Drawing.Size(191, 26);
            this.txt_DatoSlutt.TabIndex = 6;
            this.txt_DatoSlutt.Click += new System.EventHandler(this.txt_DatoSlutt_Click);
            // 
            // txt_Pin
            // 
            this.txt_Pin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pin.Location = new System.Drawing.Point(132, 382);
            this.txt_Pin.Name = "txt_Pin";
            this.txt_Pin.Size = new System.Drawing.Size(71, 26);
            this.txt_Pin.TabIndex = 7;
            this.txt_Pin.Click += new System.EventHandler(this.Calendar_Leave);
            this.txt_Pin.TextChanged += new System.EventHandler(this.txt_Pin_TextChanged);
            // 
            // Calendar
            // 
            this.Calendar.Location = new System.Drawing.Point(551, 270);
            this.Calendar.Name = "Calendar";
            this.Calendar.TabIndex = 100;
            this.Calendar.Visible = false;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            this.Calendar.Leave += new System.EventHandler(this.Calendar_Leave);
            this.Calendar.MouseLeave += new System.EventHandler(this.Calendar_Leave);
            this.Calendar.Validated += new System.EventHandler(this.Calendar_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Fornavn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Etternavn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "E-Post adresse";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Kort ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Gyldig fra";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(329, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Til";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "PIN kode";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Calendar);
            this.Controls.Add(this.txt_Pin);
            this.Controls.Add(this.txt_DatoSlutt);
            this.Controls.Add(this.txt_DatoStart);
            this.Controls.Add(this.txt_KortID);
            this.Controls.Add(this.txt_Epost);
            this.Controls.Add(this.txt_EtterNavn);
            this.Controls.Add(this.txt_For_Navn);
            this.Controls.Add(this.btn_LeggInn);
            this.Name = "Form1";
            this.Text = "Adgangs Sentral";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Click += new System.EventHandler(this.Calendar_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_LeggInn;
        private System.Windows.Forms.TextBox txt_For_Navn;
        private System.Windows.Forms.TextBox txt_EtterNavn;
        private System.Windows.Forms.TextBox txt_Epost;
        private System.Windows.Forms.TextBox txt_KortID;
        private System.Windows.Forms.TextBox txt_DatoStart;
        private System.Windows.Forms.TextBox txt_DatoSlutt;
        private System.Windows.Forms.TextBox txt_Pin;
        private System.Windows.Forms.MonthCalendar Calendar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

