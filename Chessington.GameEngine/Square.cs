using System.Collections.Generic;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public struct Square
    {
        public readonly int Row;
        public readonly int Col;

        public Square(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Square At(int row, int col)
        {
            return new Square(row, col);
        }

        public bool Equals(Square other)
        {
            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Square && Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }

        public static bool operator ==(Square left, Square right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Square left, Square right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("Row {0}, Col {1}", Row, Col);
        }

        public IEnumerable<Square> GetAvailableMoves(Piece piece, Board board)
        {
            // set the piece there
            var initialSquare = board.FindPiece(piece);
            piece.MoveTo(board, this);
            
            // understand where it can move then return piece
            var availableMoves = piece.GetAvailableMoves(board);
            piece.MoveTo(board, initialSquare);

            return availableMoves;
        }
    }
}