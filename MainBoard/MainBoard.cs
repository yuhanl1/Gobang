using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManMachine;

namespace MainBoard
{
    public partial class MainBoard : Form
    {
        public MainBoard()
        {
            InitializeComponent();
        }

        private void GoBang_Click(object sender, EventArgs e)
        {

        }

        private void button人机对弈_Click(object sender, EventArgs e)
        {
            ManMachine.ManMachine f = new ManMachine.ManMachine();
            f.Show();
        }
    }
}
