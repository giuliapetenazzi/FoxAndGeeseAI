using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxAndGeese {
    //Rappresenta una board di gioco in un dato istante
    public class Board {

        public PawnType turn { get; private set; }
        public PawnType[,] board { get; private set; }

        public bool GameOver() {
            return true;
        }

        public int EvaluateBoard(PawnType aiPlayer) {
            return 0;
        }

        public Board GetDeepCopy() {
            return new Board();
        }

        public void Move(Move move) {

        }

        public List<Move> GetPossibleMoves() {
            List<Move> allPossibleMoves = new List<Move>();
            for (int r = 0; r < 7; r++) {
                for (int c = 0; c < 7; c++) {
                    PawnType current_player = board[r, c];
                    if (!MyUtility.IsPositionOutOfCross(r, c) && current_player != PawnType.None) {
                        if (current_player == PawnType.Fox) {
                            allPossibleMoves.AddRange(IACalculateFoxValidMoves(r, c));
                            allPossibleMoves.AddRange(IACalculateFoxValidEatingMoves(r, c));
                        }
                        if (current_player == PawnType.Goose) {
                            allPossibleMoves.AddRange(IACalculateGooseValidMoves(r, c));
                        }
                    }
                }
            }
            return allPossibleMoves;
        }

        private List<Move> GetPossibleMovesForOnePlayer(PawnType current_player, int r, int c) {
            List<Move> possibleMoves = new List<Move>();
            if (current_player == PawnType.Fox) {
                possibleMoves.AddRange(IACalculateFoxValidMoves(r, c));
                possibleMoves.AddRange(IACalculateFoxValidEatingMoves(r, c));
            }
            if (current_player == PawnType.Goose) {
                possibleMoves.AddRange(IACalculateGooseValidMoves(r, c));
            }
            return possibleMoves;
        }
        
        //la goose non puo andare in diagonale
        //non puo retrocedere di riga (anzi, cioò per gli indici avanzare)
        //restano solo tre mosse: avanti, dx, sx
        private List<Move> IACalculateGooseValidMoves(int r, int c) {
            List<Move> gooseValidMoves = new List<Move>();
            if (!MyUtility.IsPositionOutOfCross(r - 1, c) && board[r - 1, c] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r - 1, c));
            if (!MyUtility.IsPositionOutOfCross(r, c - 1) && board[r, c - 1] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r, c - 1));
            if (!MyUtility.IsPositionOutOfCross(r, c + 1) && board[r, c + 1] == PawnType.None)
                gooseValidMoves.Add(new Move(PawnType.Goose, r, c, r, c + 1));
            return gooseValidMoves;
        }

        //la fox si puo muovere in tutte le 8 direzioni, a distanza 1
        private List<Move> IACalculateFoxValidMoves(int r, int c) {
            List<Move> foxValidMoves = new List<Move>();
            //setto l'intorno del centro dato
            for (int roundR = r - 1; roundR <= r + 1; roundR++) {
                for (int roundC = c - 1; roundC <= c + 1; roundC++) {
                    if (!MyUtility.IsPositionOutOfCross(roundR, roundC) &&
                        !(roundR == r && roundC == c) &&
                        board[roundR, roundC] == PawnType.None) {
                        foxValidMoves.Add(new Move(PawnType.Fox, r, c, roundR, roundC));
                    }
                }
            }
            return foxValidMoves;
        }

        //la fox per mangiare si puo muovere in tutte le 8 direzioni, a distanza 2
        private List<Move> IACalculateFoxValidEatingMoves(int r, int c) {
            List<Move> foxValidEatingMoves = new List<Move>();
            for (int roundR = r - 2; roundR <= r + 2; roundR += 2) {
                for (int roundC = c - 2; roundC <= c + 2; roundC += 2) {
                    if (
                        //la casella di destinazione deve essere nella board
                        !MyUtility.IsPositionOutOfCross(roundR, roundC) &&
                        //la casella interpolata deve essere nella board
                        !MyUtility.IsPositionOutOfCross((roundR + r) / 2, (roundC + c) / 2) &&
                        //non ci si può muovere verso la stessa casella di partenza
                        !(roundR == r && roundC == c) &&
                        //la casella di destinazione deve essere vuota
                        board[roundR, roundC] == PawnType.None &&
                        //la casella interpolata deve avere una goose sotto
                        board[(roundR + r) / 2, (roundC + c) / 2] == PawnType.Goose) {
                        foxValidEatingMoves.Add(new Move(PawnType.Fox, r, c, roundR, roundC));
                    }
                }
            }
            return foxValidEatingMoves;
        }
    }
}