using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Tests.Pieces;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var initialRow = board.FindPiece(this).Row;
            var initialCol = board.FindPiece(this).Col;

            if (ExploreInOneDirection.CheckPosition(Operation(initialRow,1), initialCol, board))
            {
                availableMoves.Add(Square.At(Operation(initialRow,1), initialCol));

                if ((initialRow == 1 && Player == Player.Black ||
                    initialRow == GameSettings.BoardSize - 2 && Player == Player.White)
                    && ExploreInOneDirection.CheckPosition(Operation(initialRow,2), initialCol, board))
                {
                    availableMoves.Add(Square.At(Operation(initialRow, 2), initialCol));
                }
            }
            
            foreach (var i in new List<int>{-1,1})
            {
                if (CheckCapturePawn(Operation(initialRow,1), initialCol+i, board, this.Player))
                {
                    availableMoves.Add(Square.At(Operation(initialRow,1), initialCol+i));
                }
            }

            return availableMoves;
        }

        private int Operation(int number1, int number2)
        {
            return number1 + (Player == Player.Black ? 1 : -1) * number2;
        }
        
        private bool CheckCapturePawn(int row, int col, Board board, Player player)
        {
            if (row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0)
            {
                if (board.GetPiece(Square.At(row, col)) != null)
                {
                    return board.GetPiece(Square.At(row,col)).Player != player;
                }
            }
            return false;
        }
    }
}