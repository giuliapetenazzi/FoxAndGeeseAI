using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneticAlgo {

	public SortedList<WeightScore, int> Evolve(SortedList<WeightScore, int> population, double retain, double randomSelect, double mutate) {
		Debug.Log("start genetic algo");
		System.Random rand = new System.Random();
		int retainIndex = (int)(population.Count * retain);
		Debug.Log("population size = " + population.Count + " " + retain + " " + randomSelect + " " + mutate);
		Debug.Log("retain tengo " + retainIndex + " soggetti");
		SortedList<WeightScore, int> parents = new SortedList<WeightScore, int>();

		var iter = population.GetEnumerator();
		iter.MoveNext();
		for (int i = 0; i < retainIndex; i++) {
			parents[iter.Current.Key] = iter.Current.Value;
			iter.MoveNext();
		}

		//aggiunge altri soggetti con una percentuale di aggiunta pari a randomSelect
		while (iter.MoveNext()) {
			if (randomSelect > rand.NextDouble()) {
				var ws = iter.Current;
				parents[ws.Key] = ws.Value;
			}
		}
		
		
		
		int parentsCount = parents.Count;
		int childrenToGenerate = population.Count - parentsCount; //quanti figli devo generare per ritornare ad avere una popolazione completa
		int generatedChildren = 0;
		Debug.LogWarning("fine evolve" + population.Count + " " + parents.Count + " " + childrenToGenerate);
		
		//crossover
		while (parents.Count < population.Count) {
			//Debug.Log("GeneticAlgo genero figli");
			int firstN = rand.Next(parentsCount);
			int secondN = rand.Next(parentsCount);
			Debug.Log("GeneticAlgo parentsCount = " + parents.Count + "firstN = " + firstN + " secondN " + secondN);
			WeightsForBoardEval male = parents.Keys[firstN].weights;
			WeightsForBoardEval female = parents.Keys[secondN].weights;
			if (!male.Equals(female)) {
				WeightsForBoardEval child = Crossover(male, female);
				child = Mutation(child, mutate);
				WeightScore ws = new WeightScore(child, 0);
				parents[ws] = 0;
				generatedChildren++;
			}
		}
		return new MatchAlgo().LaunchTournament(parents); //lancia un torneo sulla popolazione e ritorna la popolazione con gli score aggiornati
		
	}

	private WeightsForBoardEval Crossover(WeightsForBoardEval male, WeightsForBoardEval female) {
		Debug.Log("genetic algo crossover");
		// TODO: ritorna un WeightsForBoardEval nato dalla SCOPAZZA senza cappuccio tra male e female. Implementare operazioni bitwise 
		// invece della classica media fra pesi?
		male.wAheadGooseNumber++;
		return male;
	}

	private WeightsForBoardEval Mutation(WeightsForBoardEval child, double mutate) {
		Debug.Log("genetic algo mutation");
		//TODO: implementare una qualche forma di mutazione (bitwise?)
		return child;
	}

}
