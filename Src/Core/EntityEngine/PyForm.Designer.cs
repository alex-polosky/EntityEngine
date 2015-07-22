namespace EntityEngine
{
    partial class PyForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.stdOut = new System.Windows.Forms.TextBox();
            this.stdErr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsTab = true;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(432, 542);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(432, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stdOut
            // 
            this.stdOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdOut.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.stdOut.Location = new System.Drawing.Point(451, 12);
            this.stdOut.Multiline = true;
            this.stdOut.Name = "stdOut";
            this.stdOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.stdOut.Size = new System.Drawing.Size(339, 302);
            this.stdOut.TabIndex = 3;
            this.stdOut.WordWrap = false;
            // 
            // stdErr
            // 
            this.stdErr.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdErr.ForeColor = System.Drawing.Color.Red;
            this.stdErr.Location = new System.Drawing.Point(451, 320);
            this.stdErr.Multiline = true;
            this.stdErr.Name = "stdErr";
            this.stdErr.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.stdErr.Size = new System.Drawing.Size(339, 268);
            this.stdErr.TabIndex = 4;
            this.stdErr.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 600);
            this.Controls.Add(this.stdErr);
            this.Controls.Add(this.stdOut);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox stdOut;
        private System.Windows.Forms.TextBox stdErr;
    }
}

