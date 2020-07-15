using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            availableMoves.AddRange(board.DiagonalAvailableMoves(this));
            availableMoves.AddRange(board.GridAvailableMoves(this));

            return board.SelectMovesEscapingCheck(this,availableMoves);
        }
    }
}