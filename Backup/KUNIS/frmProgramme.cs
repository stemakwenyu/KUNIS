using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KUNIS
{
    public partial class frmProgramme : Form
    {
        public string query;
        public frmProgramme()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void frmProgramme_Load(object sender, EventArgs e)
        {
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            addDepartment();
        }
        private void addDepartment()
        {
            query = "SELECT * FROM department ORDER BY Department_ID ASC";
            conn cn = new conn();
            if (cn.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                this.cboDepID.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Department_ID"].ToString().Replace(" ", "") != "")
                    {
                        this.cboDepID.Items.Add(dataReader["Department_ID"].ToString());
                    }
                }

            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            cleanData();
        }
        private void cleanData()
        {
            txtProgID.Text = "";
            txtProgName.Text = "";
            txtDuration.Text = "";
            cboDepID.Text = "";
            txtProgID.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                query = "SELECT * FROM programme WHERE Prog_ID LIKE '" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        txtProgID.Text = dataReader["Prog_ID"].ToString();
                        txtProgName.Text = dataReader["Prog_Name"].ToString();
                        txtDuration.Text = dataReader["Duration"].ToString();
                        cboDepID.Text = dataReader["Department_ID"].ToString();

                    }
                    else
                    {
                        txtProgID.Text = "";
                        txtProgName.Text = "";
                        txtDuration.Text = "";
                        cboDepID.Text = "";

                    }

                }
                cn.CloseConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgID.Focus();
            }
            else if (txtProgName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgName.Focus();
            }
            else if (txtDuration.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDuration.Focus();
            }
            else if (cboDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboDepID.Focus();
            }
            else
            {
                if (FindRecord(txtProgID.Text) == true)
                {
                    MessageBox.Show(txtProgID.Text + " already exists!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProgID.Focus();
                }
                else
                {
                    query = "INSERT INTO programme VALUES('" + txtProgID.Text + "','" + txtProgName.Text + "','" + txtDuration.Text + "','" + cboDepID.Text + "')";
                    conn cn = new conn();
                    if (cn.OpenConnection() == true)
                    {
                        if (MessageBox.Show("Are you sure you want to save record?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                            cmd.ExecuteNonQuery();

                            cleanData();
                            MessageBox.Show("Records Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Records not Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtProgID.Focus();
                        }
                    }
                    cn.CloseConnection();
                }



            }
        }
        private bool FindRecord(string schval)
        {
            query = "SELECT * FROM programme WHERE Prog_ID='" + schval + "'";
            conn cn = new conn();
            if (cn.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    cn.CloseConnection();
                    return true;
                }
                else
                {
                    cn.CloseConnection();
                    return false;
                }


            }
            else
            {
                return false;
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgID.Focus();
            }
            else if (txtProgName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgName.Focus();
            }
            else if (txtDuration.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDuration.Focus();
            }
            else if (cboDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboDepID.Focus();
            }
            else
            {
                query = "UPDATE programme SET Prog_ID='" + txtProgID.Text + "',Prog_Name='" + txtProgName.Text + "',Duration='" + txtDuration.Text + "',Department_ID='" + cboDepID.Text + "' WHERE Prog_ID='" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    if (MessageBox.Show("Are you sure you want to save changes?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                        cmd.ExecuteNonQuery();

                        cleanData();
                        MessageBox.Show("Changes Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Changes Failed to Save!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSearch.Focus();
                    }
                }

                cn.CloseConnection();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgID.Focus();
            }
            else if (txtProgName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProgName.Focus();
            }
            else if (txtDuration.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDuration.Focus();
            }
            else if (cboDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboDepID.Focus();
            }
            else
            {
                query = "DELETE FROM programme WHERE Prog_ID='" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    if (MessageBox.Show("Are you sure you want to delete Record?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                        cmd.ExecuteNonQuery();

                        cleanData();
                        MessageBox.Show("Records Deleted!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Records failed to delete!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSearch.Focus();
                    }
                }

                cn.CloseConnection();

            }
        }
    }
}
