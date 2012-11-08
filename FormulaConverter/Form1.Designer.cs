namespace Formula
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
            this.txtInputFormula = new System.Windows.Forms.TextBox();
            this.txtOutputFormula = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInputFormula
            // 
            this.txtInputFormula.Location = new System.Drawing.Point(12, 12);
            this.txtInputFormula.Name = "txtInputFormula";
            this.txtInputFormula.Size = new System.Drawing.Size(419, 20);
            this.txtInputFormula.TabIndex = 0;
            // 
            // txtOutputFormula
            // 
            this.txtOutputFormula.Location = new System.Drawing.Point(12, 67);
            this.txtOutputFormula.Name = "txtOutputFormula";
            this.txtOutputFormula.ReadOnly = true;
            this.txtOutputFormula.Size = new System.Drawing.Size(419, 20);
            this.txtOutputFormula.TabIndex = 2;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(356, 38);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 103);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtOutputFormula);
            this.Controls.Add(this.txtInputFormula);
            this.Name = "Form1";
            this.Text = "Infix to Postfic notation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputFormula;
        private System.Windows.Forms.TextBox txtOutputFormula;
        private System.Windows.Forms.Button btnConvert;
    }
}

