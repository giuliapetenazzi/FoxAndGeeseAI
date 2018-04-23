using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FoxAndGeese;
using System;

//[InitializeOnLoad]
public class AiTester : MonoBehaviour {


	public void StartTest() {
		WeightsForBoardEval w1 = new WeightsForBoardEval(Int16.MaxValue, 2, 0, 0, 0, 0, 0, 0);
		AlphaBeta player1 = new AlphaBeta(PawnType.Fox, 5, w1, false);
		WeightsForBoardEval w2 = new WeightsForBoardEval(Int16.MaxValue, 0, 0, 0, 2, 0, 0, 0);
		AlphaBeta player2 = new AlphaBeta(PawnType.Fox, 5, w2, false);
		MatchTwoAi(player1, player2, 100);
	}

	public void MatchTwoAi(AlphaBeta p1, AlphaBeta p2, int numberOfMatches) {
		int player1Score = 0;
		int player2Score = 0;
		PawnType pawnType1 = p1.aiPlayer;
		PawnType opponentPawnType = pawnType1 == PawnType.Goose ? PawnType.Fox : PawnType.Goose;
		AlphaBeta player2;
		if (p2 == null) {
			player2 = new AlphaBeta(opponentPawnType, 5, null, true);
		} else {
			player2 = p2;
		}
		AlphaBeta goosePlayer = pawnType1 == PawnType.Goose ? p1 : player2;
		AlphaBeta foxPlayer = goosePlayer == p1 ? player2 : p1;
		Debug.Log("inizio partite");
		for (int i = 0; i < numberOfMatches; i++) {
			int turnLimit = 500;
			int turnCounter = 0;
			Game game = new Game(15, true);

			while (!game.IsGameOver() && turnCounter < turnLimit) {
				Move move1 = goosePlayer.RunAlphaBeta(game);
				game.MovePawn(move1);
				if (game.IsGameOver()) {
					break;
				}
				Move move2 = foxPlayer.RunAlphaBeta(game);
				game.MovePawn(move2);
				turnCounter++;
			}
			if (game.winner == p1.aiPlayer) {
				player1Score++;
			}
			if (game.winner == p2.aiPlayer) {
				player2Score++;
			}
		}
		Debug.Log("player1 ha vinto " + player1Score + " volte contro le " + player2Score + " di player2");
	}
}







	//static AiTester() {
	//	Debug.Log("ciao");
	//	WeightsForBoardEval w1 = new WeightsForBoardEval(Int32.MaxValue, 2, 4, -2, -2, -1, -1, 1);
	//	AlphaBeta player1 = new AlphaBeta(PawnType.Fox, 5);
	//	AlphaBeta player2 = new AlphaBeta(PawnType.Goose, 5);
	//	Debug.Log("AiTester inizio");
	//	for (int i = 0; i < 1; i++) {
	//		int counterTurn = 0;
	//		Game game = new Game(15, true);
	//		while (!game.IsGameOver() && counterTurn < 2000) {
	//			//Debug.Log("turno" + counterTurn + "goose num = " +
	//			//game.GetGooseNumber());
	//			Move move1 = player1.RunAlphaBeta(game);
	//			Debug.Log("move1 = " + move1);
	//			game.MovePawn(move1);
	//			if (game.IsGameOver()) {
	//				break;
	//			}
	//			Move move2 = player2.RunAlphaBeta(game);
	//			Debug.Log("move2 = " + move2);
	//			game.MovePawn(move2);
	//			counterTurn++;
	//			//Debug.Log(counterTurn);
	//		}

	//		Debug.Log("AITESTER FINIO" + " TURNO = " + counterTurn + " VINCITORE = " + game.winner + " OCHE = " + game.GetGooseNumber());

			
		//}
//	}
//}