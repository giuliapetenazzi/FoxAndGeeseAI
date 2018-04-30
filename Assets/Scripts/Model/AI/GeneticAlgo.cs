using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneticAlgo {

	public void Evolve(SortedList<WeightScore, int> population, float retain, float randomSelect, float mutate) {
		new MatchAlgo().LaunchTournament(population); //lancia un torneo sulla popolazione e aggiorna population con i nuovi score
		System.Random rand = new System.Random();
		int retainIndex = (int)((population.Count * retain) / 100);

		SortedList<WeightScore, int> parents = new SortedList<WeightScore, int>();

		var iter = population.GetEnumerator();

		for (int i = 0; i < retainIndex; i++) {
			parents[iter.Current.Key] = iter.Current.Value;
			iter.MoveNext();
		}

		//aggiunge altri soggetti con una percentuale di aggiunta di randomSelect %
		foreach (var ws in population) {
			if (randomSelect > rand.NextDouble()) {
				if (!parents.ContainsKey(ws.Key)) {
					parents.Add(ws.Key, ws.Value);
				}
			}
		}

		//crossover
		//mutation? o muovere mutation prima del crossover?

	}

}
