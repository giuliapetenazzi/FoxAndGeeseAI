using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxAndGeese {
	public class AlfaBeta {

		//indica la mossa migliore da fare a partire 
		//dallo stato da cui viene invocato AlfaBeta
		private Move bestMove;
		//indica chi è il giocatore controllato dalla IA
		private PawnType aiPlayer;
		
		public AlfaBeta(PawnType aiPlayer) {
			this.aiPlayer = aiPlayer;
		}

		//board indica lo stato di gioco
		public Move AlphaBeta(Board board) {
			MaxValue(board, int.MinValue, int.MaxValue);
			return bestMove;
		}

		//Oche
		private int MaxValue(Board board, int alpha, int beta) {
			if (board.GameOver()) {
				return board.EvaluateBoard(aiPlayer);
			}
			int v = int.MinValue;
			List<Move> moves = board.GetPossibleMoves();

			foreach (Move move in moves) {
				Board deepCopiedBoard = board.GetDeepCopy();
				deepCopiedBoard.Move(move); //serve cambiare il player attuale nella board
				int score = MinValue(deepCopiedBoard, alpha, beta);
				
				if (score > alpha) {
					alpha = score;
					bestMove = move;
				}
			}
			return v;
		}

		//Volpe
		private int MinValue(Board board, int alpha, int beta) {
			return 0;
		}

	}
}