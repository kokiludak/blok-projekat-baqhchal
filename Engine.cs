using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int bestEval = 0;
        bool foundMove = false;
        int evaluatedPositions;
        Baqhchal baqhchal;
        Baqhchal mockBoard;

        Dictionary<Baqhchal, int> transpositionTable;
        public Engine(int depth, Baqhchal baqhchal)
        {
            this.depth = depth;
            this.baqhchal = baqhchal;
            transpositionTable = new Dictionary<Baqhchal, int>();
        }


        public Move GenerateBestMove(bool sheepTurn)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            mockBoard = new Baqhchal(baqhchal);

            evaluatedPositions = 0;

            int curEval = Search(depth, sheepTurn);

            if(bestMove == null)
            {
                throw new Exception("no move found");
            }

            watch.Stop();
            Console.WriteLine("BEST MOVE: " +  bestMove);
            Console.WriteLine("best eval: " + bestEval);
            Console.WriteLine("evaluated positions: " + evaluatedPositions);
            Console.WriteLine("Time elapsed: " + watch.ElapsedMilliseconds + "ms");
            return bestMove;
        }

        public int Search(int depth_remaining, bool sheepTurn, int depth = 0, int alpha = Int32.MinValue, int beta = Int32.MaxValue)
        {

            if (transpositionTable.ContainsKey(mockBoard))
            {
                return transpositionTable[mockBoard];
            }

            evaluatedPositions++;
            if(mockBoard.GameState() != gameState.Active || depth_remaining == 0)
            {
               
                return Eval();
            }

            if (sheepTurn)
            {
                int curBestEval = int.MinValue;
                //Baqhchal prev = new Baqhchal(mockBoard);

                foreach (Move move in mockBoard.GenerateMoves(sheepTurn))
                {
  
                    mockBoard.MakeMove(move);
                    int childValue = Search(depth_remaining - 1, false, depth + 1, alpha, beta);
                    mockBoard.undoMove();

                    

                    if (childValue > curBestEval && depth == 0)
                    {
                        bestMove = move;
                        bestEval = Math.Max(bestEval, childValue); // debug value
                    }


                  
                    curBestEval = Math.Max(curBestEval, childValue);
                    alpha = Math.Max(alpha, childValue);
                    //mockBoard = new Baqhchal(prev);

                    


                    if (beta <= alpha)
                    {
                        break;
                    }

                }
                if (!transpositionTable.ContainsKey(mockBoard))
                {
                    transpositionTable.Add(mockBoard, curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 1), curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 2), curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 3), curBestEval);
                }
                    return curBestEval;
            }
            else
            {
                int curBestEval = int.MaxValue;
                //Baqhchal prev = new Baqhchal(mockBoard);

                foreach(Move move in mockBoard.GenerateMoves(sheepTurn))
                {

                    mockBoard.MakeMove(move);
                    int childValue = Search(depth_remaining - 1, true, depth + 1, alpha, beta);
                    mockBoard.undoMove();
                    //Console.WriteLine("Move: " + move + " value: " + childValue + " depth: " + depth);

                    if (childValue < curBestEval && depth == 0)
                    {
                        bestMove = move;
                        bestEval = Math.Min(bestEval, childValue);


                    }

                    curBestEval = Math.Min(curBestEval, childValue);
                    beta = Math.Min(beta, curBestEval);
                    //mockBoard = new Baqhchal(prev);
                    if(beta <= alpha)
                    {
                        break;
                    }
                }
                if (!transpositionTable.ContainsKey(mockBoard))
                {
                    transpositionTable.Add(mockBoard, curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 1), curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 2), curBestEval);
                    transpositionTable.Add(Baqhchal.grabRotation(mockBoard, 3), curBestEval);
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
