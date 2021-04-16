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
    public partial class frmUser : Form
    {
        public string query;
        public frmUser()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            AssignUserID();
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
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
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtPhoneNo.Text = "";
            txtLoginName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cboPriviledges.Text = "";
            AssignUserID();
            chckStatus.CheckState = CheckState.Unchecked;
            txtUserName.Focus();
        }
        private void AssignUserID()
        {
            conn cn = new conn();
            if (cn.OpenConnection() == true)
            {
                query = "SELECT * FROM user ORDER BY User_ID DESC";
                MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                //this.cboStaffNo.Items.Clear();
                int stffNo;
                if (dataReader.Read())
                {
                    if (dataReader["User_ID"].ToString().Replace(" ", "") != "")
                    {

                        stffNo = Convert.ToInt32((dataReader["User_ID"].ToString().Substring(4)));
                        if (stffNo < 9)
                        {
                            txtUserID.Text = "EMP/000" + (stffNo + 1);
                        }
                        else if (stffNo >= 9 && stffNo < 99)
                        {
                            txtUserID.Text = "EMP/00" + (stffNo + 1);
                        }
                        else if (stffNo >= 99 && stffNo < 999)
                        {
                            txtUserID.Text = "EMP/0" + (stffNo + 1);
                        }
                        else
                        {
                            txtUserID.Text = "EMP/" + (stffNo + 1);
                        }

                        //MessageBox.Show(txtStaffNo.Text);
                    }

                }
                else
                {
                    txtUserID.Text = "EMP/0001";
                }
                cn.CloseConnection();



            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                query = "SELECT * FROM user WHERE User_ID LIKE '" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        txtUserID.Text = dataReader["User_ID"].ToString();
                        txtUserName.Text = dataReader["User_Name"].ToString();
                        txtLoginName.Text = dataReader["Login_Name"].ToString();
                        txtPassword.Text = dataReader["Passsword"].ToString();
                        txtConfirmPassword.Text = dataReader["Passsword"].ToString();
                        cboPriviledges.Text = dataReader["Priviledges"].ToString();
                        txtPhoneNo.Text = dataReader["Phone_No"].ToString();
                        if (dataReader["Status"].ToString() == "True")
                        {
                            chckStatus.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            chckStatus.CheckState = CheckState.Unchecked;
                        }


                    }
                    else
                    {
                        txtUserID.Text = "";
                        txtUserName.Text = "";
                        txtLoginName.Text = "";
                        txtPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        cboPriviledges.Text = "";
                        txtPhoneNo.Text = "";
                        chckStatus.CheckState = CheckState.Unchecked;

                    }

                }
                cn.CloseConnection();
            }
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            long phone;
            string phn = txtPhoneNo.Text.Replace("+", "").Trim().ToString();
            if (txtUserID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
            }
            else if (txtLoginName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
            }
            else if (txtConfirmPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Password mismatch!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtPassword.Focus();
            }
            else if (cboPriviledges.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPriviledges.Focus();
            }
            else if (txtPhoneNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
            }
            else if (!long.TryParse(phn, out phone))
            {
                MessageBox.Show("Invalid Phone Number!", "Looptech International Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (!txtPhoneNo.Text.Contains("+"))
            {
                MessageBox.Show("Invalid Phone Number!", "Looptech International Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else
            {
                if (FindRecord(txtUserID.Text) == true)
                {
                    MessageBox.Show(txtUserID.Text + " already exists!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserID.Focus();
                }
                else
                {
                    int sts;
                    if (chckStatus.CheckState == CheckState.Checked)
                    {
                        sts = 1;
                    }
                    else
                    {
                        sts = 0;
                    }
                    query = "INSERT INTO user VALUES('" + txtUserID.Text + "','" + txtUserName.Text + "','" + txtLoginName.Text + "','" + txtPassword.Text + "','"+ sts +"','"+ cboPriviledges.Text +"','"+ txtPhoneNo.Text +"')";
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
                            txtUserID.Focus();
                        }
                    }
                    cn.CloseConnection();
                }



            }
        }
        private bool FindRecord(string schval)
        {
            query = "SELECT * FROM user WHERE User_ID='" + schval + "'";
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
            if (txtUserID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
            }
            else if (txtLoginName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
            }
            else if (txtConfirmPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Password mismatch!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtPassword.Focus();
            }
            else if (cboPriviledges.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPriviledges.Focus();
            }
            else if (txtPhoneNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
            }
            else
            {
                
                    int sts;
                    if (chckStatus.CheckState == CheckState.Checked)
                    {
                        sts = 1;
                    }
                    else
                    {
                        sts = 0;
                    }
                    query = "UPDATE user SET User_ID='" + txtUserID.Text + "',User_Name='" + txtUserName.Text + "',Login_Name='" + txtLoginName.Text + "',Passsword='" + txtPassword.Text + "',Status='" + sts + "',Priviledges='" + cboPriviledges.Text + "',Phone_No='" + txtPhoneNo.Text + "' WHERE User_ID='"+ txtSearch.Text  +"'";
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
                            MessageBox.Show("Changes not Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtUserID.Focus();
                        }
                    }
                    cn.CloseConnection();
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
            }
            else if (txtLoginName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
            }
            else if (txtConfirmPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Password mismatch!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtPassword.Focus();
            }
            else if (cboPriviledges.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPriviledges.Focus();
            }
            else if (txtPhoneNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
            }
            else
            {

                int sts;
                if (chckStatus.CheckState == CheckState.Checked)
                {
                    sts = 1;
                }
                else
                {
                    sts = 0;
                }
                query = "DELETE FROM user WHERE User_ID='" + txtSearch.Text + "'";
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
                        MessageBox.Show("Record not deleted!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtUserID.Focus();
                    }
                }
                cn.CloseConnection();

            }
        }
    }
}
