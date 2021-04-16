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
            frmUser us = new frmUser();
            us.MdiParent = this;
            us.Visible = true;
        }

        private void messageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessage msg = new frmMessage();
            msg.MdiParent = this;
            msg.Visible = true;
        }
    }
}
