using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            return board.SelectMovesEscapingCheck(this,board.DiagonalAvailableMoves(this));
        }

        public Bishop(Player player)
            : base(player) { }
        
    }
}