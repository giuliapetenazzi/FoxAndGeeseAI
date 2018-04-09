using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtilityIA {
    /*
    public PawnType[,] board { get; private set; }

    public static List<Move> IACalculateGooseValidMoves(int r, int c) {
        List<Move> gooseValidMoves = new List<Move>();
        if (!MyUtility.IsPositionOutOfCross(r - 1, c) && board[r - 1, c] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r - 1, c));
        if (!MyUtility.IsPositionOutOfCross(r, c - 1) && board[r, c - 1] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r, c - 1));
        if (!MyUtility.IsPositionOutOfCross(r, c + 1) && board[r, c + 1] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r, c + 1));
        return gooseValidMoves;
    }

    public static List<Move> IACalculateFoxValidMoves(int r, int c) {
        List<Move> foxValidMoves = new List<Move>();
        //setto l'intorno del centro dato
        for (int roundR = r - 1; roundR <= r + 1; roundR++) {
            for (int roundC = c - 1; roundC <= c + 1; roundC++) {
                if (!MyUtility.IsPositionOutOfCross(roundR, roundC) && !(roundR == r && roundC == C) &&
                    board[roundR, roundC] == PawnType.None) {
                    foxValidMoves.Add(new Move(PawnType.Fox, r, c, roundR, roundC));
                }
            }
        }
        return foxValidMoves;
    }

    public static List<Move> IACalculateFoxEatingValidMoves(int r, int c) {
        List<Move> foxValidEatingMoves = new List<Move>;
        for (int roundR = r - 2; roundR <= r + 2; roundR+=2) {
            for (int roundC = c - 2; roundC <= c + 2; roundC+=2) {
                if (!MyUtility.IsPositionOutOfCross(roundR, roundC) &&
                    //TODO la casella interpolata deve essere nella board
                    !MyUtility.IsPositionOutOfCross(roundR, roundC) &&
                    !(roundR == r && roundC == C) &&
                    board[roundR, roundC] == PawnType.None &&
                    //TODO la casella interpolata deve avere una goose sotto
                    board[roundR, roundC] == PawnType.Goose
                    ) {
                    foxValidEatingMoves.Add(new Move(PawnType.Fox, r, c, roundR, roundC));
                }
            }
        }
        return foxValidEatingMoves;
    }
    */


        /* 
         * //APPUNTI DA GAME
            bool eatenCorrectly = false;
            Vector2 interpolPawn = EatingPawnPosition(move);
            eatenCorrectly = (interpolPawn.x != -1 && interpolPawn.y != -1);
            // la mossa è corretta sse la pedina è stata mangiata correttamente
            return eatenCorrectly;
         */
}
