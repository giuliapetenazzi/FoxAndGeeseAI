﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FoxAndGeese {
	public class AlfaBeta {

		//indica la mossa migliore da fare a partire 
		//dallo stato da cui viene invocato AlfaBeta
		private Move bestMove;
		//indica chi è il giocatore controllato dalla IA
		private PawnType aiPlayer;
		private int maxPly;

		public AlfaBeta(PawnType aiPlayer, int maxPly) {
			this.aiPlayer = aiPlayer;
			this.maxPly = maxPly;
		}

		//board indica lo stato di gioco
		public Move AlphaBeta(Game game) {
			MaxValue(game, int.MinValue, int.MaxValue, 0);
			return bestMove;
		}

		public Move RandomMove(Game game) {
			List<Move> moves = game.GetPossibleMoves();
			int size = moves.Count;

			System.Random r = new System.Random();
			int rInt = r.Next(0, size); //for ints
			return moves[rInt];
		}

		//Oche
		private int MaxValue(Game game, int alpha, int beta, int currentPly) {
			Debug.Log("MaxValue " + currentPly);
			currentPly++;
			if (currentPly > maxPly || game.GameOver()) {
				return game.EvaluateBoard(aiPlayer);
			}
			List<Move> moves = game.GetPossibleMoves();

			foreach (Move move in moves) {
				Game deepCopiedGame = game.GetDeepCopy();
				deepCopiedGame.MovePawn(move); //il turn viene cambiato da questo metodo
				int score = MinValue(deepCopiedGame, alpha, beta, currentPly);

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
		private int MinValue(Game game, int alpha, int beta, int currentPly) {
			Debug.Log("MinValue " + currentPly);
			currentPly++;
			if (currentPly > maxPly || game.GameOver()) {
				return game.EvaluateBoard(aiPlayer);
			}
			List<Move> moves = game.GetPossibleMoves();

			foreach (Move move in moves) {
				Game deepCopiedGame = game.GetDeepCopy();
				deepCopiedGame.MovePawn(move); //serve cambiare il player attuale nella board
				int score = MaxValue(deepCopiedGame, alpha, beta, currentPly);

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