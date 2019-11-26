namespace Шахматы
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
            this.GB_MainField = new System.Windows.Forms.GroupBox();
            this.B_Start = new System.Windows.Forms.Button();
            this.L_indicator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GB_MainField
            // 
            this.GB_MainField.Location = new System.Drawing.Point(12, 12);
            this.GB_MainField.Name = "GB_MainField";
            this.GB_MainField.Size = new System.Drawing.Size(650, 650);
            this.GB_MainField.TabIndex = 7;
            this.GB_MainField.TabStop = false;
            this.GB_MainField.Text = "Шахматное поле";
            // 
            // B_Start
            // 
            this.B_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Start.Location = new System.Drawing.Point(750, 52);
            this.B_Start.Name = "B_Start";
            this.B_Start.Size = new System.Drawing.Size(347, 65);
            this.B_Start.TabIndex = 9;
            this.B_Start.Text = "Старт";
            this.B_Start.UseVisualStyleBackColor = true;
            this.B_Start.Click += new System.EventHandler(this.B_Start_Click);
            // 
            // L_indicator
            // 
            this.L_indicator.AutoSize = true;
            this.L_indicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.L_indicator.Location = new System.Drawing.Point(744, 120);
            this.L_indicator.Name = "L_indicator";
            this.L_indicator.Size = new System.Drawing.Size(180, 32);
            this.L_indicator.TabIndex = 10;
            this.L_indicator.Text = "ходят белые";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.L_indicator);
            this.Controls.Add(this.B_Start);
            this.Controls.Add(this.GB_MainField);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "Form1";
            this.Text = "Simple_Chess";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GB_MainField;
        private System.Windows.Forms.Button B_Start;
        private System.Windows.Forms.Label L_indicator;
    }
}

