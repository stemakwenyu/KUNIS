using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUNIS
{
    public partial class MDI1 : Form
    {
        public MDI1()
        {
            InitializeComponent();
        }

        private void MDI1_Load(object sender, EventArgs e)
        {
            frmMain main = new frmMain();
            main.MdiParent = this;
            if(Sessions.prev =="Admin")
            {
                //userToolStripMenuItem.Visible = true;

            }
            else
            {
                //userToolStripMenuItem.Visible  = false;
            }
            main.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmlg = new frmLogin();
            frmlg.Visible = true;
            this.Dispose();
        }

        private void schoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSchool sch = new frmSchool();
            sch.MdiParent = this;
            sch.Visible = true;
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartment dep = new frmDepartment();
            dep.MdiParent = this;
            dep.Visible = true;
        }

        private void programmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProgramme prog = new frmProgramme();
            prog.MdiParent = this;
            prog.Visible = true;
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudent stud = new frmStudent();
            stud.MdiParent = this;
            stud.Visible = true;
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Sessions.prev =="Admin")
            {
                frmUser us = new frmUser();
                us.MdiParent = this;
                us.Visible = true;

            }
            else
            {
                MessageBox.Show("You dont have priviledge to access this service","KUNIS Messaging System",MessageBoxButtons.OK,MessageBoxIcon.Warning);


            }
            
        }

        private void messageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessage msg = new frmMessage();
            msg.MdiParent = this;
            msg.Visible = true;
        }
    }
}
