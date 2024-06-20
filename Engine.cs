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
                int curBestEval = int.MinValue;
                Baqhchal prev = new Baqhchal(mockBoard);

                foreach (Move move in mockBoard.GenerateMoves(sheepTurn))
                {
  
                    mockBoard.MakeMove(move);
                    int childValue = Search(depth_remaining - 1, false, depth + 1, alpha, beta);

                    //Console.WriteLine("Move: " + move + " value: " + childValue + " depth: " + depth);

                    

                    if (childValue > curBestEval && depth == 0)
                    {
                        bestMove = move;
                        bestEval = Math.Max(bestEval, childValue); // debug value
                    }


                    Console.WriteLine("before set num sheep " + mockBoard.capturedSheep);
                    curBestEval = Math.Max(curBestEval, childValue);
                    alpha = Math.Max(alpha, childValue);
                    mockBoard = new Baqhchal(prev);

                    Console.WriteLine("after sheep " + mockBoard.capturedSheep);


                    if (beta <= alpha)
                    {
                        break;
                    }

                }
                //Console.WriteLine("sheep return value: " + curBestEval);
                return curBestEval;
            }
            else
            {
                int curBestEval = int.MaxValue;
                Baqhchal prev = new Baqhchal(mockBoard);

                foreach(Move move in mockBoard.GenerateMoves(sheepTurn))
                {

                    mockBoard.MakeMove(move);
                    int childValue = Search(depth_remaining - 1, true, depth + 1, alpha, beta);

                    //Console.WriteLine("Move: " + move + " value: " + childValue + " depth: " + depth);

                    if (childValue < curBestEval && depth == 0)
                    {
                        bestMove = move;
                        bestEval = Math.Min(bestEval, childValue);


                    }

                    curBestEval = Math.Min(curBestEval, childValue);
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
            if (mockBoard.GameState() == gameState.SheepWon) return int.MaxValue;
            if (mockBoard.GameState() == gameState.TigerWon) return int.MinValue;

            //Console.WriteLine("eval " + -mockBoard.capturedSheep);

            return -mockBoard.capturedSheep;
        }

    }
}
