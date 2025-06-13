namespace WindowsFormsApp1
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
            this.micBox = new System.Windows.Forms.ComboBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.ucEchocardioscopy1 = new WindowsFormsApp1.ucEchocardioscopy();
            this.SuspendLayout();
            // 
            // micBox
            // 
            this.micBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.micBox.FormattingEnabled = true;
            this.micBox.Location = new System.Drawing.Point(1094, 403);
            this.micBox.Name = "micBox";
            this.micBox.Size = new System.Drawing.Size(121, 24);
            this.micBox.TabIndex = 8;
            this.micBox.SelectedIndexChanged += new System.EventHandler(this.micBox_SelectedIndexChanged_1);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.stopButton.Location = new System.Drawing.Point(1184, 471);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(179, 23);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "Конец записи";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startButton.Location = new System.Drawing.Point(970, 471);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(194, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Старт записи";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // ucEchocardioscopy1
            // 
            this.ucEchocardioscopy1.AutoSize = true;
            this.ucEchocardioscopy1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucEchocardioscopy1.BackColor = System.Drawing.SystemColors.Window;
            this.ucEchocardioscopy1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucEchocardioscopy1.Location = new System.Drawing.Point(0, 0);
            this.ucEchocardioscopy1.Margin = new System.Windows.Forms.Padding(5);
            this.ucEchocardioscopy1.Name = "ucEchocardioscopy1";
            this.ucEchocardioscopy1.Size = new System.Drawing.Size(1434, 1108);
            this.ucEchocardioscopy1.TabIndex = 0;
            this.ucEchocardioscopy1.Load += new System.EventHandler(this.ucEchocardioscopy1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1455, 731);
            this.Controls.Add(this.micBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.ucEchocardioscopy1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucEchocardioscopy ucEchocardioscopy1;
        private System.Windows.Forms.ComboBox micBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
    }
}

