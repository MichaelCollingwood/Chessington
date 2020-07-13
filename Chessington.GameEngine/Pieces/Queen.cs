using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Tests.Pieces;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            foreach (var direction in new List<int>{0,1,2,3,4,5,6,7})
            {
                availableMoves.AddRange(ExploreInOneDirection.ExploreOneDirection(board,board.FindPiece(this),direction));
            }

            return availableMoves;
        }
    }
}