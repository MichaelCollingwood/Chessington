using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;
            var availableMoves = new List<Square>();

            for (var i = -GameSettings.BoardSize; i <= GameSettings.BoardSize; i++)
            {
                if (Board.CheckPosition(initialRow + i, initialCol + i, board))
                {
                    availableMoves.Add(Square.At(initialRow + i, initialCol + i));
                }

                if (Board.CheckPosition(initialRow + i, initialCol - i, board))
                {
                    availableMoves.Add(Square.At(initialRow + i, initialCol - i));
                }
            }
            availableMoves.RemoveAll(x => x == Square.At(initialRow,initialCol));

            return availableMoves;
        }

        public Bishop(Player player)
            : base(player) { }
    }
}