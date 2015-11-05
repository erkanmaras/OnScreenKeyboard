namespace OnScreenKeyboardDemo
{
    partial class FrmMain
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblClickMe = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.keyboardControl = new OnScreenKeyboard.Keyboard();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(65, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(558, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(65, 39);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(558, 20);
            this.textBox2.TabIndex = 1;
            // 
            // lblClickMe
            // 
            this.lblClickMe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClickMe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClickMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickMe.Location = new System.Drawing.Point(640, 12);
            this.lblClickMe.Name = "lblClickMe";
            this.lblClickMe.Size = new System.Drawing.Size(87, 47);
            this.lblClickMe.TabIndex = 2;
            this.lblClickMe.Text = "Open Dialog!";
            this.lblClickMe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClickMe.Click += new System.EventHandler(this.lblClickMe_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.keyboardControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 129);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(729, 290);
            this.panel1.TabIndex = 4;
            // 
            // keyboardControl
            // 
            this.keyboardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyboardControl.GutterSize = 0;
            this.keyboardControl.Location = new System.Drawing.Point(2, 2);
            this.keyboardControl.Name = "keyboardControl";
            this.keyboardControl.Size = new System.Drawing.Size(725, 286);
            this.keyboardControl.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Location = new System.Drawing.Point(12, 42);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(49, 13);
            this.lblSurname.TabIndex = 6;
            this.lblSurname.Text = "Surname";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 419);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblClickMe);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.MinimumSize = new System.Drawing.Size(745, 458);
            this.Name = "FrmMain";
            this.Text = "Demo";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblClickMe;
        private OnScreenKeyboard.Keyboard keyboardControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSurname;
    }
}

