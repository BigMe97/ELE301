
namespace Sentral
{
    partial class FormAddKortleser
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
            this.label4 = new System.Windows.Forms.Label();
            this.txt_KortleserID = new System.Windows.Forms.TextBox();
            this.btn_LeggInn = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_From = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_To = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Kortleser ID";
            // 
            // txt_KortleserID
            // 
            this.txt_KortleserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_KortleserID.Location = new System.Drawing.Point(111, 76);
            this.txt_KortleserID.Name = "txt_KortleserID";
            this.txt_KortleserID.Size = new System.Drawing.Size(71, 26);
            this.txt_KortleserID.TabIndex = 16;
            this.txt_KortleserID.TextChanged += new System.EventHandler(this.txt_KortleserID_TextChanged);
            // 
            // btn_LeggInn
            // 
            this.btn_LeggInn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_LeggInn.Location = new System.Drawing.Point(45, 229);
            this.btn_LeggInn.Name = "btn_LeggInn";
            this.btn_LeggInn.Size = new System.Drawing.Size(101, 36);
            this.btn_LeggInn.TabIndex = 23;
            this.btn_LeggInn.Text = "Legg til kortleser";
            this.btn_LeggInn.UseVisualStyleBackColor = false;
            this.btn_LeggInn.Click += new System.EventHandler(this.btn_LeggInn_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Exit.Location = new System.Drawing.Point(178, 229);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(101, 36);
            this.btn_Exit.TabIndex = 24;
            this.btn_Exit.Text = "Avslutt";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Fra rom";
            // 
            // txt_From
            // 
            this.txt_From.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_From.Location = new System.Drawing.Point(111, 118);
            this.txt_From.Name = "txt_From";
            this.txt_From.Size = new System.Drawing.Size(71, 26);
            this.txt_From.TabIndex = 25;
            this.txt_From.TextChanged += new System.EventHandler(this.txt_From_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Til rom";
            // 
            // txt_To
            // 
            this.txt_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_To.Location = new System.Drawing.Point(111, 160);
            this.txt_To.Name = "txt_To";
            this.txt_To.Size = new System.Drawing.Size(71, 26);
            this.txt_To.TabIndex = 27;
            this.txt_To.TextChanged += new System.EventHandler(this.txt_To_TextChanged);
            // 
            // FormAddKortleser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 296);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_To);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_From);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_LeggInn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_KortleserID);
            this.Name = "FormAddKortleser";
            this.Text = "Legg til ny kortleser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_KortleserID;
        private System.Windows.Forms.Button btn_LeggInn;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_From;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_To;
    }
}