using System;
using System.Collections.Generic;

namespace Chessington.GameEngine.Tests.Pieces
{
    public class ExploreInOneDirection
    {
        public static IEnumerable<Square> ExploreOneDirection(Board board, Square initialSquare, int direction)
        {
            var squareList = new List<Square>();
            var nextSquare = iterator(initialSquare, direction);
            
            while (true) {
                if (!CheckPosition(nextSquare.Row, nextSquare.Col, board))
                {
                    break;
                }
                squareList.Add(nextSquare);
                nextSquare = iterator(nextSquare, direction);
            }

            if (CheckCapture(nextSquare.Row,nextSquare.Col,board))
            {
                squareList.Add(nextSquare);
            }
            return squareList;
        }

        private static Square iterator(Square square, int direction)
        {
            switch (direction)
            {
                case 0: 
                    return Square.At(square.Row+1, square.Col);
                case 1: 
                    return Square.At(square.Row+1, square.Col+1);
                case 2: 
                    return Square.At(square.Row, square.Col+1);
                case 3: 
                    return Square.At(square.Row-1, square.Col+1);
                case 4: 
                    return Square.At(square.Row-1, square.Col);
                case 5: 
                    return Square.At(square.Row-1, square.Col-1);
                case 6: 
                    return Square.At(square.Row, square.Col-1);
                case 7: 
                    return Square.At(square.Row+1, square.Col-1);
            }
            return new Square();
        }
        
        public static bool CheckPosition(int row, int col, Board board)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0 
                   && board.GetPiece(Square.At(row,col)) == null;
        }
        public static bool CheckCapture(int row, int col, Board board)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0 
                   && (board.GetPiece(Square.At(row,col)) == null || board.GetPiece(Square.At(row,col)).Player != board.CurrentPlayer);
        }
    }
}