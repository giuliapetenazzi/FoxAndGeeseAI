using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FoxAndGeese;
using System;


//[InitializeOnLoad]
public class AiTester : MonoBehaviour {


	public void StartTest() {
		Debug.Log("startedTest");
		MatchAlgo matchAlgo = new MatchAlgo();
		System.Random rand = new System.Random();
		List<WeightsForBoardEval> weights = new List<WeightsForBoardEval>();
		int counter = 0;
		//creo 10 giocatori per ogni feature, ognuno con pesi casuali sulla singola feature
		for (int i = 0; i < 8; i++) { //scorre le feature
			Debug.Log("scorroFeature");
			for (int j = 0; j < 2; j++) { //indica quanti giocatori per ogni tipologia di feature singola creare
				weights.Add(new WeightsForBoardEval(i, rand.Next(Int32.MinValue, Int32.MaxValue)));
			}
		}
		SortedList<WeightScore, int> scoreMap = new SortedList<WeightScore, int>();	

		//per ogni peso = giocatore, fa giocare 50 match come oca e 50 match come volpe al giocatore contro un giocatore casuale 
		for (int i = 0; i < weights.Count; i++) {
			int score = 0;
			WeightsForBoardEval weight = weights[i];
			AlphaBeta playerAsFox = new AlphaBeta(PawnType.Fox, 3, weight, false);
			AlphaBeta playerAsGoose = new AlphaBeta(PawnType.Goose, 3, weight, false);
			score = matchAlgo.MatchTwoAi(playerAsFox, null, 25);
			score += matchAlgo.MatchTwoAi(playerAsGoose, null, 25);
			WeightScore ws = new WeightScore(weight, score);
			scoreMap.Add(ws, score);
		}

		//Utils.PrintDictionary(scoreMap);



		//WeightsForBoardEval w1 = new WeightsForBoardEval(Int16.MaxValue, 2, 0, 0, 0, 0, 0, 0);
		//AlphaBeta player1 = new AlphaBeta(PawnType.Fox, 5, w1, false);
		//WeightsForBoardEval w2 = new WeightsForBoardEval(Int16.MaxValue, 0, 0, 0, 2, 0, 0, 0);
		//AlphaBeta player2 = new AlphaBeta(PawnType.Fox, 5, w2, false);
		//MatchTwoAi(player1, player2, 2);

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