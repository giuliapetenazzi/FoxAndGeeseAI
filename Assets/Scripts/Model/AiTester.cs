using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FoxAndGeese;
using System;

//[InitializeOnLoad]
public class AiTester {
	static AiTester() {
		Debug.Log("ciao");
		AlphaBeta player1 = new AlphaBeta(PawnType.Fox, 3);
		AlphaBeta player2 = new AlphaBeta(PawnType.Goose, 3);
		int counterTurn = 0;
		Debug.Log("AiTester inizio");
		for (int i = 0; i < 10; i++) {
			Game game = new Game(15, true);
			while (!game.IsGameOver() && counterTurn < 850000) {
				//Debug.Log("turno" + counterTurn + "goose num = " +
				//game.GetGooseNumber());
				Move move1 = player1.RunAlphaBeta(game);
				game.MovePawn(move1);
				if (game.IsGameOver()) {
					break;
				}
				Move move2 = player2.RandomMove(game);
				game.MovePawn(move2);
				counterTurn++;
			}

			Debug.Log("AITESTER FINIO" + " TURNO = " + counterTurn + " VINCITORE = " + game.winner + " OCHE = " + game.GetGooseNumber());
		}
	}
}