namespace SLSShippingApp
{
    partial class WindowsUsers
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
            this.scWindowsUsers = new System.Windows.Forms.SplitContainer();
            this.gbAddWindowsUser = new System.Windows.Forms.GroupBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.cbDefaultInputType = new System.Windows.Forms.ComboBox();
            this.cbDefaultScanScreen = new System.Windows.Forms.ComboBox();
            this.txtWindowsLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvWindowsUsers = new System.Windows.Forms.DataGridView();
            this.WindowsLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultScanScreen = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DefaultInputType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scWindowsUsers)).BeginInit();
            this.scWindowsUsers.Panel1.SuspendLayout();
            this.scWindowsUsers.Panel2.SuspendLayout();
            this.scWindowsUsers.SuspendLayout();
            this.gbAddWindowsUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWindowsUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // scWindowsUsers
            // 
            this.scWindowsUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scWindowsUsers.Location = new System.Drawing.Point(0, 0);
            this.scWindowsUsers.Name = "scWindowsUsers";
            this.scWindowsUsers.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scWindowsUsers.Panel1
            // 
            this.scWindowsUsers.Panel1.Controls.Add(this.gbAddWindowsUser);
            // 
            // scWindowsUsers.Panel2
            // 
            this.scWindowsUsers.Panel2.Controls.Add(this.dgvWindowsUsers);
            this.scWindowsUsers.Size = new System.Drawing.Size(500, 369);
            this.scWindowsUsers.SplitterDistance = 170;
            this.scWindowsUsers.TabIndex = 0;
            // 
            // gbAddWindowsUser
            // 
            this.gbAddWindowsUser.Controls.Add(this.btnAddUser);
            this.gbAddWindowsUser.Controls.Add(this.cbDefaultInputType);
            this.gbAddWindowsUser.Controls.Add(this.cbDefaultScanScreen);
            this.gbAddWindowsUser.Controls.Add(this.txtWindowsLogin);
            this.gbAddWindowsUser.Controls.Add(this.label3);
            this.gbAddWindowsUser.Controls.Add(this.label2);
            this.gbAddWindowsUser.Controls.Add(this.label1);
            this.gbAddWindowsUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAddWindowsUser.Location = new System.Drawing.Point(0, 0);
            this.gbAddWindowsUser.Name = "gbAddWindowsUser";
            this.gbAddWindowsUser.Size = new System.Drawing.Size(500, 170);
            this.gbAddWindowsUser.TabIndex = 0;
            this.gbAddWindowsUser.TabStop = false;
            this.gbAddWindowsUser.Text = "Add User";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(200, 125);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 45;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // cbDefaultInputType
            // 
            this.cbDefaultInputType.FormattingEnabled = true;
            this.cbDefaultInputType.Items.AddRange(new object[] {
            "UPC",
            "SFO"});
            this.cbDefaultInputType.Location = new System.Drawing.Point(246, 82);
            this.cbDefaultInputType.Name = "cbDefaultInputType";
            this.cbDefaultInputType.Size = new System.Drawing.Size(93, 21);
            this.cbDefaultInputType.TabIndex = 10;
            // 
            // cbDefaultScanScreen
            // 
            this.cbDefaultScanScreen.FormattingEnabled = true;
            this.cbDefaultScanScreen.Items.AddRange(new object[] {
            "BAY0",
            "PRINTED",
            "QS",
            "VINYL"});
            this.cbDefaultScanScreen.Location = new System.Drawing.Point(246, 50);
            this.cbDefaultScanScreen.Name = "cbDefaultScanScreen";
            this.cbDefaultScanScreen.Size = new System.Drawing.Size(93, 21);
            this.cbDefaultScanScreen.TabIndex = 5;
            // 
            // txtWindowsLogin
            // 
            this.txtWindowsLogin.Location = new System.Drawing.Point(246, 19);
            this.txtWindowsLogin.Name = "txtWindowsLogin";
            this.txtWindowsLogin.Size = new System.Drawing.Size(93, 20);
            this.txtWindowsLogin.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Default Input Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Default Scan Screen:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Windows Login:";
            // 
            // dgvWindowsUsers
            // 
            this.dgvWindowsUsers.AllowUserToAddRows = false;
            this.dgvWindowsUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWindowsUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWindowsUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WindowsLogin,
            this.DefaultScanScreen,
            this.DefaultInputType});
            this.dgvWindowsUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWindowsUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvWindowsUsers.MultiSelect = false;
            this.dgvWindowsUsers.Name = "dgvWindowsUsers";
            this.dgvWindowsUsers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvWindowsUsers.Size = new System.Drawing.Size(500, 195);
            this.dgvWindowsUsers.TabIndex = 50;
            this.dgvWindowsUsers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWindowsUsers_CellValueChanged);
            this.dgvWindowsUsers.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvWindowsUsers_CurrentCellDirtyStateChanged);
            this.dgvWindowsUsers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvWindowsUsers_UserDeletingRow);
            // 
            // WindowsLogin
            // 
            this.WindowsLogin.DataPropertyName = "WindowsLogin";
            this.WindowsLogin.HeaderText = "Windows Login";
            this.WindowsLogin.Name = "WindowsLogin";
            this.WindowsLogin.ReadOnly = true;
            // 
            // DefaultScanScreen
            // 
            this.DefaultScanScreen.DataPropertyName = "DefaultScanScreen";
            this.DefaultScanScreen.HeaderText = "Scan Screen";
            this.DefaultScanScreen.Items.AddRange(new object[] {
            "BAY0",
            "PRINTED",
            "QS",
            "VINYL"});
            this.DefaultScanScreen.Name = "DefaultScanScreen";
            // 
            // DefaultInputType
            // 
            this.DefaultInputType.DataPropertyName = "DefaultInputType";
            this.DefaultInputType.HeaderText = "Input Type";
            this.DefaultInputType.Items.AddRange(new object[] {
            "UPC",
            "SFO"});
            this.DefaultInputType.Name = "DefaultInputType";
            // 
            // WindowsUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 369);
            this.Controls.Add(this.scWindowsUsers);
            this.Name = "WindowsUsers";
            this.Text = "Windows Users";
            this.Load += new System.EventHandler(this.WindowsUsers_Load);
            this.scWindowsUsers.Panel1.ResumeLayout(false);
            this.scWindowsUsers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scWindowsUsers)).EndInit();
            this.scWindowsUsers.ResumeLayout(false);
            this.gbAddWindowsUser.ResumeLayout(false);
            this.gbAddWindowsUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWindowsUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scWindowsUsers;
        private System.Windows.Forms.GroupBox gbAddWindowsUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox cbDefaultInputType;
        private System.Windows.Forms.ComboBox cbDefaultScanScreen;
        private System.Windows.Forms.TextBox txtWindowsLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvWindowsUsers;
       // private System.Windows.Forms.BindingSource dsWindowsUsersBindingSource;
          //      private System.Windows.Forms.DataGridViewTextBoxColumn windowsLoginDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn defaultScanScreenDataGridViewTextBoxColumn;
       // private System.Windows.Forms.DataGridViewTextBoxColumn defaultInputTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowsLogin;
        private System.Windows.Forms.DataGridViewComboBoxColumn DefaultScanScreen;
        private System.Windows.Forms.DataGridViewComboBoxColumn DefaultInputType;
    }
}