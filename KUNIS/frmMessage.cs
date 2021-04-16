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
    public partial class frmMessage : Form
    {
        public string query;
        public frmMessage()
        {
            InitializeComponent();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            groupBox56.Left = (this.ClientSize.Width - groupBox56.Width) / 2;
            groupBox56.Top = (this.ClientSize.Height - groupBox56.Height) / 2;
            groupBox6.Visible = false;
            groupBox4.Visible = false;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFindRecipient_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox6.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = false;
        }

        private void btnCancel2_Click_1(object sender, EventArgs e)
        {
            groupBox6.Visible = false;
        }

        private void btnFindMessages_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = true;
            groupBox4.Visible = false;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtMessage.Text =="")
            {
                MessageBox.Show("Type message first!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMessage.Focus();
            }
            else if(txtSubject.Text == "")
            {
                MessageBox.Show("Type Subject First!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubject.Focus();
            }
            else if (cboCriteriaSearch.Text == "")
            {
                MessageBox.Show("Please select Criteria!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCriteriaSearch.Focus();
            }
            else if (txtSearch.Text == "")
            {
                MessageBox.Show("Type Search First!", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
            }
            else
            {
                query = "";
                if(cboCriteriaSearch.Text =="All")
                {
                    query = "SELECT * FROM student WHERE Status=1";
                }
                else if (cboCriteriaSearch.Text == "Programme")
                {
                    query = "SELECT * FROM student WHERE Status=1 and Prog_ID='"+ txtSearch.Text  +"'";
                }
                else if (cboCriteriaSearch.Text == "Reg No")
                {
                    query = "SELECT * FROM student WHERE Status=1 and Reg_No='" + txtSearch.Text + "'";
                }
                else if (cboCriteriaSearch.Text == "Department")
                {
                    query = "SELECT * FROM studentview WHERE Status=1 and Department_ID='" + txtSearch.Text + "'";
                }
                else if (cboCriteriaSearch.Text == "School")
                {
                    query = "SELECT * FROM studentview WHERE Status=1 and School_ID='" + txtSearch.Text + "'";
                }
                else
                {
                    MessageBox.Show("Invalid Criteria", "KUNIS Messaging System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboCriteriaSearch.Focus();
                }
                conn cn = new conn();
                if(cn.OpenConnection ()==true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    this.dataGridView1.Rows.Clear();
                    while(dataReader.Read())
                    {
                        string msg = "";
                        msg = "Dear " + dataReader["Student_Name"].ToString () + ",\n Subject: " + txtSubject.Text + " \n Message:" + txtMessage.Text;
                        string[] row = new string[] { dataReader["Phone_No"].ToString(),txtSubject.Text,msg,"Pending","--","--","SMS" };
                        dataGridView1.Rows.Add(row);
               
                    }
                }
                cn.CloseConnection();


            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = 0;
            while (ind < this.dataGridView1.Rows.Count)
            {
                


                    query = "INSERT INTO messages(Direction,Type,Status,ChannelID,ScheduledTimeSecs,Recipient,Body,Subject) VALUES(2,2,1,1001,1,'" + this.dataGridView1.Rows[ind].Cells[0].Value.ToString() + "','" + this.dataGridView1.Rows[ind].Cells[2].Value.ToString() + "','" + this.dataGridView1.Rows[ind].Cells[1].Value.ToString() + "')";
                    conn cn = new conn();
                    if (cn.OpenConnection() == true)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, cn.connect);
                        cmd.ExecuteNonQuery();
                    }
                    ind++;
                    cn.CloseConnection();
                
            }
            }

            catch (Exception ee)
            {

            }
            this.dataGridView1.Rows.Clear();
            MessageBox.Show("Messages Sent");

        }
    }
}
