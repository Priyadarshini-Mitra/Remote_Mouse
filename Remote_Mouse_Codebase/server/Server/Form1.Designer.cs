namespace Server
{
    partial class frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            this.txtb_ServerDisplay = new System.Windows.Forms.TextBox();
            this.btn_ServerOnOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_ServerDisplay
            // 
            this.txtb_ServerDisplay.Location = new System.Drawing.Point(13, 13);
            this.txtb_ServerDisplay.Multiline = true;
            this.txtb_ServerDisplay.Name = "txtb_ServerDisplay";
            this.txtb_ServerDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_ServerDisplay.Size = new System.Drawing.Size(438, 622);
            this.txtb_ServerDisplay.TabIndex = 0;
            // 
            // btn_ServerOnOff
            // 
            this.btn_ServerOnOff.Location = new System.Drawing.Point(458, 13);
            this.btn_ServerOnOff.Name = "btn_ServerOnOff";
            this.btn_ServerOnOff.Size = new System.Drawing.Size(116, 23);
            this.btn_ServerOnOff.TabIndex = 1;
            this.btn_ServerOnOff.Text = "Turn Server On";
            this.btn_ServerOnOff.UseVisualStyleBackColor = true;
            this.btn_ServerOnOff.Click += new System.EventHandler(this.btn_ServerOnOff_Click);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 647);
            this.Controls.Add(this.btn_ServerOnOff);
            this.Controls.Add(this.txtb_ServerDisplay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Main";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_ServerDisplay;
        private System.Windows.Forms.Button btn_ServerOnOff;
    }
}

