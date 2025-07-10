using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPDominos
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.Load += new EventHandler(Home_Load);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Home_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            timer1.Interval = 100;  
            timer1.Start();
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }
       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum) 
            {
                progressBar1.Value += 5;
                label3.Text = progressBar1.Value.ToString() + "%";
            }
            else
            {
                timer1.Stop();
                Adminlog newForm = new Adminlog();  
                newForm.Show();

                
                this.Hide();  
            }
        }
    }
}
