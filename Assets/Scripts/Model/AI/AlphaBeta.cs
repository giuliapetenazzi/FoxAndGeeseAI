using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FoxAndGeese;

public class AlphaBeta {

	//indica la mossa migliore da fare a partire 
	//dallo stato da cui viene invocato AlfaBeta
	private Move bestMove;
	//indica chi è il giocatore controllato dalla IA
	private PawnType aiPlayer;
	private int maxPly;

	public AlphaBeta(PawnType aiPlayer, int maxPly) {
		this.aiPlayer = aiPlayer;
		this.maxPly = maxPly;
	}

	//board indica lo stato di gioco
	public Move RunAlphaBeta(Game game) {
		Game deepCopiedGame = game.GetDeepCopy();
		MaxValue(deepCopiedGame, int.MinValue, int.MaxValue, 0);
		return bestMove;
	}

	public Move RandomMove(Game game) {
		List<Move> moves = game.GetPossibleMoves();
		int size = moves.Count;
		System.Random r = new System.Random();
		int rInt = r.Next(0, size); //for ints
		return moves[rInt];
	}

	private int MaxValue(Game game, int alpha, int beta, int currentPly) {
		//Debug.Log("MaxValue " + currentPly);
		currentPly++;
		if (currentPly > maxPly || game.GameOver()) {
            int wWinningState = 200; // meglio per volpe se basso
            int wGooseNumber = 2; // meglio per volpe se basso
            int wAheadGooseNumber = 4; // meglio per volpe se basso
            int wFoxEatingMoves = -2; // meglio per volpe se alto
            int wFoxMoves = -2; // meglio per volpe se alto
            int wGooseFreedomness = -1; // meglio per volpe se alta
            int wInterness = -1; // meglio per volpe se alto
            int wExterness = 1; // meglio per volpe se basso
			return game.EvaluateBoard(aiPlayer, wWinningState, wGooseNumber,
                wAheadGooseNumber, wFoxEatingMoves, wFoxMoves, wGooseFreedomness, wInterness, wExterness);
		}
		List<Move> moves = game.GetPossibleMoves();

		foreach (Move move in moves) {
			Game deepCopiedGame = game.GetDeepCopy();
			deepCopiedGame.MovePawn(move); //il turn viene cambiato da questo metodo
			int score = MinValue(deepCopiedGame, alpha, beta, currentPly);

			if (score > alpha) {
				alpha = score;
				if (currentPly == 1) {
					bestMove = move;
				}
			}

			if (alpha >= beta) {
				break;
			}
		}
		return alpha;
	}

	private int MinValue(Game game, int alpha, int beta, int currentPly) {
		//Debug.Log("MinValue " + currentPly);
		currentPly++;
		if (currentPly > maxPly || game.GameOver()) {
            int wWinningState = 200; // meglio per volpe se basso
            int wGooseNumber = 2; // meglio per volpe se basso
            int wAheadGooseNumber = 4; // meglio per volpe se basso
            int wFoxEatingMoves = -2; // meglio per volpe se alto
            int wFoxMoves = -2; // meglio per volpe se alto
            int wGooseFreedomness = -1; // meglio per volpe se alta
            int wInterness = -1; // meglio per volpe se alto
            int wExterness = 1; // meglio per volpe se basso
			return game.EvaluateBoard(aiPlayer, wWinningState, wGooseNumber,
                wAheadGooseNumber, wFoxEatingMoves, wFoxMoves, wGooseFreedomness, wInterness, wExterness);
		}
		List<Move> moves = game.GetPossibleMoves();

		foreach (Move move in moves) {
			Game deepCopiedGame = game.GetDeepCopy();
			deepCopiedGame.MovePawn(move); //il turn viene cambiato da questo metodo
			int score = MaxValue(deepCopiedGame, alpha, beta, currentPly);

			if (score < beta) {
				beta = score;
				//bestMove = move;
			}

			if (alpha >= beta) {
				break;
			}
		}
		return beta;
	}

}
