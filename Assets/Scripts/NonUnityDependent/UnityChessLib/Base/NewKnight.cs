﻿using System;

namespace UnityChess {
    public class NewKnight : NewPiece {
        public NewKnight(Square startingPosition, Side color) : base(startingPosition, color) {}
        public NewKnight(NewKnight knightCopy) : base(knightCopy) {}

        public override void UpdateLegalMoves(ChessGameState gameState) {
            LegalMoves.Clear();
            CheckKnightCircleSquares(board);
        }

        private void CheckKnightCircleSquares(Board board) {
            for (int fileOffset = -2; fileOffset <= 2; fileOffset++) {
                if (fileOffset == 0) continue;

                foreach (int rankOffset in Math.Abs(fileOffset) == 2 ? new[] {-1, 1} : new[] {-2, 2}) {
                    Square testSquare = new Square(Position, fileOffset, rankOffset);
                    Movement testMove = new Movement(Position, testSquare);

                    Square enemyKingPosition = Color == Side.White ? board.BlackKing.Position : board.WhiteKing.Position;
                    if (testSquare.IsValid && !board.IsOccupiedBySide(testSquare, Color) && Rules.MoveObeysRules(board, testMove, Color) && testSquare != enemyKingPosition)
                        LegalMoves.Add(new Movement(testMove));
                }
            }
        }
    }
}