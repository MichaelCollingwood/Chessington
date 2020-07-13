using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player)
        {
            
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var initialRow = board.FindPiece(this).Row;
            var row1 = Operation(board.FindPiece(this).Row, 1);
            var row2 = Operation(board.FindPiece(this).Row, 2);
            var col = board.FindPiece(this).Col;
            
            var availableMoves = new List<Square>();
            if (!Board.CheckPosition(row1, col, board)) return availableMoves;
            
            if ((initialRow == 1 && Player == Player.Black) ||
                (initialRow == GameSettings.BoardSize - 1 && Player == Player.White))
            {
                availableMoves.Add(Square.At(row1, col));

                if (Board.CheckPosition(row2, col, board))
                {
                    availableMoves.Add(Square.At(row2, col));
                }
            }
            else
            {
                availableMoves.Add(Square.At(row1, col));
            }

            return availableMoves;
        }

        private int Operation(int number1, int number2)
        {
            return number1 + (Player == Player.Black ? 1 : -1) * number2;
        }
    }
}