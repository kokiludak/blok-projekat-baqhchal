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
        Engine opponent;

        Move bestMove;

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
            opponent = new Engine(Int32.Parse(textUserDepth.Text), tabla);
            curSheepTurn = playerIsSheep.Checked;
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            tabla = new Baqhchal(tableSize);
            opponent = new Engine(Int32.Parse(textUserDepth.Text), tabla);
            curSheepTurn = playerIsSheep.Checked;
            firstSel = false;

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen black = new Pen(Color.Black);
            Pen red = new Pen(Color.Red);
            Pen blue = new Pen(Color.Blue);
            Pen orange = new Pen(Color.Orange);
            Pen endMove = new Pen(Color.FromArgb(5, 227, 64));

            SolidBrush tiger = new SolidBrush(Color.FromArgb(212, 173, 2));
            SolidBrush sheep = new SolidBrush(Color.FromArgb(211, 228, 235));

            labelFirstSel.Text = firstSel.ToString();

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

                g.DrawRectangle(red, xy - cellSize / 2, xx - cellSize / 2, cellSize, cellSize);
            }

            //draw legal moves
            if (checkBoxDrawLegalMoves.Checked)
            {
                List<Move> legalMoves = tabla.GenerateMoves(curSheepTurn);
                labelCountLegalMoves.Text = "Legal Moves: " + legalMoves.Count.ToString();
                foreach(Move move in legalMoves)
                {
                    g.DrawEllipse(blue, move.starty * cellSize + margina - cellSize / 2, move.startx * cellSize + margina - cellSize / 2, cellSize, cellSize);
                    g.DrawEllipse(endMove, move.endy * cellSize + margina - cellSize / 2, move.endx * cellSize + margina - cellSize / 2, cellSize, cellSize);
                }
            }


            //draw opponent move
            if (checkBoxOpponentDraw.Checked && bestMove != null)
            {
                //Move move = opponent.GenerateBestMove(curSheepTurn);
                g.DrawEllipse(red, bestMove.starty * cellSize + margina - cellSize / 2, bestMove.startx * cellSize + margina - cellSize / 2, cellSize, cellSize);
                g.DrawEllipse(orange, bestMove.endy * cellSize + margina - cellSize / 2, bestMove.endx * cellSize + margina - cellSize / 2, cellSize, cellSize);
                bestMove = null;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            double y = (double)(e.X - margina) / cellSize;
            double x = (double)(e.Y - margina) / cellSize;
            double xr = Math.Round(x);
            double yr = Math.Round(y);

            //Console.WriteLine("points: " + x + " " + y + " " + xr + " " + yr);

            if(Math.Abs(yr - y) + Math.Abs(xr - x) < 0.5f && (xr >= 0 && xr < tableSize && yr >= 0 && yr < tableSize))
            {
                //Console.WriteLine("valid point");
                if (firstSel)
                {
                    if(tabla.board[(int)xr, (int)yr] == piece.Empty) PointSelected((int)xr, (int)yr);
                }
                else
                {
                    if (curSheepTurn && tabla.numSheep < tabla.MinSheep && tabla.board[(int)xr, (int)yr] == piece.Empty)
                    {
                        PointSelected((int)xr, (int)yr);
                    }
                    else if (tabla.board[(int)xr, (int)yr] != piece.Empty) PointSelected((int)xr, (int)yr);
                    else
                    {
                        firstSel = false;
                    }
                }
            }

            Invalidate();
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            //tabla.unMakeMove(new Move(x1, y1, x2, y2), curSheepTurn);
            //Console.WriteLine("undo");
            tabla.undoMove();
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
                   // Console.WriteLine("point selected: place sheep");
                    PerformMove(xr, yr);
                }
            }
            else
            {
                //Console.WriteLine("point selected: movement");
                PerformMove(xr, yr);
            }
        }

        private void checkBoxDrawLegalMoves_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void checkBoxOpponentDraw_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void buttonBestMove_Click(object sender, EventArgs e)
        {
            bestMove = opponent.GenerateBestMove(curSheepTurn);
            Invalidate();
        }

        private void buttonEngineMove_Click(object sender, EventArgs e)
        {
            EngineMove();
            Invalidate();
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            tabla = Baqhchal.grabRotation(tabla, 1);
            opponent.baqhchal = tabla;
            Invalidate();
        }

        private void buttonPrintBoard_Click(object sender, EventArgs e)
        {
            Console.WriteLine(tabla.ToString());

        }

        private void PerformMove(int xr, int yr)
        {
            x2 = xr;
            y2 = yr;
            List<Move> allLegalMoves = tabla.GenerateMoves(curSheepTurn);
            //foreach(Move m in allLegalMoves) Console.WriteLine(m);


            Move myMove = new Move(x1, y1, x2, y2, curSheepTurn);
            Console.WriteLine("izabrani: " + myMove);

            if (allLegalMoves.Contains(myMove))
            {
                Console.WriteLine("selected move is legal, piece type: " + tabla.board[myMove.startx, myMove.starty]);
                tabla.MakeMove(myMove);
                curSheepTurn = !curSheepTurn;


                if(checkBoxAutoPlay.Checked) EngineMove();
            }
            else
            {
                Console.WriteLine("selected move is not legal");
            }
            firstSel = false;
            Invalidate();
            if (tabla.GameState() != gameState.Active)
            {
                MessageBox.Show("neko je pukao nzm ko");
                tabla = new Baqhchal(tableSize);
                opponent = new Engine(Int32.Parse(textUserDepth.Text), tabla);
                curSheepTurn = playerIsSheep.Checked;
                firstSel = false;

                Invalidate();
            }

            

            
           
        }

        public void EngineMove()
        {
            Console.WriteLine("asking engine for move: " + (curSheepTurn ? "Sheep" : "Tiger"));
            tabla.MakeMove(opponent.GenerateBestMove(curSheepTurn));
            curSheepTurn = !curSheepTurn;
        }
    }
}
