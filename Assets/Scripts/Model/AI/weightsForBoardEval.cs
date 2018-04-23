using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsForBoardEval { 
    private int wWinningState; // meglio per volpe se basso
    private int wGooseNumber; // meglio per volpe se basso
    private int wAheadGooseNumber; // meglio per volpe se basso
    private int wFoxEatingMoves; // meglio per volpe se alto
    private int wFoxMoves; // meglio per volpe se alto
    private int wGooseFreedomness; // meglio per volpe se alta
    private int wInterness; // meglio per volpe se alto
    private int wExterness;// meglio per volpe se basso

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

    public int WWinningState {
        get {
            return wWinningState;
        }

        set {
            wWinningState = value;
        }
    }

    public int WGooseNumber {
        get {
            return wGooseNumber;
        }

        set {
            wGooseNumber = value;
        }
    }

    public int WAheadGooseNumber {
        get {
            return wAheadGooseNumber;
        }

        set {
            wAheadGooseNumber = value;
        }
    }

    public int WFoxEatingMoves {
        get {
            return wFoxEatingMoves;
        }

        set {
            wFoxEatingMoves = value;
        }
    }

    public int WFoxMoves {
        get {
            return wFoxMoves;
        }

        set {
            wFoxMoves = value;
        }
    }

    public int WGooseFreedomness {
        get {
            return wGooseFreedomness;
        }

        set {
            wGooseFreedomness = value;
        }
    }

    public int WInterness {
        get {
            return wInterness;
        }

        set {
            wInterness = value;
        }
    }

    public int WExterness {
        get {
            return wExterness;
        }

        set {
            wExterness = value;
        }
    }
}
