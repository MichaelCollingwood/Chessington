using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        private bool isDoubleStepActive;
        private Dictionary<Player,Square> kingSquares = new Dictionary<Player,Square>
        {
            {Player.White, Square.At(7,5)},
            {Player.Black, Square.At(0,5)}
        };
        
        private bool isInCheck;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }
            
            // condition for potential en passant
            if (movingPiece.IsPawn && Math.Abs(from.Row - to.Row) == 2)
            {
                movingPiece.DoubleStep = true;
                isDoubleStepActive = true;
            }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }
            
            //en passant victim piece
            var enPassantVictim = GetPiece(Square.At(Pawn.Operation(to.Row, -1, movingPiece.Player), to.Col));
            
            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }
            else if (enPassantVictim != null)
            {
                if (enPassantVictim.Player != movingPiece.Player && enPassantVictim.DoubleStep && isDoubleStepActive)
                {
                    OnPieceCaptured(board[Pawn.Operation(to.Row, -1, movingPiece.Player), to.Col]);
                    board[Pawn.Operation(to.Row, -1, movingPiece.Player), to.Col] = null;
                }
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
            
            //set isInCheck as NextPlayer?
            isInCheck = movingPiece.GetAvailableMoves(this).Contains(kingSquares[CurrentPlayer]);
        }

        public List<Square> DiagonalAvailableMoves(Piece piece)
        {
            var availableMoves = new List<Square>();
            availableMoves.AddRange(ExploreOneDirection(piece, x => Square.At(x.Row + 1, x.Col + 1)));
            availableMoves.AddRange(ExploreOneDirection(piece, x => Square.At(x.Row - 1, x.Col - 1)));
            availableMoves.AddRange(ExploreOneDirection(piece, x => Square.At(x.Row + 1, x.Col - 1)));
            availableMoves.AddRange(ExploreOneDirection(piece, x => Square.At(x.Row - 1, x.Col + 1)));
            return availableMoves;
        }
        
        public List<Square> GridAvailableMoves(Piece piece)
        {
            var availableMoves = new List<Square>();
            availableMoves.AddRange(ExploreOneDirection(piece,x=>Square.At(x.Row+1,x.Col)));
            availableMoves.AddRange(ExploreOneDirection(piece,x=>Square.At(x.Row-1,x.Col)));
            availableMoves.AddRange(ExploreOneDirection(piece,x=>Square.At(x.Row,x.Col+1)));
            availableMoves.AddRange(ExploreOneDirection(piece,x=>Square.At(x.Row,x.Col-1)));
            return availableMoves;
        }

        private IEnumerable<Square> ExploreOneDirection(Piece piece, Func<Square, Square> iterator)
        {
            var squareList = new List<Square>();
            var nextSquare = iterator(FindPiece(piece));
            
            while (IsValidPosition(nextSquare.Row, nextSquare.Col)) 
            {
                squareList.Add(nextSquare);
                nextSquare = iterator(nextSquare);
            }

            if (IsValidCapture(nextSquare.Row,nextSquare.Col))
            {
                squareList.Add(nextSquare);
            }
            return squareList;
        }

        public bool IsValidPosition(int row, int col)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0 
                   && GetPiece(Square.At(row,col)) == null;
        }
        public bool IsValidCapture(int row, int col)
        {
            return row < GameSettings.BoardSize && row >= 0 && col < GameSettings.BoardSize && col >= 0 
                   && (GetPiece(Square.At(row,col)) == null || GetPiece(Square.At(row,col)).Player != CurrentPlayer);
        }
        public List<Square> SelectMovesEscapingCheck(List<Square> moves)
        {
            if (isInCheck)
            {
                return moves.Where(x => x != kingSquares[CurrentPlayer]).ToList();
            }
            else
            {
                return moves;
            }
        }
        
        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
