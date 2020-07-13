using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player)
        {
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;
            var availableMoves = new List<Square>();

            for (var i = -GameSettings.BoardSize; i <= GameSettings.BoardSize; i++)
            {
                if (Board.CheckPosition(initialRow, initialCol + i, board))
                {
                    availableMoves.Add(Square.At(initialRow, initialCol + i));
                }

                if (Board.CheckPosition(initialRow + i, initialCol, board))
                {
                    availableMoves.Add(Square.At(initialRow + i, initialCol));
                }
            }
            availableMoves.RemoveAll(x => x == Square.At(initialRow,initialCol));

            return availableMoves;
        }
    }
}