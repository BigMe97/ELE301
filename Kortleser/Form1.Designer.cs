
namespace Kortleser
{
    partial class Kortleser
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
            this.components = new System.ComponentModel.Container();
            this.btn_num_1 = new System.Windows.Forms.Button();
            this.txt_KortID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sp = new System.IO.Ports.SerialPort(this.components);
            this.btn_num_2 = new System.Windows.Forms.Button();
            this.btn_num_3 = new System.Windows.Forms.Button();
            this.btn_num_4 = new System.Windows.Forms.Button();
            this.btn_num_5 = new System.Windows.Forms.Button();
            this.btn_num_6 = new System.Windows.Forms.Button();
            this.btn_num_7 = new System.Windows.Forms.Button();
            this.btn_num_8 = new System.Windows.Forms.Button();
            this.btn_num_9 = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_num_0 = new System.Windows.Forms.Button();
            this.btn_num_HASH = new System.Windows.Forms.Button();
            this.cbCOMPort = new System.Windows.Forms.ComboBox();
            this.tD4 = new System.Windows.Forms.Timer(this.components);
            this.pbLocked = new System.Windows.Forms.PictureBox();
            this.pbDoor = new System.Windows.Forms.PictureBox();
            this.pbAlarm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_num_1
            // 
            this.btn_num_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_1.FlatAppearance.BorderSize = 3;
            this.btn_num_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_1.Location = new System.Drawing.Point(29, 96);
            this.btn_num_1.Name = "btn_num_1";
            this.btn_num_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_1.Size = new System.Drawing.Size(76, 55);
            this.btn_num_1.TabIndex = 2;
            this.btn_num_1.Text = "1";
            this.btn_num_1.UseVisualStyleBackColor = true;
            this.btn_num_1.Click += new System.EventHandler(this.btn_num_1_Click);
            // 
            // txt_KortID
            // 
            this.txt_KortID.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_KortID.Location = new System.Drawing.Point(29, 45);
            this.txt_KortID.Name = "txt_KortID";
            this.txt_KortID.Size = new System.Drawing.Size(93, 29);
            this.txt_KortID.TabIndex = 1;
            this.txt_KortID.TextChanged += new System.EventHandler(this.txt_KortID_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "KortID";
            // 
            // sp
            // 
            this.sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_DataReceived);
            // 
            // btn_num_2
            // 
            this.btn_num_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_2.FlatAppearance.BorderSize = 3;
            this.btn_num_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_2.Location = new System.Drawing.Point(136, 96);
            this.btn_num_2.Name = "btn_num_2";
            this.btn_num_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_2.Size = new System.Drawing.Size(76, 55);
            this.btn_num_2.TabIndex = 4;
            this.btn_num_2.Text = "2";
            this.btn_num_2.UseVisualStyleBackColor = true;
            this.btn_num_2.Click += new System.EventHandler(this.btn_num_2_Click);
            // 
            // btn_num_3
            // 
            this.btn_num_3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_3.FlatAppearance.BorderSize = 3;
            this.btn_num_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_3.Location = new System.Drawing.Point(243, 96);
            this.btn_num_3.Name = "btn_num_3";
            this.btn_num_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_3.Size = new System.Drawing.Size(76, 55);
            this.btn_num_3.TabIndex = 5;
            this.btn_num_3.Text = "3";
            this.btn_num_3.UseVisualStyleBackColor = true;
            this.btn_num_3.Click += new System.EventHandler(this.btn_num_3_Click);
            // 
            // btn_num_4
            // 
            this.btn_num_4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_4.FlatAppearance.BorderSize = 3;
            this.btn_num_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_4.Location = new System.Drawing.Point(29, 184);
            this.btn_num_4.Name = "btn_num_4";
            this.btn_num_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_4.Size = new System.Drawing.Size(76, 55);
            this.btn_num_4.TabIndex = 6;
            this.btn_num_4.Text = "4";
            this.btn_num_4.UseVisualStyleBackColor = true;
            this.btn_num_4.Click += new System.EventHandler(this.btn_num_4_Click);
            // 
            // btn_num_5
            // 
            this.btn_num_5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_5.FlatAppearance.BorderSize = 3;
            this.btn_num_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_5.Location = new System.Drawing.Point(135, 184);
            this.btn_num_5.Name = "btn_num_5";
            this.btn_num_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_5.Size = new System.Drawing.Size(76, 55);
            this.btn_num_5.TabIndex = 7;
            this.btn_num_5.Text = "5";
            this.btn_num_5.UseVisualStyleBackColor = true;
            this.btn_num_5.Click += new System.EventHandler(this.btn_num_5_Click);
            // 
            // btn_num_6
            // 
            this.btn_num_6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_6.FlatAppearance.BorderSize = 3;
            this.btn_num_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_6.Location = new System.Drawing.Point(243, 184);
            this.btn_num_6.Name = "btn_num_6";
            this.btn_num_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_6.Size = new System.Drawing.Size(76, 55);
            this.btn_num_6.TabIndex = 8;
            this.btn_num_6.Text = "6";
            this.btn_num_6.UseVisualStyleBackColor = true;
            this.btn_num_6.Click += new System.EventHandler(this.btn_num_6_Click);
            // 
            // btn_num_7
            // 
            this.btn_num_7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_7.FlatAppearance.BorderSize = 3;
            this.btn_num_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_7.Location = new System.Drawing.Point(29, 272);
            this.btn_num_7.Name = "btn_num_7";
            this.btn_num_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_7.Size = new System.Drawing.Size(76, 55);
            this.btn_num_7.TabIndex = 9;
            this.btn_num_7.Text = "7";
            this.btn_num_7.UseVisualStyleBackColor = true;
            this.btn_num_7.Click += new System.EventHandler(this.btn_num_7_Click);
            // 
            // btn_num_8
            // 
            this.btn_num_8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_8.FlatAppearance.BorderSize = 3;
            this.btn_num_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_8.Location = new System.Drawing.Point(136, 272);
            this.btn_num_8.Name = "btn_num_8";
            this.btn_num_8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_8.Size = new System.Drawing.Size(76, 55);
            this.btn_num_8.TabIndex = 10;
            this.btn_num_8.Text = "8";
            this.btn_num_8.UseVisualStyleBackColor = true;
            this.btn_num_8.Click += new System.EventHandler(this.btn_num_8_Click);
            // 
            // btn_num_9
            // 
            this.btn_num_9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_9.FlatAppearance.BorderSize = 3;
            this.btn_num_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_9.Location = new System.Drawing.Point(243, 272);
            this.btn_num_9.Name = "btn_num_9";
            this.btn_num_9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_9.Size = new System.Drawing.Size(76, 55);
            this.btn_num_9.TabIndex = 11;
            this.btn_num_9.Text = "9";
            this.btn_num_9.UseVisualStyleBackColor = true;
            this.btn_num_9.Click += new System.EventHandler(this.btn_num_9_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Clear.FlatAppearance.BorderSize = 3;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Location = new System.Drawing.Point(29, 360);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Clear.Size = new System.Drawing.Size(76, 55);
            this.btn_Clear.TabIndex = 12;
            this.btn_Clear.Text = "C";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_num_0
            // 
            this.btn_num_0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_0.FlatAppearance.BorderSize = 3;
            this.btn_num_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_0.Location = new System.Drawing.Point(136, 360);
            this.btn_num_0.Name = "btn_num_0";
            this.btn_num_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_0.Size = new System.Drawing.Size(76, 55);
            this.btn_num_0.TabIndex = 13;
            this.btn_num_0.Text = "0";
            this.btn_num_0.UseVisualStyleBackColor = true;
            this.btn_num_0.Click += new System.EventHandler(this.btn_num_0_Click);
            // 
            // btn_num_HASH
            // 
            this.btn_num_HASH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_num_HASH.FlatAppearance.BorderSize = 3;
            this.btn_num_HASH.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_num_HASH.Location = new System.Drawing.Point(243, 360);
            this.btn_num_HASH.Name = "btn_num_HASH";
            this.btn_num_HASH.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_num_HASH.Size = new System.Drawing.Size(76, 55);
            this.btn_num_HASH.TabIndex = 14;
            this.btn_num_HASH.Text = "#";
            this.btn_num_HASH.UseVisualStyleBackColor = true;
            this.btn_num_HASH.Click += new System.EventHandler(this.btn_num_HASH_Click);
            // 
            // cbCOMPort
            // 
            this.cbCOMPort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbCOMPort.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCOMPort.FormattingEnabled = true;
            this.cbCOMPort.Location = new System.Drawing.Point(384, 45);
            this.cbCOMPort.Name = "cbCOMPort";
            this.cbCOMPort.Size = new System.Drawing.Size(76, 28);
            this.cbCOMPort.Sorted = true;
            this.cbCOMPort.TabIndex = 16;
            this.cbCOMPort.SelectedIndexChanged += new System.EventHandler(this.cbCOMPort_SelectedIndexChanged);
            // 
            // tD4
            // 
            this.tD4.Interval = 45000;
            this.tD4.Tick += new System.EventHandler(this.tD4_Tick);
            // 
            // pbLocked
            // 
            this.pbLocked.Image = global::Kortleser.Properties.Resources.locked;
            this.pbLocked.Location = new System.Drawing.Point(426, 266);
            this.pbLocked.Name = "pbLocked";
            this.pbLocked.Size = new System.Drawing.Size(50, 43);
            this.pbLocked.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLocked.TabIndex = 17;
            this.pbLocked.TabStop = false;
            // 
            // pbDoor
            // 
            this.pbDoor.Image = global::Kortleser.Properties.Resources.Door_Closed;
            this.pbDoor.InitialImage = null;
            this.pbDoor.Location = new System.Drawing.Point(384, 99);
            this.pbDoor.Name = "pbDoor";
            this.pbDoor.Size = new System.Drawing.Size(135, 161);
            this.pbDoor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDoor.TabIndex = 15;
            this.pbDoor.TabStop = false;
            // 
            // pbAlarm
            // 
            this.pbAlarm.Location = new System.Drawing.Point(416, 327);
            this.pbAlarm.Name = "pbAlarm";
            this.pbAlarm.Size = new System.Drawing.Size(71, 71);
            this.pbAlarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAlarm.TabIndex = 18;
            this.pbAlarm.TabStop = false;
            // 
            // Kortleser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 531);
            this.Controls.Add(this.pbAlarm);
            this.Controls.Add(this.pbLocked);
            this.Controls.Add(this.cbCOMPort);
            this.Controls.Add(this.pbDoor);
            this.Controls.Add(this.btn_num_HASH);
            this.Controls.Add(this.btn_num_0);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_num_9);
            this.Controls.Add(this.btn_num_8);
            this.Controls.Add(this.btn_num_7);
            this.Controls.Add(this.btn_num_6);
            this.Controls.Add(this.btn_num_5);
            this.Controls.Add(this.btn_num_4);
            this.Controls.Add(this.btn_num_3);
            this.Controls.Add(this.btn_num_2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_KortID);
            this.Controls.Add(this.btn_num_1);
            this.Name = "Kortleser";
            this.Text = "Kortleser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Kortleser_FormClosing);
            this.Load += new System.EventHandler(this.Kortleser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_num_1;
        private System.Windows.Forms.TextBox txt_KortID;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort sp;
        private System.Windows.Forms.Button btn_num_2;
        private System.Windows.Forms.Button btn_num_3;
        private System.Windows.Forms.Button btn_num_4;
        private System.Windows.Forms.Button btn_num_5;
        private System.Windows.Forms.Button btn_num_6;
        private System.Windows.Forms.Button btn_num_7;
        private System.Windows.Forms.Button btn_num_8;
        private System.Windows.Forms.Button btn_num_9;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_num_0;
        private System.Windows.Forms.Button btn_num_HASH;
        private System.Windows.Forms.PictureBox pbDoor;
        private System.Windows.Forms.ComboBox cbCOMPort;
        private System.Windows.Forms.Timer tD4;
        private System.Windows.Forms.PictureBox pbLocked;
        private System.Windows.Forms.PictureBox pbAlarm;
    }
}

