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

        const int margina = 50;
        const int cellSize = 100;

        bool firstSel = false;
        int x1, y1;
        int x2, y2;


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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // margine 50px
            //
            Graphics g = e.Graphics;
            Pen black = new Pen(Color.Black);
            Pen red = new Pen(Color.Red);
            for(int i = 0; i < tableSize - 1; i++)
            {
                for(int j = 0; j < tableSize - 1; j++)
                {
                    g.DrawRectangle(black, new Rectangle(margina + j * cellSize, margina + i * cellSize, cellSize, cellSize));

                    //dijagonala
                    if((i + j) % 2 == 0)
                    {
                        //gore levo > dole desno
                        g.DrawLine(black, new Point(margina + (j * cellSize), margina + (i * cellSize)), new Point(margina + ((j + 1) * cellSize), margina + ((i + 1) * cellSize)));
                    }
                    else
                    {
                        //dole levo > gore desno
                        g.DrawLine(black, margina + j * cellSize, margina + (i + 1) * cellSize, margina + (j + 1) * cellSize, margina + i * cellSize);
                    }
                }
            }


            //draw Selection
            if (firstSel)
            {
                int xx = margina + (x1 * cellSize);
                int xy = margina + (y1 * cellSize);

                g.DrawRectangle(red, xx - cellSize / 2, xy - cellSize / 2, cellSize, cellSize);
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            double x = (double)(e.X - margina) / cellSize;
            double y = (double)(e.Y - margina) / cellSize;
            double xr = Math.Round(x);
            double yr = Math.Round(y);
            if(Math.Abs(yr - y) + Math.Abs(xr - x) < 20 && tabla.board[(int)xr, (int)yr] != piece.Empty)
            {
                //kliknuo polje
                PointSelected((int)xr, (int)yr);
            }

            Invalidate();
        }

        private void PointSelected(int xr, int yr)
        {
            if (!firstSel)
            {
                x1 = xr;
                y1 = yr;
                firstSel = true;
            }
            else
            {
                firstSel = false;
                x2 = xr;
                y2 = yr;
                List<Move> allLegalMoves = tabla.GenerateMoves(playerIsSheep.Checked);
                Move myMove = new Move(x1, y1, x2, y2);
                if (allLegalMoves.Contains(myMove)) tabla.MakeMove(myMove, playerIsSheep.Checked);
            }
        }
    }
}
