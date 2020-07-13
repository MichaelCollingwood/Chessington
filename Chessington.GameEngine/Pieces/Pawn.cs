using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player)
        {
            
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;

            if (CheckPosition(Operation(initialRow, 1), initialCol, board))
            {
                availableMoves.Add(Square.At(Operation(initialRow,1), initialCol));

                if ((initialRow == 1 && Player == Player.Black ||
                    initialRow == GameSettings.BoardSize - 2 && Player == Player.White)
                    && CheckPosition(Operation(initialRow,2), initialCol, board))
                {
                    availableMoves.Add(Square.At(Operation(initialRow, 2), initialCol));
                }
            }

            return availableMoves;
        }

        private int Operation(int number1, int number2)
        {
            return number1 + (Player == Player.Black ? 1 : -1) * number2;
        }
        
        private bool CheckPosition(int row, int col, Board board)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0 && board.GetPiece(Square.At(row,col)) == null;
        }
    }
}