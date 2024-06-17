using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum gameState
{
    Active,
    SheepWon,
    TigerWon
}

public enum piece
{
    Tiger,
    Sheep,
    Empty
}

namespace Baqhchal
{

    public class Move { 
        public int startx, starty;
        public int endx, endy;

        public Move(int startx, int starty, int endx, int endy)
        {
            this.startx = startx;
            this.starty = starty;
            this.endx = endx;
            this.endy = endy;
        }

        public bool isMoveCapture()
        {
            int xd = Math.Abs(endx - startx);
            int yd = Math.Abs(endy - starty);
            return Math.Sqrt((xd * xd) + (yd * yd)) >= 2f;
        }
    }

    public class Baqhchal
    {
        HashSet<int[,]> boardStates;
        private int tableSize = 5;
        private piece[,] board;


        private const int minSheep = 10;
        private int capturedSheep;
        private int numSheep;
        //private bool sheepTurn;

        public Baqhchal(int tableSize)
        {
            this.tableSize = tableSize;
            board = new piece[tableSize, tableSize];
            numSheep = 0;
            //sheepTurn = true;

            for(int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                    if (i == 0 && j == 0) board[i, j] = piece.Tiger;
                    else if (i == tableSize - 1 && j == 0) board[i, j] = piece.Tiger;
                    else if (i == 0 && j == tableSize - 1) board[i, j] = piece.Tiger;
                    else if (i == tableSize - 1 && j == tableSize - 1) board[i, j] = piece.Tiger;


                    else board[i, j] = piece.Empty;
                }
            }

        }

        

        public bool IsMoveLegal(Move move, bool sheepTurn)
        {
            //throw new NotImplementedException();

            //moves must be in bounds
            if (move.startx < 0 || move.startx >= tableSize) return false;
            if (move.endx < 0 || move.endx >= tableSize) return false;

            if(move.starty < 0 || move.starty >= tableSize) return false;
            if (move.endy < 0 || move.endy >= tableSize) return false;



            //cannot move empty pieces, cannot move ontop of other pieces
            if (board[move.startx, move.starty] != piece.Empty) return false;
            if (board[move.endx, move.endy] != piece.Empty) return false;

            //captures must occur over a sheep
            if (!sheepTurn && move.isMoveCapture())
            {
                if (board[(move.endx - move.startx) / 2, (move.endy - move.starty / 2)] != piece.Sheep) return false;
            }



            return true;
        }

        public void MakeMove(Move move, bool sheepTurn)
        {
            if (sheepTurn)
            {
                if(numSheep < minSheep)
                {
                    //place sheep;
                    board[move.startx, move.starty] = piece.Sheep;
                }
                else
                {
                    board[move.startx, move.starty] = piece.Empty;
                    board[move.endx, move.endy] = piece.Sheep;
                }
            }
            else
            {
                if (move.isMoveCapture())
                {
                    board[move.startx, move.starty] = piece.Empty;
                    board[(move.startx + move.endx) / 2, (move.starty + move.endy) / 2] = piece.Empty;
                    board[move.endx, move.endy] = piece.Tiger;
                }
                else
                {
                    board[move.startx, move.starty] = piece.Empty;
                    board[move.endx, move.endy] = piece.Tiger;
                }
            }

        }

        public void unMakeMove(Move move, bool sheepTurn)
        {
            throw new NotImplementedException();
        }

        public List<Move> GenerateMoves(bool sheepturn)
        {
            //throw new NotImplementedException();
            List<Move> legalMoves = new List<Move>();
            if (sheepturn)
            {
                for(int i = 0; i < tableSize; i++)
                {
                    for(int j = 0; j < tableSize; j++)
                    {

                    }
                }
            }
            else
            {

            }

            return legalMoves;
        }

        public gameState GameState()
        {
            if (capturedSheep > 5) return gameState.TigerWon;

            if (GenerateMoves(false).Count == 0) return gameState.SheepWon;

            return gameState.Active;
        }


    }
}
