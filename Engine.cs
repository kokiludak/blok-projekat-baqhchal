using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baqhchal
{
    //TODO
    /*
     * 
     * 
     * 
     */
    //
    public class Engine
    {
        public int depth;
        Move bestMove = null; //ne mora null al aj
        int bestEval = Int32.MinValue;
        bool foundMove = false;

        Baqhchal baqhchal;
        Baqhchal mockBoard;
        public Engine(int depth, Baqhchal baqhchal)
        {
            this.depth = depth;
            this.baqhchal = baqhchal;
        }


        public Move GenerateBestMove(bool sheepTurn)
        {
            mockBoard = new Baqhchal(baqhchal);
            foundMove = false;
            bestEval = Search(depth, sheepTurn);

            if(bestMove == null)
            {
                throw new Exception("no move found");
            }
            return bestMove;
        }

        public int Search(int depth_remaining, bool sheepTurn, int depth = 0, int alpha = Int32.MinValue, int beta = Int32.MaxValue)
        {
            if(baqhchal.GameState() != gameState.Active || depth_remaining == 0)
            {
                return Eval();
            }

            if (sheepTurn)
            {
                int curBestEval = Int32.MinValue;
                Baqhchal prev = new Baqhchal(mockBoard);

                foreach (Move move in prev.GenerateMoves(sheepTurn))
                {
                    mockBoard = new Baqhchal(prev);
                    mockBoard.MakeMove(move);
                    int value = Search(depth_remaining - 1, !sheepTurn, depth + 1, alpha, beta);
                    //baqhchal.undoMove();

                    if(value > curBestEval && depth == 0)
                    {
                        bestMove = move;
                    }
                    bestEval = Math.Max(bestEval, value);
                    curBestEval = Math.Max(curBestEval, value);
                    alpha = Math.Max(alpha, value);
                    mockBoard = new Baqhchal(prev);


                    if(beta <= alpha)
                    {
                        break;
                    }

                }
                return curBestEval;
            }
            else
            {
                int curBestEval = Int32.MaxValue;
                Baqhchal prev = new Baqhchal(baqhchal);

                foreach(Move move in prev.GenerateMoves(sheepTurn))
                {
                    mockBoard = new Baqhchal(prev);
                    mockBoard.MakeMove(move);
                    int value = Search(depth_remaining - 1, !sheepTurn, depth + 1, alpha, beta);
                    //baqhchal.undoMove();


                    if (value < curBestEval && depth == 0)
                    {
                        bestMove = move;
                    }

                    curBestEval = Math.Min(curBestEval, value);
                    beta = Math.Min(beta, curBestEval);
                    mockBoard = new Baqhchal(prev);
                    if(beta <= alpha)
                    {
                        break;
                    }
                }
                return curBestEval;
            }


        }


        public int Eval()
        {
            int winvl = Int32.MaxValue;
            //if (sheep) winvl = Int32.MaxValue;
            //else winvl = Int32.MinValue;
            if (baqhchal.GameState() == gameState.SheepWon) return winvl;
            if (baqhchal.GameState() == gameState.TigerWon) return -winvl;

            

            return false ? -baqhchal.capturedSheep : baqhchal.capturedSheep;
        }

    }
}
