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
		Match(player1, 100);
	}

	public void Match(AlphaBeta player1, int numberOfMatches) {
		int player1Score = 0;
		PawnType pawnType1 = player1.aiPlayer;
		PawnType opponentPawnType = pawnType1 == PawnType.Goose ? PawnType.Fox : PawnType.Goose;
		AlphaBeta randomPlayer = new AlphaBeta(opponentPawnType, 5, null, true);
		AlphaBeta goosePlayer = pawnType1 == PawnType.Goose ? player1 : randomPlayer;
		AlphaBeta foxPlayer = goosePlayer == player1 ? randomPlayer : player1;

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
			if (game.winner == player1.aiPlayer) {
				player1Score++;
			}
		}
		Debug.Log("player1 ha vinto " + player1Score + " volte ");
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