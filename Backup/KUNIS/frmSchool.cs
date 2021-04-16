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
    public partial class frmSchool : Form
    {
        public string query;
        public frmSchool()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cleanData();
        }
        private void cleanData()
        {
            txtSchoolID.Text = "";
            txtSchoolName.Text = "";
            txtRoom.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            txtSchoolID.Focus();
        }

        private void frmSchool_Load(object sender, EventArgs e)
        {
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolID.Focus();
            }
            else if (txtSchoolName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolName.Focus();
            }
            else if (txtRoom.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoom.Focus();
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
            }
            else
            {
                if (FindRecord(txtSchoolID.Text) == true)
                {
                    MessageBox.Show( txtSchoolID.Text + " already exists!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSchoolID.Focus();
                }
                else
                {
                    query = "INSERT INTO school VALUES('"+txtSchoolID.Text +"','"+ txtSchoolName.Text +"','"+ txtRoom.Text +"','"+ txtDescription.Text +"')";
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
                            txtSchoolID.Focus();
                        }
                    }
                    cn.CloseConnection();
                }
                
                

            }
        }
        private bool FindRecord(string schval)
        {
            query = "SELECT * FROM school WHERE School_ID='" + schval + "'";
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                query = "SELECT * FROM school WHERE School_ID LIKE '"+ txtSearch.Text +"'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        txtSchoolID.Text =dataReader ["School_ID"].ToString ();
                        txtSchoolName.Text = dataReader["School_Name"].ToString();
                        txtRoom.Text = dataReader["Room"].ToString();
                        txtDescription.Text = dataReader["Description"].ToString();

                    }
                    else
                    {
                        txtSchoolID.Text = "";
                        txtSchoolName.Text = "";
                        txtRoom.Text = "";
                        txtDescription.Text = "";

                    }

                }
                cn.CloseConnection();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolID.Focus();
            }
            else if (txtSchoolName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolName.Focus();
            }
            else if (txtRoom.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoom.Focus();
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
            }
            else if (txtSearch.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
            }
            else
            {
                query = "UPDATE school SET School_ID='" + txtSchoolID.Text + "',School_Name='" + txtSchoolName.Text + "',Room='" + txtRoom.Text + "',Description='" + txtDescription.Text + "' WHERE School_ID='"+ txtSearch.Text  +"'";
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
            if (txtSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolID.Focus();
            }
            else if (txtSchoolName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSchoolName.Focus();
            }
            else if (txtRoom.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoom.Focus();
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
            }
            else if (txtSearch.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
            }
            else
            {
                query = "DELETE FROM school WHERE School_ID='" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    if (MessageBox.Show("Are you sure you want to delete record?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                        cmd.ExecuteNonQuery();

                        cleanData();
                        MessageBox.Show("Record deleted!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Record failed to delete!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSearch.Focus();
                    }
                }

                cn.CloseConnection();

            }
        }
    }
}
