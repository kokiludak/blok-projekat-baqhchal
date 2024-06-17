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
        public bool capture;
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
                if (move.capture)
                {
                    
                }
                else
                {

                }
            }

        }

        public void unMakeMove(Move move, bool sheepTurn)
        {

        }

        public List<Move> GenerateMoves(bool sheepturn)
        {
            throw new NotImplementedException();
        }

        public gameState GameState()
        {
            if (capturedSheep > 5) return gameState.TigerWon;

            if (GenerateMoves(false).Count == 0) return gameState.SheepWon;

            return gameState.Active;
        }


    }
}
