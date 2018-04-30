using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeightsForBoardEval { 
    public int wWinningState { get; set; } // meglio per volpe se basso
	public int wGooseNumber { get; set; } // meglio per volpe se basso
	public int wAheadGooseNumber { get; set; } // meglio per volpe se basso
	public int wFoxEatingMoves { get; set; } // meglio per volpe se alto
	public int wFoxMoves { get; set; } // meglio per volpe se alto
	public int wGooseFreedomness { get; set; } // meglio per volpe se alta
	public int wInterness { get; set; } // meglio per volpe se alto
	public int wExterness { get; set; } // meglio per volpe se basso


	public WeightsForBoardEval(int numberOfFeature, int weight) {
		if (numberOfFeature == 0) {
			wWinningState = weight;
		} else if (numberOfFeature == 1) {
			wGooseNumber = weight;
		} else if (numberOfFeature == 2) {
			wAheadGooseNumber = weight;
		} else if (numberOfFeature == 3) {
			wFoxEatingMoves = weight;
		} else if (numberOfFeature == 4) {
			wFoxMoves = weight;
		} else if (numberOfFeature == 5) {
			wGooseFreedomness = weight;
		} else if (numberOfFeature == 6) {
			wInterness = weight;
		} else if (numberOfFeature == 7) {
			wExterness = weight;
		}
	}

	public WeightsForBoardEval(int wWinningState, int wGooseNumber, int wAheadGooseNumber,
        int wFoxEatingMoves, int wFoxMoves, int wGooseFreedomness, int wInterness, int wExterness) {
        this.wWinningState = wWinningState;
        this.wGooseNumber = wGooseNumber;
        this.wAheadGooseNumber = wAheadGooseNumber;
        this.wFoxEatingMoves = wFoxEatingMoves;
        this.wFoxMoves = wFoxMoves;
        this.wGooseFreedomness = wGooseFreedomness;
        this.wInterness = wInterness;
        this.wExterness = wExterness;
    }

	public override bool Equals(object obj) {
		var eval = obj as WeightsForBoardEval;
		return eval != null &&
			   wWinningState == eval.wWinningState &&
			   wGooseNumber == eval.wGooseNumber &&
			   wAheadGooseNumber == eval.wAheadGooseNumber &&
			   wFoxEatingMoves == eval.wFoxEatingMoves &&
			   wFoxMoves == eval.wFoxMoves &&
			   wGooseFreedomness == eval.wGooseFreedomness &&
			   wInterness == eval.wInterness &&
			   wExterness == eval.wExterness;
	}

	public override int GetHashCode() {
		var hashCode = -1428641248;
		hashCode = hashCode * -1521134295 + wWinningState.GetHashCode();
		hashCode = hashCode * -1521134295 + wGooseNumber.GetHashCode();
		hashCode = hashCode * -1521134295 + wAheadGooseNumber.GetHashCode();
		hashCode = hashCode * -1521134295 + wFoxEatingMoves.GetHashCode();
		hashCode = hashCode * -1521134295 + wFoxMoves.GetHashCode();
		hashCode = hashCode * -1521134295 + wGooseFreedomness.GetHashCode();
		hashCode = hashCode * -1521134295 + wInterness.GetHashCode();
		hashCode = hashCode * -1521134295 + wExterness.GetHashCode();
		return hashCode;
	}
}
