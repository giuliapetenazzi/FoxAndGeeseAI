using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneticAlgo {

	public SortedList<WeightScore, int> Evolve(SortedList<WeightScore, int> population, double retain, double randomSelect, double mutate) {
		System.Random rand = new System.Random();
		int retainIndex = (int)((population.Count * retain) / 100);

		SortedList<WeightScore, int> parents = new SortedList<WeightScore, int>();

		var iter = population.GetEnumerator();

		for (int i = 0; i < retainIndex; i++) {
			parents[iter.Current.Key] = iter.Current.Value;
			iter.MoveNext();
		}

		//aggiunge altri soggetti con una percentuale di aggiunta pari a randomSelect
		foreach (var ws in population) {
			if (randomSelect > rand.NextDouble()) {
				if (!parents.ContainsKey(ws.Key)) {
					parents.Add(ws.Key, ws.Value);
				}
			}
		}

		int parentsCount = parents.Count;
		int childrenToGenerate = population.Count - parentsCount; //quanti figli devo generare per ritornare ad avere una popolazione completa
		int generatedChildren = 0;
		//crossover
		while (generatedChildren < childrenToGenerate) {
			WeightsForBoardEval male = parents.Keys[rand.Next(parentsCount)].weights;
			WeightsForBoardEval female = parents.Keys[rand.Next(parentsCount)].weights;
			if (!male.Equals(female)) {
				WeightsForBoardEval child = Crossover(male, female);
				child = Mutation(child);
				WeightScore ws = new WeightScore(child, 0);
				parents[ws] = 0;
				generatedChildren++;
			}
		}
		new MatchAlgo().LaunchTournament(parents); //lancia un torneo sulla popolazione e aggiorna population con i nuovi score
		return parents;
	}

	private WeightsForBoardEval Crossover(WeightsForBoardEval male, WeightsForBoardEval female) {
		// TODO: ritorna un WeightsForBoardEval nato dalla SCOPAZZA senza cappuccio tra male e female. Implementare operazioni bitwise 
		// invece della classica media fra pesi?
		return null;
	}

	private WeightsForBoardEval Mutation(WeightsForBoardEval child) {
		//TODO: implementare una qualche forma di mutazione (bitwise?)
		return null;
	}

}
