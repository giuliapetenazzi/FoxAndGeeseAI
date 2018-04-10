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
		public Move AlphaBeta(Game game) {
			MaxValue(game, int.MinValue, int.MaxValue);
			return bestMove;
		}

		//Oche
		private int MaxValue(Game game, int alpha, int beta) {
			if (game.GameOver()) {
				return game.EvaluateBoard(aiPlayer);
			}
			List<Move> moves = game.GetPossibleMoves();

			foreach (Move move in moves) {
				Game deepCopiedGame = game.GetDeepCopy();
				deepCopiedGame.MovePawn(move); //il turn viene cambiato da questo metodo
				int score = MinValue(deepCopiedGame, alpha, beta);

				if (score > alpha) {
					alpha = score;
					bestMove = move;
				}

				if (alpha >= beta) {
					break;
				}
			}
			return alpha;
		}

		//Volpe
		private int MinValue(Game game, int alpha, int beta) {
			if (game.GameOver()) {
				return game.EvaluateBoard(aiPlayer);
			}
			List<Move> moves = game.GetPossibleMoves();

			foreach (Move move in moves) {
				Game deepCopiedGame = game.GetDeepCopy();
				deepCopiedGame.MovePawn(move); //serve cambiare il player attuale nella board
				int score = MaxValue(deepCopiedGame, alpha, beta);

				if (score < beta) {
					beta = score;
					bestMove = move;
				}

				if (alpha >= beta) {
					break;
				}
			}
			return beta;
		}

	}
}