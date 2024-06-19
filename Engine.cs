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

    public class Node
    {
        public List<Node> Children;
        public int data;

        public Node(List<Node> children, int data)
        {
            Children = children;
            this.data = data;
        }
    }
    public class Engine
    {
        public int depth;
        Move bestMove = null; //ne mora null al aj
        int bestEval = 0;
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
            int curEval = Search(depth, sheepTurn);

            if(bestMove == null)
            {
                throw new Exception("no move found");
            }

            Console.WriteLine("BEST MOVE: " +  bestMove);
            Console.WriteLine("best eval: " + bestEval);
            return bestMove;
        }

        public int Search(int depth_remaining, bool sheepTurn, int depth = 0, int alpha = Int32.MinValue, int beta = Int32.MaxValue)
        {
            if(mockBoard.GameState() != gameState.Active || depth_remaining == 0)
            {
                return Eval();
            }

            if (sheepTurn)
            {
                int curBestEval = -74928472;
                Baqhchal prev = new Baqhchal(mockBoard);

                foreach (Move move in mockBoard.GenerateMoves(sheepTurn))
                {
                    //Console.Write("evaluate move: " + move + " at depth: " + depth);
                    //mockBoard = new Baqhchal(prev);
                    mockBoard.MakeMove(move);
                    int value = Search(depth_remaining - 1, false, depth + 1, alpha, beta);
                    //Console.WriteLine(" value: " + value);
                    if(value > curBestEval && depth == 0)
                    {
                        bestMove = move;
                        bestEval = Math.Max(bestEval, value);
                    }
                    
                    curBestEval = Math.Max(curBestEval, value);
                    alpha = Math.Max(alpha, value);
                    mockBoard = new Baqhchal(prev);


                    if(beta <= alpha)
                    {
                        //Console.WriteLine("maximizer prune");
                        break;
                    }

                }
                return curBestEval;
            }
            else
            {
                int curBestEval = 134214214;
                Baqhchal prev = new Baqhchal(mockBoard);
                //Console.WriteLine("SEARCH ITERATION DEPTH: " + depth);

                foreach(Move move in mockBoard.GenerateMoves(sheepTurn))
                {
                    //Console.Write("evaluate move: " + move + " at depth: " + depth);
                    //mockBoard = new Baqhchal(prev);
                    mockBoard.MakeMove(move);
                    int value = Search(depth_remaining - 1, true, depth + 1, alpha, beta);
                    //Console.WriteLine(" value: " + value);



                    if (depth == 0) Console.WriteLine("value: " + value);
                    if (value < curBestEval && depth == 0)
                    {
                        Console.WriteLine("new best move" + move + value);
                        bestMove = move;
                        bestEval = Math.Min(bestEval, value);
                    }

                    curBestEval = Math.Min(curBestEval, value);
                    beta = Math.Min(beta, curBestEval);
                    mockBoard = new Baqhchal(prev);
                    if(beta <= alpha)
                    {
                        //Console.WriteLine("minimizer prune");
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
            if (mockBoard.GameState() == gameState.SheepWon) return winvl;
            if (mockBoard.GameState() == gameState.TigerWon) return -winvl;

            

            return -mockBoard.capturedSheep;
        }

    }
}
