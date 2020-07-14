using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>
            {
                Square.At(board.FindPiece(this).Row+2,board.FindPiece(this).Col-1),
                Square.At(board.FindPiece(this).Row+2,board.FindPiece(this).Col+1),
                Square.At(board.FindPiece(this).Row+1,board.FindPiece(this).Col+2),
                Square.At(board.FindPiece(this).Row-1,board.FindPiece(this).Col+2),
                Square.At(board.FindPiece(this).Row-2,board.FindPiece(this).Col+1),
                Square.At(board.FindPiece(this).Row-2,board.FindPiece(this).Col-1),
                Square.At(board.FindPiece(this).Row-1,board.FindPiece(this).Col-2),
                Square.At(board.FindPiece(this).Row+1,board.FindPiece(this).Col-2)
            };

            return availableMoves.Where(x => board.IsValidCapture(x.Row,x.Col));
        }
    }
}