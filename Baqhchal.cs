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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (!(obj is Move)) return false;

            Move other = obj as Move;
            return Equals(other);
        }

        public bool Equals(Move other)
        {
            if (other == null) return false;
            if (startx != other.startx) return false;
            if (starty != other.starty) return false;
            if (endx != other.endx) return false;
            if (endy != other.endy) return false;

            return true;

        }
        public override string ToString()
        {
            return startx.ToString() + " " + starty.ToString() + " " + endx.ToString() + " " + endy.ToString();
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
        HashSet<piece[,]> boardStates;
        private int tableSize = 5;
        public piece[,] board { private set; get; }


        public const int minSheep = 10;
        public int capturedSheep { private set; get; }
        public int numSheep { private set; get; }

        public int MinSheep => minSheep;
        //private bool sheepTurn;

        public Baqhchal(int tableSize)
        {
            this.tableSize = tableSize;
            board = new piece[tableSize, tableSize];
            boardStates = new HashSet<piece[,]>();
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

            

            //if move is sheep place, must be over empty point
            if (numSheep < minSheep && sheepTurn)
            {
                //Console.WriteLine("sheep placement logic entered");
                return board[move.startx, move.starty] == piece.Empty;
            }

            //cannot move empty pieces, cannot move ontop of other pieces
            if (board[move.startx, move.starty] == piece.Empty) return false;
            if (board[move.endx, move.endy] != piece.Empty) return false;


            //start square cannot equal end square
            if ((move.startx == move.endx) && (move.starty == move.endy)) return false;


            //moves must be over a line on the grid
            int dx = Math.Abs(move.endx - move.startx);
            int dy = Math.Abs(move.endy - move.starty);
            //any direction
            if((move.startx + move.starty) % 2 == 0)
            {
                //
                if (move.isMoveCapture())
                {
                    if (dx == 0 && dy != 2) return false;
                    else if (dy == 0 && dx != 2) return false;

                    else if (dx != 2 && dy != 2) return false;
                }
                else
                {
                    if(dx > 1 || dy > 1) return false;
                }
            }
            else
            {
                //orthogonally

                if ((dx == 0) == (dy == 0)) return false;
            }



            //captures must occur over a sheep
            if (!sheepTurn && move.isMoveCapture())
            {
                if (board[(move.endx + move.startx) / 2, (move.endy + move.starty / 2)] != piece.Sheep) return false;
            }
            else if (sheepTurn && move.isMoveCapture()) return false;



            //moves cannot reach a state already reached after all sheep have been placed
            if(numSheep >= minSheep)
            {
                MakeMove(move, sheepTurn, true);
                if (boardStates.Contains(board))
                {
                    unMakeMove(move, sheepTurn);
                    return false;
                }

                unMakeMove(move, sheepTurn);
            }



            return true;
        }

        public void MakeMove(Move move, bool sheepTurn, bool isSearchMove = false)
        {
            Console.WriteLine("made move: " + move);
            if (sheepTurn)
            {
                if(numSheep < minSheep)
                {
                    //place sheep;
                    board[move.startx, move.starty] = piece.Sheep;
                    numSheep++;
                    Console.WriteLine("place sheep");
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

                    if (!isSearchMove) capturedSheep++;

                }
                else
                {
                    board[move.startx, move.starty] = piece.Empty;
                    board[move.endx, move.endy] = piece.Tiger;
                }
            }

            if (!isSearchMove) boardStates.Add(board);

        }

        public void unMakeMove(Move move, bool sheepTurn)
        {
            //throw new NotImplementedException();
            Console.WriteLine("unmade move: " + move);
            boardStates.Remove(board);
            if (sheepTurn)
            {
                if(numSheep <= minSheep)
                {
                    board[move.startx, move.starty] = piece.Empty;
                    numSheep--;
                }
                else
                {
                    board[move.startx, move.starty] = piece.Sheep;
                    board[move.endx, move.endy] = piece.Empty;
                }
            }
            else
            {
                if (move.isMoveCapture())
                {
                    board[move.endx, move.endy] = piece.Empty;
                    board[(move.endx + move.startx) / 2, (move.endy + move.starty) / 2] = piece.Sheep;
                    board[move.startx, move.starty] = piece.Tiger;
                }
                else
                {
                    board[move.endx, move.endy] = piece.Empty;
                    board[move.startx, move.starty] = piece.Tiger;
                }
            }
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
                        for(int dx = -1; dx <= 1; dx++)
                        {
                            for(int dy = -1; dy <= 1; dy++)
                            {
                                Move m = new Move(i, j, i + dx, j + dy);
                                if (IsMoveLegal(m, sheepturn)) legalMoves.Add(m);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < tableSize; i++)
                {
                    for (int j = 0; j < tableSize; j++)
                    {
                        for (int dx = -2; dx <= 2; dx++)
                        {
                            for (int dy = -2; dy <= 2; dy++)
                            {
                                Move m = new Move(i, j, i + dx, j + dy);
                                if (IsMoveLegal(m, sheepturn)) legalMoves.Add(m);
                            }
                        }
                    }
                }
            }

            return legalMoves;
        }

        public gameState GameState()
        {
            if (capturedSheep >= 5) return gameState.TigerWon;

            if (GenerateMoves(false).Count == 0) return gameState.SheepWon;

            return gameState.Active;
        }


        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if(!(obj is Baqhchal)) return false;

            Baqhchal other = obj as Baqhchal;
            if(other == null) return false;
            return Equals(other);
        }
        public bool Equals(Baqhchal other)
        {
            if(tableSize != other.tableSize) return false;
            for(int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                    if (board[i, j] != other.board[i, j]) return false;
                }
            }
            return true;
        }
    }
}
