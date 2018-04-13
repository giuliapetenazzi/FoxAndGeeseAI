using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxAndGeese;
using System;

using UnityEditor;

[InitializeOnLoad]
public class AiTester {
	static AiTester() {
		Debug.Log("ciao");
		Game game = new Game(15, true);
		AlfaBeta player1 = new AlfaBeta(PawnType.Fox, 2);
		AlfaBeta player2 = new AlfaBeta(PawnType.Goose, 2);
		int counterTurn = 0;
		Debug.Log("AiTester inizio");
		while (!game.IsGameOver() && counterTurn < 4000) {
			Debug.Log("turno" + counterTurn + "goose num = " +
				game.GetGooseNumber());
			Move move1 = player1.AlphaBeta(game);
			game.MovePawn(move1);
			if (game.IsGameOver()) {
				break;
			}
			Move move2 = player2.RandomMove(game);
			game.MovePawn(move2);
			counterTurn++;
		}

		//Debug.Log("AITESTER FINIO" + game.GetGooseNumber());

	}
}