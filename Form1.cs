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


        bool curSheepTurn;
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
            curSheepTurn = playerIsSheep.Checked;
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
            SolidBrush tiger = new SolidBrush(Color.FromArgb(212, 173, 2));
            SolidBrush sheep = new SolidBrush(Color.FromArgb(211, 228, 235));
            for(int i = 0; i < tableSize - 1; i++)
            {
                for(int j = 0; j < tableSize - 1; j++)
                {
                    g.DrawRectangle(black, new Rectangle(margina + j * cellSize, margina + i * cellSize, cellSize, cellSize));

                   

                    //dijagonala
                    if ((i + j) % 2 == 0)
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


            //draw on points
            for(int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                    if (tabla.board[i, j] == piece.Tiger)
                    {
                        //g.FillCircle(tiger, margina + j * cellSize - cellSize / 2, margina + i * cellSize - cellSize / 2, cellSize, cellSize);

                        g.FillEllipse(tiger, margina + j * cellSize - cellSize / 4, margina + i * cellSize - cellSize / 4, cellSize / 2, cellSize / 2);
                    }
                    else if (tabla.board[i, j] == piece.Sheep)
                    {
                        g.FillEllipse(sheep, margina + j * cellSize - cellSize / 4, margina + i * cellSize - cellSize / 4, cellSize / 2, cellSize / 2);
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
            double y = (double)(e.X - margina) / cellSize;
            double x = (double)(e.Y - margina) / cellSize;
            double xr = Math.Round(x);
            double yr = Math.Round(y);

            Console.WriteLine("niggers: " + x + " " + y + " " + xr + " " + yr);
            Console.WriteLine("jiggers: " + (Math.Abs(yr - y) + Math.Abs(xr - x)));

            if(Math.Abs(yr - y) + Math.Abs(xr - x) < 40 && (tabla.board[(int)xr, (int)yr] != piece.Empty || (curSheepTurn && tabla.numSheep < tabla.MinSheep)))
            {
                Console.WriteLine("gigger nigger");
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
                if(curSheepTurn && tabla.numSheep < tabla.MinSheep)
                {
                    PerformMove(xr, yr);
                }
            }
            else
            {
                PerformMove(xr, yr);
            }
        }
        private void PerformMove(int xr, int yr)
        {
            x2 = xr;
            y2 = yr;
            List<Move> allLegalMoves = tabla.GenerateMoves(playerIsSheep.Checked);
            Move myMove = new Move(x1, y1, x2, y2);
            if (allLegalMoves.Contains(myMove))
            {
                Console.WriteLine("gigganigga");
                tabla.MakeMove(myMove, playerIsSheep.Checked);
            }
                firstSel = false;
        }
    }
}
