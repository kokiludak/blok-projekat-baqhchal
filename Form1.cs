using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baqhchal
{
    public partial class Form1 : Form
    {
        const int tableSize = 5;
        
        Baqhchal tabla;
        Engine protivnik;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabla = new Baqhchal(tableSize);
            protivnik = new Engine(Int32.Parse(textUserDepth.Text), tabla, playerIsSheep.Checked);
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}
