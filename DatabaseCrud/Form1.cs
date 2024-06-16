using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DatabaseCrud
{
    public partial class Form1 : Form
    {
        int timeLeft;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {

                timeLeft = timeLeft - 1;

            }

            else

            {

                timer1.Stop();

                new Form2().Show();

                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timeLeft = 10;
            timer1.Start();
        }
    }
}
