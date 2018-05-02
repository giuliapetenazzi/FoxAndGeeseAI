using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FoxAndGeese;
using System;


//[InitializeOnLoad]
public class AiTester : MonoBehaviour {


	public void StartTest() {
		Debug.Log("0 ");

		MatchAlgo matchAlgo = new MatchAlgo();
		GeneticAlgo geneticAlgo = new GeneticAlgo();
		System.Random rand = new System.Random();
		List<WeightsForBoardEval> weights = new List<WeightsForBoardEval>();
		int counter = 0;
		//creo 10 giocatori per ogni feature, ognuno con pesi casuali sulla singola feature
		for (int i = 0; i < 8; i++) { //scorre le feature
			for (int j = 0; j < 2; j++) { //indica quanti giocatori per ogni tipologia di feature singola creare
				weights.Add(new WeightsForBoardEval(i, rand.Next(Int32.MinValue, Int32.MaxValue)));
			}
		}
		SortedList<WeightScore, int> scoreMap = new SortedList<WeightScore, int>();
		Debug.Log("1 ");
		//per ogni peso = giocatore, fa giocare 25 match come oca e 25 match come volpe al giocatore contro un giocatore casuale 
		for (int i = 0; i < weights.Count; i++) {
			Debug.Log("weights for = " + i);
			int score = 0;
			WeightsForBoardEval weight = weights[i];
			AlphaBeta playerAsFox = new AlphaBeta(PawnType.Fox, 3, weight, false);
			AlphaBeta playerAsGoose = new AlphaBeta(PawnType.Goose, 3, weight, false);
			score = matchAlgo.MatchTwoAi(playerAsFox, null, 10);
			score += matchAlgo.MatchTwoAi(playerAsGoose, null, 10);
			WeightScore ws = new WeightScore(weight, score);
			scoreMap[ws] = score;
		}
		Debug.Log("2 ");

		// evolve 10 volte la popolazione
		for (int i = 0; i < 2; i++) {
			Debug.Log("inizio evoluzione turno " + i);
			scoreMap = geneticAlgo.Evolve(scoreMap, 0.2, 0.05, 0.01);
		}
		
	}


}






