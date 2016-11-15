namespace TestingNewEngine0
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbRender = new System.Windows.Forms.CheckBox();
            this.cbPhysics = new System.Windows.Forms.CheckBox();
            this.cbSystem = new System.Windows.Forms.CheckBox();
            this.numThreadWait = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numThreadWait)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(13, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 212);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(369, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(450, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbRender
            // 
            this.cbRender.AutoSize = true;
            this.cbRender.Location = new System.Drawing.Point(81, 13);
            this.cbRender.Name = "cbRender";
            this.cbRender.Size = new System.Drawing.Size(61, 17);
            this.cbRender.TabIndex = 4;
            this.cbRender.Text = "Render";
            this.cbRender.UseVisualStyleBackColor = true;
            this.cbRender.CheckedChanged += new System.EventHandler(this.cbRender_CheckedChanged);
            // 
            // cbPhysics
            // 
            this.cbPhysics.AutoSize = true;
            this.cbPhysics.Location = new System.Drawing.Point(148, 13);
            this.cbPhysics.Name = "cbPhysics";
            this.cbPhysics.Size = new System.Drawing.Size(62, 17);
            this.cbPhysics.TabIndex = 5;
            this.cbPhysics.Text = "Physics";
            this.cbPhysics.UseVisualStyleBackColor = true;
            this.cbPhysics.CheckedChanged += new System.EventHandler(this.cbPhysics_CheckedChanged);
            // 
            // cbSystem
            // 
            this.cbSystem.AutoSize = true;
            this.cbSystem.Location = new System.Drawing.Point(15, 13);
            this.cbSystem.Name = "cbSystem";
            this.cbSystem.Size = new System.Drawing.Size(60, 17);
            this.cbSystem.TabIndex = 6;
            this.cbSystem.Text = "System";
            this.cbSystem.UseVisualStyleBackColor = true;
            this.cbSystem.CheckedChanged += new System.EventHandler(this.cbSystem_CheckedChanged);
            // 
            // numThreadWait
            // 
            this.numThreadWait.Location = new System.Drawing.Point(216, 10);
            this.numThreadWait.Name = "numThreadWait";
            this.numThreadWait.Size = new System.Drawing.Size(51, 20);
            this.numThreadWait.TabIndex = 7;
            this.numThreadWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Thread Wait (ms)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numThreadWait);
            this.Controls.Add(this.cbRender);
            this.Controls.Add(this.cbSystem);
            this.Controls.Add(this.cbPhysics);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numThreadWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbRender;
        private System.Windows.Forms.CheckBox cbPhysics;
        private System.Windows.Forms.CheckBox cbSystem;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numThreadWait;

    }
}

