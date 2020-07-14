using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;

            if (board.IsValidPosition(Operation(initialRow,1), initialCol))
            {
                availableMoves.Add(Square.At(Operation(initialRow,1), initialCol));

                if (IsPawnStart(initialRow) && board.IsValidPosition(Operation(initialRow,2), initialCol))
                {
                    availableMoves.Add(Square.At(Operation(initialRow, 2), initialCol));
                }
            }
            
            foreach (var i in new List<int>{-1,1})
            {
                if (CheckCapturePawn(Operation(initialRow,1), initialCol+i, board, this.Player))
                {
                    availableMoves.Add(Square.At(Operation(initialRow,1), initialCol+i));
                }
            }

            return availableMoves;
        }

        private int Operation(int number1, int number2)
        {
            return number1 + (Player == Player.Black ? 1 : -1) * number2;
        }
        
        private bool CheckCapturePawn(int row, int col, Board board, Player player)
        {
            if (row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0)
            {
                if (board.GetPiece(Square.At(row, col)) != null)
                {
                    return board.GetPiece(Square.At(row,col)).Player != player;
                }
            }
            return false;
        }

        private bool IsPawnStart(int row)
        {
            return row == 1 && Player == Player.Black || row == GameSettings.BoardSize - 2 && Player == Player.White;
        }
    }
}