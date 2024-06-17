using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baqhchal
{
    public class Baqhchal
    {
        private int tableSize = 5;
        private int[,] board;
        private int numSheep;
        private bool sheepTurn;

        Baqhchal(int tableSize)
        {
            this.tableSize = tableSize;
            board = new int[tableSize, tableSize];
            numSheep = 0;
            sheepTurn = true;
        }

        public void MakeMove(int startIndex, int endIndex)
        {
            throw new NotImplementedException();
        }

    }
}
