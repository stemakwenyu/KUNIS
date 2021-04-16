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
    public partial class frmStudent : Form
    {
        public string query;
        public frmStudent()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            addProgram();
        }
        private void addProgram()
        {
            query = "SELECT * FROM programme ORDER BY Prog_ID ASC";
            conn cn = new conn();
            if (cn.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                this.cboProgID.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Prog_ID"].ToString().Replace(" ", "") != "")
                    {
                        this.cboProgID.Items.Add(dataReader["Prog_ID"].ToString());
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
            txtRegNo.Text = "";
            txtStudName.Text = "";
            txtPhoneNo.Text = "";
            dtPicDoB.Value = DateTime.Now;
            rbtnMale.Checked = true;
            rbtnFemale.Checked = false;
            txtEmail.Text = "";
            txtPostalAddress.Text = "";
            txtPic.Text = "";
            pictureBox1.Image = null;
            chckStatus.CheckState = CheckState.Checked;
            cboProgID.Text = "";
            txtRegNo.Focus();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                query = "SELECT * FROM student WHERE Reg_No LIKE '" + txtSearch.Text + "'";
                conn cn = new conn();
                if (cn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        txtRegNo.Text = dataReader["Reg_No"].ToString();
                        txtStudName.Text = dataReader["Student_Name"].ToString();
                        dtPicDoB.Value   = Convert.ToDateTime(dataReader["DoB"].ToString());
                        if (dataReader["Gender"].ToString()== "Male")
                        {
                            rbtnMale.Checked = true;
                        }
                        else
                        {
                            rbtnFemale.Checked = true;
                        }
                        txtEmail.Text = dataReader["Email_Address"].ToString();
                        txtPostalAddress.Text = dataReader["Postal_Address"].ToString();
                        txtPhoneNo.Text = dataReader["Phone_No"].ToString();
                        txtPic.Text = dataReader["Photo"].ToString().Replace("__","\\");
                        if (txtPic.Text != "")
                        {
                            pictureBox1.Image = Image.FromFile(txtPic.Text);
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }
                        if (dataReader["Status"].ToString() == "True")
                        {
                            chckStatus.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            chckStatus.CheckState = CheckState.Unchecked;
                        }
                        cboProgID.Text = dataReader["Prog_ID"].ToString();


                    }
                    else
                    {
                        txtRegNo.Text = "";
                        txtStudName.Text = "";
                        txtPhoneNo.Text = "";
                        dtPicDoB.Value = DateTime.Now;
                        rbtnMale.Checked = true;
                        rbtnFemale.Checked = false;
                        txtEmail.Text = "";
                        txtPostalAddress.Text = "";
                        txtPic.Text = "";
                        pictureBox1.Image = null;
                        chckStatus.CheckState = CheckState.Checked;
                        cboProgID.Text = "";

                    }

                }
                cn.CloseConnection();
            }
        }
        private bool FindRecord(string schval)
        {
            query = "SELECT * FROM student WHERE Reg_No='" + schval + "'";
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            long phone;
            string phn = txtPhoneNo.Text.Replace("+", "").Trim().ToString();
            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegNo.Focus();
            }
            else if (txtStudName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudName.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
            }
            else if (!IsValidEmail(txtEmail.Text.Replace(" ", "").ToString()))
            {
                MessageBox.Show("Invalid Email", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (txtPostalAddress.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPostalAddress.Focus();
            }
            else if (cboProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboProgID.Focus();
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
                if (FindRecord(txtRegNo.Text) == true)
                {
                    MessageBox.Show(txtRegNo.Text + " already exists!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRegNo.Focus();
                }
                else
                {
                    int sts;
                    string gnder;
                    if (chckStatus.CheckState == CheckState.Checked)
                    {
                        sts = 1;
                    }
                    else
                    {
                        sts = 0;
                    }
                    if (rbtnFemale.Checked == true)
                    {
                        gnder = "Female";
                    }
                    else
                    {
                        gnder = "Male";
                    }
                    string pic;
                    pic=txtPic.Text.Replace ("\\","__");
                    query = "INSERT INTO student VALUES('" + txtRegNo.Text + "','" + txtStudName.Text + "','" + txtPhoneNo.Text + "','" + dtPicDoB.Value.ToString("yyyy-MM-dd") + "','" + gnder + "','" + txtEmail.Text + "','" + txtPostalAddress.Text + "','" + pic + "','" + sts + "','" + cboProgID.Text + "')";
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
                            txtRegNo.Focus();
                        }
                    }
                    cn.CloseConnection();
                }



            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            long phone;
            string phn = txtPhoneNo.Text.Replace("+", "").Trim().ToString();
            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegNo.Focus();
            }
            else if (txtStudName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudName.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
            }
            else if (!IsValidEmail(txtEmail.Text.Replace(" ", "").ToString()))
            {
                MessageBox.Show("Invalid Email", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (txtPostalAddress.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPostalAddress.Focus();
            }
            else if (cboProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboProgID.Focus();
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
                
                    int sts;
                    string gnder;
                    if (chckStatus.CheckState == CheckState.Checked)
                    {
                        sts = 1;
                    }
                    else
                    {
                        sts = 0;
                    }
                    if (rbtnFemale.Checked == true)
                    {
                        gnder = "Female";
                    }
                    else
                    {
                        gnder = "Male";
                    }
                    string pic;
                    pic = txtPic.Text.Replace("\\", "__");
                    query = "UPDATE student SET Reg_No='" + txtRegNo.Text + "',Student_Name='" + txtStudName.Text + "',Phone_No='" + txtPhoneNo.Text + "',DoB='" + dtPicDoB.Value.ToString("yyyy-MM-dd") + "',Gender='" + gnder + "',Email_Address='" + txtEmail.Text + "',Postal_Address='" + txtPostalAddress.Text + "',Status='" + sts + "',Prog_ID='" + cboProgID.Text + "',Photo='"+ pic +"' WHERE Reg_No='" + txtSearch.Text + "'";
                    conn cn = new conn();
                    if (cn.OpenConnection() == true)
                    {
                        if (MessageBox.Show("Are you sure you want to save changes?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                            cmd.ExecuteNonQuery();

                            cleanData();
                            MessageBox.Show("Records Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Records not Saved!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtRegNo.Focus();
                        }
                    }
                    cn.CloseConnection();
                



            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            long phone;
            string phn = txtPhoneNo.Text.Replace("+", "").Trim().ToString();
            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegNo.Focus();
            }
            else if (txtStudName.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudName.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
            }
            else if (!IsValidEmail(txtEmail.Text.Replace(" ", "").ToString()))
            {
                MessageBox.Show("Invalid Email", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (txtPostalAddress.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPostalAddress.Focus();
            }
            else if (cboProgID.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboProgID.Focus();
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
                
                    int sts;
                    string gnder;
                    if (chckStatus.CheckState == CheckState.Checked)
                    {
                        sts = 1;
                    }
                    else
                    {
                        sts = 0;
                    }
                    if (rbtnFemale.Checked == true)
                    {
                        gnder = "Female";
                    }
                    else
                    {
                        gnder = "Male";
                    }
                    query = "DELETE FROM student WHERE Reg_No='" + txtSearch.Text + "'";
                    conn cn = new conn();
                    if (cn.OpenConnection() == true)
                    {
                        if (MessageBox.Show("Are you sure you want to delete record?", "KUNIS Messaging System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                            cmd.ExecuteNonQuery();

                            cleanData();
                            MessageBox.Show("Records Deleted!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Records not Deleted!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtRegNo.Focus();
                        }
                    }
                    cn.CloseConnection();
                



            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //open file dialog
            OpenFileDialog open = new OpenFileDialog();
            //image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //display image in picture box
                pictureBox1.Image = new Bitmap(open.FileName);
                //image file path
                txtPic.Text = open.FileName;
            }
        }


       

        
    }
}
