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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cleanData();
        }
        private void cleanData()
        {
            txtLoginName.Text = "";
            txtPassword.Text = "";
            txtLoginName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLoginName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLoginName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
            }
            else
            {
                conn connect = new conn();
                string query = "SELECT * FROM  user WHERE Login_Name='" + txtLoginName.Text.ToString() + "' AND Passsword='" + txtPassword.Text.ToString() + "' AND Status=1";

                //open connection
                if (connect.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connect.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    if (dataReader.Read())
                    {
                        //list[0].Add(dataReader["id"] + "");
                        //list[1].Add(dataReader["name"] + "");
                        if (dataReader["Login_Name"].ToString() == txtLoginName.Text && dataReader["Passsword"].ToString() == txtPassword.Text)
                        {
                            Sessions.username = dataReader["User_Name"].ToString();
                            Sessions.prev = dataReader["Priviledges"].ToString();
                            Sessions.userID = dataReader["User_ID"].ToString();
                            Sessions.loginTime = DateTime.Now.ToString();
                            MDI1 mdi = new MDI1();
                            mdi.Visible = true;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username/Password Mismatch!\nPlease Try again!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtLoginName.Text = "";
                            txtPassword.Text = "";
                            txtLoginName.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username/Password Mismatch!\nPlease Try again!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtLoginName.Text = "";
                        txtPassword.Text = "";
                        txtLoginName.Focus();
                    }

                    //close Data Reader
                    dataReader.Close();


                    //close connection
                    connect.CloseConnection();
                }
            }

        }
    }
}
