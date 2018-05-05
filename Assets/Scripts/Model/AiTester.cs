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

		//WeightScore ws1 = new WeightScore(new WeightsForBoardEval(0, 102, 0, 0 ,0 ,0 ,0 ,0), 3);
		//WeightScore ws2 = new WeightScore(new WeightsForBoardEval(0, 101, 0, 0, 0, 0, 0, 0), 3);
		//Debug.LogWarning("equals = " + ws1.Equals(ws2));
		//Debug.LogWarning("compareTO = " + ws1.CompareTo(ws2));
		//SortedList<WeightScore, int> test = new SortedList<WeightScore, int>();
		//test.Add(ws1, 0);
		//test.Add(ws2, 8);
		//Debug.LogWarning("test size = " + test.Count);
		////test.Remove(ws1);
		//Debug.LogWarning("test size = after remove " + test.Count);
		//test.Keys.RemoveAt(0);
		//Debug.LogWarning("test size = " + test.Count);

		MatchAlgo matchAlgo = new MatchAlgo();
		GeneticAlgo geneticAlgo = new GeneticAlgo();
		System.Random rand = new System.Random();
		List<WeightsForBoardEval> weights = new List<WeightsForBoardEval>();
		int counter = 0;
		//creo 10 giocatori per ogni feature, ognuno con pesi casuali sulla singola feature
		for (int i = 0; i < 3; i++) { //scorre le feature
			for (int j = 0; j < 2; j++) { //indica quanti giocatori per ogni tipologia di feature singola creare
				weights.Add(new WeightsForBoardEval(i, rand.Next(10000)));
			}
		}
		SortedList<WeightScore, int> scoreMap = new SortedList<WeightScore, int>();
		Debug.Log("aiTester finito di craere giocatori single feature ");
		Debug.Log("aiTester weights size = " + weights.Count);
		
		//per ogni peso = giocatore, fa giocare 25 match come oca e 25 match come volpe al giocatore contro un giocatore casuale 
		for (int i = 0; i < weights.Count; i++) {
			int score = 0;
			WeightsForBoardEval weight = weights[i];
			AlphaBeta playerAsFox = new AlphaBeta(PawnType.Fox, 3, weight, false);
			AlphaBeta playerAsGoose = new AlphaBeta(PawnType.Goose, 3, weight, false);
			score = matchAlgo.MatchTwoAi(playerAsFox, null, 3).first;
			score += matchAlgo.MatchTwoAi(playerAsGoose, null, 3).first;
			WeightScore ws = new WeightScore(weight, score);
			scoreMap[ws] = score;
		}
		Debug.Log("aiTester prima di evoluzione");
		Debug.Log("aiTester scoreMap size = " + scoreMap.Count);
		// evolve 10 volte la popolazione
		for (int i = 0; i < 2; i++) {
			Debug.Log("inizio evoluzione turno " + i);
			scoreMap = geneticAlgo.Evolve(scoreMap, 0.6, 0.05, 0.01);
		}
		Debug.LogWarning("best weights " + scoreMap.Keys[0]);

	}


}






