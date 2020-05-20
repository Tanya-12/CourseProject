namespace CustomArchiver
{
    internal partial class CustomForm
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
            this.bt_root = new System.Windows.Forms.Button();
            this.bt_create = new System.Windows.Forms.Button();
            this.bt_open = new System.Windows.Forms.Button();
            this.bt_unpack = new System.Windows.Forms.Button();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bt_root
            // 
            this.bt_root.BackColor = System.Drawing.Color.LightPink;
            this.bt_root.Location = new System.Drawing.Point(23, 12);
            this.bt_root.Name = "bt_root";
            this.bt_root.Size = new System.Drawing.Size(202, 23);
            this.bt_root.TabIndex = 0;
            this.bt_root.Text = "Select root folder for archiving...";
            this.bt_root.UseVisualStyleBackColor = false;
            // 
            // bt_create
            // 
            this.bt_create.BackColor = System.Drawing.Color.LightPink;
            this.bt_create.Location = new System.Drawing.Point(231, 12);
            this.bt_create.Name = "bt_create";
            this.bt_create.Size = new System.Drawing.Size(202, 23);
            this.bt_create.TabIndex = 3;
            this.bt_create.Text = "Create archive for selection...";
            this.bt_create.UseVisualStyleBackColor = false;
            // 
            // bt_open
            // 
            this.bt_open.BackColor = System.Drawing.Color.LightPink;
            this.bt_open.Location = new System.Drawing.Point(23, 41);
            this.bt_open.Name = "bt_open";
            this.bt_open.Size = new System.Drawing.Size(202, 23);
            this.bt_open.TabIndex = 4;
            this.bt_open.Text = "Select archive to analyze...";
            this.bt_open.UseVisualStyleBackColor = false;
            // 
            // bt_unpack
            // 
            this.bt_unpack.BackColor = System.Drawing.Color.LightPink;
            this.bt_unpack.Location = new System.Drawing.Point(231, 41);
            this.bt_unpack.Name = "bt_unpack";
            this.bt_unpack.Size = new System.Drawing.Size(202, 23);
            this.bt_unpack.TabIndex = 5;
            this.bt_unpack.Text = "Unpack archive to directory...";
            this.bt_unpack.UseVisualStyleBackColor = false;
            // 
            // tb_log
            // 
            this.tb_log.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tb_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tb_log.Location = new System.Drawing.Point(0, 83);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.Size = new System.Drawing.Size(447, 142);
            this.tb_log.TabIndex = 6;
            // 
            // CustomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(447, 225);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.bt_unpack);
            this.Controls.Add(this.bt_open);
            this.Controls.Add(this.bt_create);
            this.Controls.Add(this.bt_root);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CustomForm";
            this.ShowIcon = false;
            this.Text = "Archiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_root;
        private System.Windows.Forms.Button bt_create;
        private System.Windows.Forms.Button bt_open;
        private System.Windows.Forms.Button bt_unpack;
        private System.Windows.Forms.TextBox tb_log;
    }
}

