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
    public partial class frmSplash : Form
    {
        public int counter;
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter += 20;
            if (counter >= 100)
            {
                progressBar1.Value = 100;
                lblProgress.Text = "Progress..100%";
            }
            else
            {
                progressBar1.Value = counter;
                lblProgress.Text ="Progress.." + counter + "%";
            }
            if(counter==100)
            {
                frmLogin lg=new frmLogin();
                lg.Visible =true;
                this.Hide();
            }

        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            counter = 0;
        }
    }
}
