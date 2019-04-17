using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace SLSShippingApp
{
    public partial class WindowsUsers : Form
    {
        public WindowsUsers()
        {
            InitializeComponent();
        }

        private void WindowsUsers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsWindowsUsers.tblWindowsUsers' table. You can move, or remove it, as needed.
            PopulateWindowsUsers();
        }

        private void PopulateWindowsUsers()
        {
            DisableGridEvents();
            dgvWindowsUsers.DataSource = null;

            NameValueCollection nvcc = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/WindowsUsers");
            DataTable dtUsers = new DataTable("WindowsUsers");
            DataColumn dcName = new DataColumn("WindowsLogin", typeof(String));
            DataColumn dcScreen = new DataColumn("DefaultScanScreen", typeof(String));
            dtUsers.Columns.Add(dcName);
            dtUsers.Columns.Add(dcScreen);

            foreach (String key in nvcc.AllKeys)
            {
                object[] rowVals = new object[2];
                DataRowCollection rowCollection = dtUsers.Rows;
                rowVals[0] = key.ToString();
                rowVals[1] = nvcc[key].ToString();
                DataRow row = rowCollection.Add(rowVals);
            }

            try
            {
                dgvWindowsUsers.DataSource = dtUsers;
                dgvWindowsUsers.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error retrieving Windows Users: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                EnableGridEvents();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (txtWindowsLogin.Text.Trim() == String.Empty)
                return;

            String sWindowUser = txtWindowsLogin.Text.Trim();
            String sDefaultScreen = cbDefaultScanScreen.Text.ToString();

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                NameValueCollection nvcc = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/WindowsUsers");
                nvcc.Add(sWindowUser, sDefaultScreen);

                //save the file
                config.Save(ConfigurationSaveMode.Modified);
                //relaod the section you modified
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error adding Windows User: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                PopulateWindowsUsers();
            }
        }

        private void dgvWindowsUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvWindowsUsers.IsCurrentCellDirty)
            {
                dgvWindowsUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvWindowsUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow dgvRow = (DataGridViewRow)dgvWindowsUsers.Rows[e.RowIndex];
            if (dgvRow.Cells[0].Value.ToString() == String.Empty)
                return;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            NameValueCollection nvcc = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/WindowsUsers");

            String sWindowLogin = dgvRow.Cells[0].Value.ToString();
            String sDefaultScreen = dgvRow.Cells[1].Value.ToString();

            try
            {
                nvcc[sWindowLogin] = sDefaultScreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error updating Windows User: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                PopulateWindowsUsers();
            }
        }

        private void DisableGridEvents()
        {
            dgvWindowsUsers.CurrentCellDirtyStateChanged -= new EventHandler(dgvWindowsUsers_CurrentCellDirtyStateChanged);
            dgvWindowsUsers.CellValueChanged -= new DataGridViewCellEventHandler(dgvWindowsUsers_CellValueChanged);
        }
        private void EnableGridEvents()
        {
            dgvWindowsUsers.CurrentCellDirtyStateChanged += new EventHandler(dgvWindowsUsers_CurrentCellDirtyStateChanged);
            dgvWindowsUsers.CellValueChanged += new DataGridViewCellEventHandler(dgvWindowsUsers_CellValueChanged);
        }

        private void dgvWindowsUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            if (MessageBox.Show(String.Format("Are you sure you want to DELETE user:{0}?", e.Row.Cells[0].Value), "Add/Edit Windows Users", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String sWindowsLogin = e.Row.Cells[0].Value.ToString();
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    NameValueCollection nvcc = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/WindowsUsers");
                    nvcc.Remove(sWindowsLogin);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error DELETING Windows User: {0} - {1}", e.Row.Cells[0].Value, ex.Message), "Add/Edit Windows Users");

                }
                finally
                {
                    PopulateWindowsUsers();
                }
            }
            else
            {
                e.Cancel = true;
                dgvWindowsUsers.CancelEdit();
                PopulateWindowsUsers();
            }
        }
    }
}
