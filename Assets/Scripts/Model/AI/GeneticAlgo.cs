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
				WeightsForBoardEval child = CrossoverAndMutate(male, female, mutate);
				WeightScore ws = new WeightScore(child, 0);
				parents[ws] = 0;
				generatedChildren++;
			}
		}
		return new MatchAlgo().LaunchTournament(parents); //lancia un torneo sulla popolazione e ritorna la popolazione con gli score aggiornati
		
	}

    // ritorna un WeightsForBoardEval nato da male e female con operazioni bitwise 
    private WeightsForBoardEval CrossoverAndMutate(WeightsForBoardEval male, WeightsForBoardEval female, double mutationProbability) {
		Debug.Log("genetic algo crossover");
        /*
        //variazione scema scema
        WeightsForBoardEval child = new WeightsForBoardEval(male.wGooseNumber, female.wAheadGooseNumber, male.wFoxEatingMoves, female.wFoxMoves,
            male.wGooseFreedomness, female.wInterness, male.wExterness);
        */
        Dictionary<String, BitArray> bitFather = new Dictionary<String, BitArray>();
        Dictionary<String, BitArray> bitMother = new Dictionary<String, BitArray>();
        Dictionary<String, bool[]> boolMatrixChild = new Dictionary<String, bool[]>();
        foreach (String key in male.weightDict.Keys) {
            bitFather[key] = new BitArray(new int[] { male.weightDict[key] });
            bitMother[key] = new BitArray(new int[] { female.weightDict[key] });
            bool[] boolFather = new bool[16];
            bool[] boolMother = new bool[16];
            bool[] boolChild = new bool[16];
            for (int i = 0; i < 16; i++) {
                boolFather[i] = bitFather[key][i + 16];
                boolMother[i] = bitMother[key][i + 16];
            }
            //POST ho riempito i due genitori booleani
            //crossover
            boolChild = Mix(boolFather, boolMother);
            //mutazione
            System.Random random = new System.Random();
            double prob = random.NextDouble();
            if (prob > mutationProbability) {
                boolChild = Mutate(boolChild);
            }
            boolMatrixChild.Add(key, boolChild);
        }
        
        //TODO conversione da boolChild in bitChild

        WeightsForBoardEval child = new WeightsForBoardEval(
            GetIntFromBoolArray(boolMatrixChild["wGooseNumber"]),
            GetIntFromBoolArray(boolMatrixChild["wAheadGooseNumber"]),
            GetIntFromBoolArray(boolMatrixChild["wFoxEatingMoves"]),
            GetIntFromBoolArray(boolMatrixChild["wFoxMoves"]),
            GetIntFromBoolArray(boolMatrixChild["wGooseFreedomness"]),
            GetIntFromBoolArray(boolMatrixChild["wInterness"]),
            GetIntFromBoolArray(boolMatrixChild["wExterness"])
            );
		return child;
	}

    private bool[] Mutate(bool[] boolArray) {
        if (boolArray.Length > 16) {
            throw new ArgumentException ("metodo mutate ho piu di 16 bit");
        }
        System.Random rand = new System.Random();
        int pos = rand.Next() * 100 % 15;
        boolArray[pos + 1] = !boolArray[pos + 1];
        return boolArray;
    }

    private bool[] Mix(bool[] boolFather, bool[] boolMother) {
        if (boolFather.Length > 16 || boolMother.Length > 16) {
            throw new ArgumentException ("metodo mix ho piu di 16 bit");
        }
        bool[] boolChild = new bool[16];
        //copio la prima metà dal padre
        for (int i = 0; i < 8; i++) {
            boolChild[i] = boolFather[i];
        }
        //copio la seconda metà dalla madre
        for (int i = 8; i < 16; i++) {
            boolChild[i] = boolMother[i];
        }
        return boolChild;
    }

    private Int16 GetIntFromBoolArray(bool[] boolArray) {
        if (boolArray.Length > 16)
            throw new ArgumentException("Genetic Algo :: getIntFromBitArray :: ci sono piu di 16 bits");
        Int16 number = Convert.ToInt16(boolArray);
        return number;
    }
}