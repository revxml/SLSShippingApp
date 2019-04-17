using System;
using System.Collections.Generic;
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
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        DataTable dt;
        String sSQL = String.Empty;
        SqlDataReader reader;

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
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SLSShippingAppConnection"].ConnectionString);
            sSQL = "SELECT * FROM tblWindowsUsers";
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            sqlCon.Open();
            dgvWindowsUsers.AutoGenerateColumns = false;

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt = new DataTable("WindowsLogins");
                dt.Load(reader);
                dgvWindowsUsers.DataSource = dt;
                dgvWindowsUsers.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error retrieving Windows Users: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                reader.Dispose();
                EnableGridEvents();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (txtWindowsLogin.Text.Trim() == String.Empty)
                return;

            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SLSShippingAppConnection"].ConnectionString);
            sSQL = String.Format("INSERT INTO tblWindowsUsers (WindowsLogin,DefaultScanScreen,DefaultInputType) VALUES ('{0}','{1}','{2}')", txtWindowsLogin.Text.Trim(), cbDefaultScanScreen.Text, cbDefaultInputType.Text);
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            sqlCon.Open();
            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error adding Windows User: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
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
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SLSShippingAppConnection"].ConnectionString);
            sSQL = String.Format("UPDATE tblWindowsUsers SET DefaultScanScreen = '{1}',DefaultInputType = '{2}' WHERE WindowsLogin = '{0}'", dgvRow.Cells[0].Value, dgvRow.Cells[1].Value, dgvRow.Cells[2].Value);
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            sqlCon.Open();
            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error updating Windows User: {0}", ex.Message), "Windows Users");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
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
                sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SLSShippingAppConnection"].ConnectionString);
                sSQL = String.Format("DELETE FROM tblWindowsUsers  WHERE WindowsLogin = '{0}'", e.Row.Cells[0].Value);
                sqlCmd = new SqlCommand(sSQL, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCon.Open();
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error DELETING Windows User: {0} - {1}", e.Row.Cells[0].Value, ex.Message), "Add/Edit Windows Users");

                }
                finally
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCmd.Dispose();
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
