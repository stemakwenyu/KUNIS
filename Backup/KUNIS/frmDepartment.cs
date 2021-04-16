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
    public partial class frmDepartment : Form
    {
        public string query;
        public frmDepartment()
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
            txtDepID.Text = "";
            txtDepName.Text = "";
            txtRoom.Text = "";
            txtDescription.Text = "";
            cboSchoolID.Text="";
            txtDepID.Focus();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            addSchool();
        }
        private void addSchool()
        {
            query = "SELECT * FROM school ORDER BY School_ID ASC";
            conn cn = new conn();
            if (cn.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                this.cboSchoolID.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["School_ID"].ToString().Replace(" ", "") != "")
                    {
                        this.cboSchoolID.Items.Add(dataReader["School_ID"].ToString());
                    }
                }

            }
        }
        private bool FindRecord(string schval)
        {
            query = "SELECT * FROM department WHERE Department_ID='" + schval + "'";
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
                query = "SELECT * FROM department WHERE Department_ID LIKE '" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        txtDepID.Text = dataReader["Department_ID"].ToString();
                        txtDepName.Text = dataReader["Department_Name"].ToString();
                        txtRoom.Text = dataReader["Room"].ToString();
                        txtDescription.Text = dataReader["Description"].ToString();
                        cboSchoolID.Text = dataReader["School_ID"].ToString();

                    }
                    else
                    {
                        txtDepID.Text = "";
                        txtDepName.Text = "";
                        txtRoom.Text = "";
                        txtDescription.Text = "";
                        cboSchoolID.Text = "";

                    }
                    
                }
                cn.CloseConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepID.Focus();
            }
            else if (txtDepName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepName.Focus();
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
            else if (cboSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchoolID.Focus();
            }
            else
            {
                if (FindRecord(txtDepID.Text) == true)
                {
                    MessageBox.Show(txtDepID.Text + " already exists!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDepID.Focus();
                }
                else
                {
                    query = "INSERT INTO department VALUES('" + txtDepID.Text + "','" + txtDepName.Text + "','" + txtRoom.Text + "','" + txtDescription.Text + "','"+ cboSchoolID.Text  +"')";
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
                            txtDepID.Focus();
                        }
                    }
                    cn.CloseConnection();
                }



            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepID.Focus();
            }
            else if (txtDepName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepName.Focus();
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
            else if (cboSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchoolID.Focus();
            }
            else
            {
                query = "UPDATE department SET Department_ID='" + txtDepID.Text + "',Department_Name='" + txtDepName.Text + "',Room='" + txtRoom.Text + "',Description='" + txtDescription.Text + "',School_ID='"+cboSchoolID.Text +"' WHERE Department_ID='" + txtSearch.Text + "'";
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
            if (txtDepID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepID.Focus();
            }
            else if (txtDepName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepName.Focus();
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
            else if (cboSchoolID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchoolID.Focus();
            }
            else
            {
                query = "DELETE FROM department WHERE Department_ID='" + txtSearch.Text + "'";
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
