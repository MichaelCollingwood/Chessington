using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player)
        {
            IsPawn = true;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;
            
            // move forwards
            if (board.IsValidPosition(Operation(initialRow,1, Player), initialCol))
            {
                availableMoves.Add(Square.At(Operation(initialRow,1, Player), initialCol));

                if (IsPawnStart(this.Player, initialRow) && board.IsValidPosition(Operation(initialRow,2, Player), initialCol))
                {
                    availableMoves.Add(Square.At(Operation(initialRow, 2, Player), initialCol));
                }
            }
            
            // attack diagonal adjacent
            foreach (var i in new List<int>{-1,1})
            {
                if (IsValidPawnCapture(Operation(initialRow,1, Player), initialCol+i, board, Player))
                {
                    availableMoves.Add(Square.At(Operation(initialRow,1, Player), initialCol+i));
                }
            }

            return board.SelectMovesEscapingCheck(this, availableMoves);
        }

        public static int Operation(int number1, int number2, Player player)
        {
            return number1 + (player == Player.Black ? 1 : -1) * number2;
        }
        
        private bool IsValidPawnCapture(int row, int col, Board board, Player player)
        {
            if (row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0)
            {
                // standard take
                if (board.GetPiece(Square.At(row, col)) != null)
                {
                    return board.GetPiece(Square.At(row,col)).Player != player;
                }
                // en passant
                else if (board.GetPiece(Square.At(Operation(row,-1, Player),col)) != null)
                {
                    return board.GetPiece(Square.At(Operation(row,-1, Player),col)).Player != player 
                           && board.GetPiece(Square.At(Operation(row,-1, Player),col)).DoubleStep;
                }
            }

            return false;
        }

        public static bool IsPawnStart(Player player, int row)
        {
            return row == 1 && player == Player.Black || row == GameSettings.BoardSize - 2 && player == Player.White;
        }
    }
}