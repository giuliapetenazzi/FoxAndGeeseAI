using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeightScore : IComparable {

	public WeightsForBoardEval weights { get; set; } // pesi
	public int score { get; set; } //punteggio di vittorie

	public WeightScore(WeightsForBoardEval weights, int score) {
		this.weights = weights;
		this.score = score;
	}	

	public int CompareTo(object obj) {
		return score.CompareTo(obj);
	}

	public override bool Equals(object obj) {
		var score = obj as WeightScore;
		return score != null &&
			   EqualityComparer<WeightsForBoardEval>.Default.Equals(weights, score.weights);
	}

	public override int GetHashCode() {
		return -1248457298 + EqualityComparer<WeightsForBoardEval>.Default.GetHashCode(weights);
	}
}
