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

		public List<Move> GetPossibleMoves() {
			List<Move> moves = new List<Move>();
			return moves;
		}

		public Board GetDeepCopy() {
			return new Board();
		}
		
		public void Move(Move move) {
		
		}
	}
}
