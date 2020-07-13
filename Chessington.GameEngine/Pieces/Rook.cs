using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Tests.Pieces;

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

            var explorer = ExploreInOneDirection();
            
            availableMoves.RemoveAll(x => x == Square.At(initialRow,initialCol));

            return availableMoves;
        }
        
        private bool CheckPosition(int row, int col)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0;
        }
    }
}