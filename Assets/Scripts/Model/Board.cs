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

		public List<Move> GetPossibleMoves(PawnType current_player, int r, int c) {
			List<Move> allPossibleMoves = new List<Move>();
            /*
            if (current_player == PawnType.Fox) {
                allPossibleMoves.AddRange(MyUtilityIA.IACalculateFoxValidMoves(r, c));
                allPossibleMoves.AddRange(MyUtilityIA.IACalculateFoxValidEatingMoves(r, c));
            }
            if (current_player == PawnType.Goose) {
                allPossibleMoves.AddRange(MyUtilityIA.IACalculateGooseValidMoves(r, c));
            }
            */
			return allPossibleMoves;
		}

		public Board GetDeepCopy() {
			return new Board();
		}
		
		public void Move(Move move) {
		
		}
	}
}
