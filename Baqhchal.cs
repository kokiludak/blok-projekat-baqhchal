using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum gameState
{
    Active,
    SheepWon,
    TigerWon
}

namespace Baqhchal
{

    public class Move { 
        int startx, starty;
        int endx, endy;
    
    }

    public class Baqhchal
    { 

        private int tableSize = 5;
        private int[,] board;

        private int capturedSheep;
        private int numSheep;
        //private bool sheepTurn;

        public Baqhchal(int tableSize)
        {
            this.tableSize = tableSize;
            board = new int[tableSize, tableSize];
            numSheep = 0;
            //sheepTurn = true;
        }




        public void MakeMove(Move move, bool sheepTurn)
        {
            if (sheepTurn)
            {

            }
            else
            {

            }

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
