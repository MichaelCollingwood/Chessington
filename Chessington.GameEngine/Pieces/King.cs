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
            var availableMoves = new List<Square>
            {
                Square.At(board.FindPiece(this).Row+1,board.FindPiece(this).Col+1),
                Square.At(board.FindPiece(this).Row+1,board.FindPiece(this).Col),
                Square.At(board.FindPiece(this).Row+1,board.FindPiece(this).Col-1),
                Square.At(board.FindPiece(this).Row,board.FindPiece(this).Col+1),
                Square.At(board.FindPiece(this).Row,board.FindPiece(this).Col-1),
                Square.At(board.FindPiece(this).Row-1,board.FindPiece(this).Col+1),
                Square.At(board.FindPiece(this).Row-1,board.FindPiece(this).Col),
                Square.At(board.FindPiece(this).Row-1,board.FindPiece(this).Col-1)
            };

            return availableMoves.Where(x=>board.IsValidPosition(x.Row,x.Col) || board.IsValidCapture(x.Row,x.Col));
        }
    }
}