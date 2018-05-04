using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxAndGeese;
using System;

public class MatchAlgo {

	//lancia un torneo su population e ne fa side effects; alla fine di tutto, ordine i pesi in population in base al migliore risultato del torneo
	public void LaunchTournament(SortedList<WeightScore, int> population) {
        Dictionary<WeightScore, int> scores = new Dictionary<WeightScore, int>();
        for (int i = 0; i < population.Count; i++) {
            WeightScore ws1 = population.Keys[i];
            for (int j = i + 1; j < population.Keys.Count; j++) {
                WeightScore ws2 = population.Keys[j];
				if (!ws1.Equals(ws2)) {
					AlphaBeta ws1AsGoose = new AlphaBeta(PawnType.Goose, 3, ws1.weights, false);
					AlphaBeta ws1AsFox= new AlphaBeta(PawnType.Fox, 3, ws1.weights, false);
					AlphaBeta ws2AsGoose = new AlphaBeta(PawnType.Goose, 3, ws2.weights, false);
					AlphaBeta ws2AsFox= new AlphaBeta(PawnType.Fox, 3, ws2.weights, false);
                    PairOfScores pairOfScoresGoose = MatchTwoAi(ws1AsGoose, ws2AsFox, 1);
                    PairOfScores pairOfScoresFox = MatchTwoAi(ws1AsFox, ws2AsGoose, 1);
                    scores[ws1] += pairOfScoresGoose.first;
                    scores[ws1] += pairOfScoresFox.first;
                    scores[ws2] += pairOfScoresGoose.second;
                    scores[ws2] += pairOfScoresFox.second;
				}
			}
			WeightScore ws = new WeightScore(ws1.weights, scores[ws1]);
			population.Remove(ws1);
            population.Add(ws, scores[ws1]);
		}
	}


	//fa giocare due ai l'uno contro l'altro per numberOfMatches volte. Se p2 è null, crea un giocatore random.
	public PairOfScores MatchTwoAi(AlphaBeta p1, AlphaBeta p2, int numberOfMatches) {
		Debug.Log("gioco2game");
		int player1Score = 0;
		int player2Score = 0;
		PawnType pawnType1 = p1.aiPlayer;
		PawnType opponentPawnType = pawnType1 == PawnType.Goose ? PawnType.Fox : PawnType.Goose;
		AlphaBeta player2;
		if (p2 == null) {
			player2 = new AlphaBeta(opponentPawnType, 5, null, true);
		} else {
			player2 = p2;
		}
		AlphaBeta goosePlayer = pawnType1 == PawnType.Goose ? p1 : player2;
		AlphaBeta foxPlayer = goosePlayer == p1 ? player2 : p1;
		//Debug.Log("inizio partite");
		for (int i = 0; i < numberOfMatches; i++) {
			int turnLimit = 200;
			int turnCounter = 0;
			Game game = new Game(15, true);

			while (!game.GameOver() && turnCounter < turnLimit) {
				Move move1 = goosePlayer.RunAlphaBeta(game);
				game.MovePawn(move1);
				//Debug.Log("move goose = " + move1);
				if (game.GameOver()) {
					break;
				}
				Move move2 = foxPlayer.RunAlphaBeta(game);
				//Debug.Log("move fox = " + move1);
				game.MovePawn(move2);
				turnCounter++;
			}
			if (game.winner == p1.aiPlayer) {
				player1Score++;
			}
			if (game.winner == player2.aiPlayer) {
				player2Score++;
			}
			//Debug.LogWarning("game numero " + i + " finito vincitore = " + game.winner + " oche rimaste = " + game.GetGooseNumber());
		}
		//Debug.Log("player1 ha vinto " + player1Score + " volte contro le " + player2Score + " di player2");
		return new PairOfScores(player1Score, player2Score);
	}

}
