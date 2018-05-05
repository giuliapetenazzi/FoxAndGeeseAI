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

    private Int16 getIntFromBitArray(BitArray bitArray) {
        if (bitArray.Length > 16)
            throw new ArgumentException("Genetic Algo Argument length shall be at most 16 bits.");
        Int16[] array = new Int16[1];
        bitArray.CopyTo(array, 0);
        return array[0];
    }

    // ritorna un WeightsForBoardEval nato da male e female con operazioni bitwise 
    private WeightsForBoardEval Crossover(WeightsForBoardEval male, WeightsForBoardEval female) {
		Debug.Log("genetic algo crossover");
        //inserisco le feature nel figlio (alternativamente tra padre e madre)
        /*
        WeightsForBoardEval child = new WeightsForBoardEval(male.wGooseNumber, female.wAheadGooseNumber, male.wFoxEatingMoves, female.wFoxMoves,
            male.wGooseFreedomness, female.wInterness, male.wExterness);
        */
        Dictionary<String, BitArray> bitFather = new Dictionary<String, BitArray>();
        foreach (String key in male.weightDict.Keys) {
            bitFather[key] = new BitArray(new int[] { male.weightDict[key] });
        }
        Dictionary<String, BitArray> bitMother = new Dictionary<String, BitArray>();
        foreach (String key in female.weightDict.Keys) {
            bitMother[key] = new BitArray(new int[] { female.weightDict[key] });
        }
        Dictionary<String, BitArray> bitChild = new Dictionary<String, BitArray>();
        foreach ( String key in male.weightDict.Keys) {
            bitChild[key] = mix(bitFather[key], bitMother[key]);
        }

        WeightsForBoardEval child = new WeightsForBoardEval(
            getIntFromBitArray(bitChild["wGooseNumber"]),
            getIntFromBitArray(bitChild["wAheadGooseNumber"]),
            getIntFromBitArray(bitChild["wFoxEatingMoves"]),
            getIntFromBitArray(bitChild["wFoxMoves"]),
            getIntFromBitArray(bitChild["wGooseFreedomness"]),
            getIntFromBitArray(bitChild["wInterness"]),
            getIntFromBitArray(bitChild["wExterness"])
            );
		return child;
	}

    private BitArray mix(BitArray bitFather, BitArray bitMother) {
        BitArray bitChild = new BitArray(bitFather);
        for (int i = 0; i < bitChild.Count; i++) {
            if (i % 2 == 1) {
                bitChild[i] = bitMother[i];
            }
        }
        throw new NotImplementedException();
    }

    private WeightsForBoardEval Mutation(WeightsForBoardEval child, double mutate) {
        //TODO: implementare una qualche forma di mutazione (bitwise?)
        System.Random random = new System.Random();
        double prob = random.NextDouble();
        if (prob > mutate) {
            int pos = random.Next(0, 7);
            List<BitArray> bitChild = new List<BitArray>();
            bitChild[0] = (new BitArray(new int[] { child.wGooseNumber }));
            bitChild[1] = (new BitArray(new int[] { child.wAheadGooseNumber }));
            bitChild[2] = (new BitArray(new int[] { child.wFoxEatingMoves }));
            bitChild[3] = (new BitArray(new int[] { child.wFoxMoves }));
            bitChild[4] = (new BitArray(new int[] { child.wGooseFreedomness }));
            bitChild[5] = (new BitArray(new int[] { child.wInterness }));
            bitChild[6] = (new BitArray(new int[] { child.wExterness }));
            //faccio la variazione della feature in posizione pos
            bitChild[pos] = (new BitArray(new int[] { child.wExterness }));
            int everyOrUnique = random.NextDouble();
        }
        return child;
	}



}
