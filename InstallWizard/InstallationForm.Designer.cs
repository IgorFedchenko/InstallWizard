using System.Windows.Forms;

namespace InstallWizard
{
    partial class InstallationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel InstallatoinPanel = null;

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
            this.InstallatoinPanel = new System.Windows.Forms.Panel();
            this.NextBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallatoinPanel
            // 
            this.InstallatoinPanel.AutoScroll = true;
            this.InstallatoinPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.InstallatoinPanel.Location = new System.Drawing.Point(0, 0);
            this.InstallatoinPanel.Name = "InstallatoinPanel";
            this.InstallatoinPanel.Size = new System.Drawing.Size(477, 258);
            this.InstallatoinPanel.TabIndex = 0;
            // 
            // NextBtn
            // 
            this.NextBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.NextBtn.Location = new System.Drawing.Point(402, 258);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 45);
            this.NextBtn.TabIndex = 0;
            this.NextBtn.Text = "Next";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.Next_Click);
            // 
            // InstallationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(477, 303);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.InstallatoinPanel);
            this.Name = "InstallationForm";
            this.Text = "Exo Installation Wizard";
            this.ResumeLayout(false);

        }

        #endregion

        private Button NextBtn;
    }
}

