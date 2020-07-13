using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;
            var availableMoves = new List<Square>();
            
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (Board.CheckPosition(initialRow + i, initialCol + j, board))
                    {
                        availableMoves.Add(Square.At(initialRow + i, initialCol + j));
                    }
                }
            }
            
            availableMoves.RemoveAll(x => x == Square.At(initialRow,initialCol));
            
            return availableMoves;
        }
    }
}