using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Tests.Pieces;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            foreach (var direction in new List<int>{1,3,5,7})
            {
                availableMoves.AddRange(ExploreInOneDirection.ExploreOneDirection(board,board.FindPiece(this),direction));
            }

            return availableMoves;
        }

        public Bishop(Player player)
            : base(player) { }
        
    }
}