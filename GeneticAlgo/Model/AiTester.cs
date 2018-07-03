using System.Collections;
using System.Collections.Generic;
using FoxAndGeese;
using System;


//[InitializeOnLoad]
public class AiTester {

	static void Main() {
		Console.WriteLine("inizio main");
		new AiTester().StartTest();
	}


	public void StartTest() {
		
		//WeightScore ws1 = new WeightScore(new WeightsForBoardEval(0, 101, 0, 0, 0, 0, 0, 0), 3);
		//WeightScore ws2 = new WeightScore(new WeightsForBoardEval(0, 102, 0, 0, 0, 0, 0, 0), 3);
		//WeightScore ws3 = new WeightScore(new WeightsForBoardEval(0, 102, 0, 0, 0, 0, 0, 0), 3);
		//WeightScore ws4 = new WeightScore(new WeightsForBoardEval(0, 102, 0, 0, 0, 0, 0, 0), 3);


		//Console.WriteLine("equals = " + ws1.Equals(ws2));
		//Console.WriteLine("compareTO = " + ws1.CompareTo(ws2));
		//SortedList<WeightScore, int> test = new SortedList<WeightScore, int>();
		//test.Add(ws1, 0);
		//test.Add(ws2, 8);
		//test.Add(ws3, 0);
		//test.Add(ws4, 0);
		//Console.WriteLine("test size = " + test.Count);
		//Console.WriteLine("containsKey = " + test.ContainsKey(ws3));
		
		MatchAlgo matchAlgo = new MatchAlgo();
		GeneticAlgo geneticAlgo = new GeneticAlgo();
		System.Random rand = new System.Random();
		List<WeightsForBoardEval> weights = new List<WeightsForBoardEval>();
		int counter = 0;
		//creo 10 giocatori per ogni feature, ognuno con pesi casuali sulla singola feature
		for (int i = 0; i < 5; i++) { //scorre le feature
			for (int j = 0; j < 2; j++) { //indica quanti giocatori per ogni tipologia di feature singola creare
				
				WeightsForBoardEval w =  new WeightsForBoardEval(i, (Int16)rand.Next(Int16.MaxValue));
				weights.Add(w);
				Console.WriteLine("creato peso iniziale casuale = " + w);
			}
		}
		SortedList<WeightScore, int> scoreMap = new SortedList<WeightScore, int>();
		Console.WriteLine("aiTester finito di craere giocatori single feature ");
		Console.WriteLine("aiTester weights size = " + weights.Count);
		
		//per ogni peso = giocatore, fa giocare 25 match come oca e 25 match come volpe al giocatore contro un giocatore casuale 
		for (int i = 0; i < weights.Count; i++) {
			int score = 0;
			WeightsForBoardEval weight = weights[i];
			AlphaBeta playerAsFox = new AlphaBeta(PawnType.Fox, 3, weight, false);
			AlphaBeta playerAsGoose = new AlphaBeta(PawnType.Goose, 3, weight, false);
			score = matchAlgo.MatchTwoAi(playerAsFox, null, 3).first;
			score += matchAlgo.MatchTwoAi(playerAsGoose, null, 3).first;
			WeightScore ws = new WeightScore(weight, score);
			//for (int j = 0; i < scoreMap.Count; j++) {
			//	WeightScore w = scoreMap.Keys[j];
			//	if (w.CompareTo(ws) == 0) {
			//		Console.WriteLine("w già contenuto =  " + w);
			//		Console.WriteLine("ws nuovo = " + ws);
			//	}
			//}
			
			//Console.WriteLine("ws = " + ws + " contains = " + scoreMap.ContainsKey(ws));
			scoreMap.Add(ws, score);
		}
		
		Console.WriteLine("aiTester prima di evoluzione");
		//Console.WriteLine("aiTester scoreMap size = " + scoreMap.Count);
		// evolve 10 volte la popolazione
		for (int i = 0; i < 2; i++) {
			Console.WriteLine("\n \n \n \n inizio evoluzione turno " + i);
			scoreMap = geneticAlgo.Evolve(scoreMap, 0.2, 0.05, 0.01);
		}
		Console.WriteLine("best weights " + scoreMap.Keys[0]);
		Console.WriteLine("finito completamente aiTester");
		Console.ReadKey();
	}


}






