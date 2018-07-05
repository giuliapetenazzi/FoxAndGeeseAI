using System.Collections;
using System.Collections.Generic;

using FoxAndGeese;
using System;

public class MatchAlgo {

	//lancia un torneo su population e ne fa side effects; alla fine di tutto, ordine i pesi in population in base al migliore risultato del torneo
	public SortedList<WeightScore, int> LaunchTournament(SortedList<WeightScore, int> population, List<WeightsForBoardEval> testPopulation) {

		SortedList<WeightScore, int> newPopulation = new SortedList<WeightScore, int>();
		Dictionary<WeightScore, int> scores = new Dictionary<WeightScore, int>();
		for (int i = 0; i < population.Count; i++) {
			WeightScore ws1 = population.Keys[i];
			for (int j = 0; j < testPopulation.Count; j++) {
				WeightsForBoardEval testPlayer = testPopulation[j];
				AlphaBeta ws1AsGoose = new AlphaBeta(PawnType.Goose, 3, ws1.weights, false);
				AlphaBeta ws1AsFox = new AlphaBeta(PawnType.Fox, 3, ws1.weights, false);
				AlphaBeta ws2AsGoose = new AlphaBeta(PawnType.Goose, 3, testPlayer, false);
				AlphaBeta ws2AsFox = new AlphaBeta(PawnType.Fox, 3, testPlayer, false);
				PairOfScores pairOfScoresGoose = MatchTwoAi(ws1AsGoose, ws2AsFox, 1, i, j);
				PairOfScores pairOfScoresFox = MatchTwoAi(ws1AsFox, ws2AsGoose, 1, i, j);
				if (!scores.ContainsKey(ws1)) {
					scores[ws1] = 0;
				}
				scores[ws1] += pairOfScoresGoose.first;
				scores[ws1] += pairOfScoresFox.first;
			}
			WeightScore ws = new WeightScore(ws1.weights, scores[ws1]);
			newPopulation.Add(ws, scores[ws1]);
		}
		return newPopulation;
	}
	//Console.WriteLine("inizio LaunchTournament");
	//SortedList<WeightScore, int> newPopulation = new SortedList<WeightScore, int>();
	//Dictionary<WeightScore, int> scores = new Dictionary<WeightScore, int>();
	//      for (int i = 0; i < population.Count; i++) {
	//          WeightScore ws1 = population.Keys[i];
	//          for (int j = i + 1; j < population.Keys.Count; j++) {
	//              WeightScore ws2 = population.Keys[j];
	//		if (!ws1.Equals(ws2)) {
	//			AlphaBeta ws1AsGoose = new AlphaBeta(PawnType.Goose, 3, ws1.weights, false);
	//			AlphaBeta ws1AsFox= new AlphaBeta(PawnType.Fox, 3, ws1.weights, false);
	//			AlphaBeta ws2AsGoose = new AlphaBeta(PawnType.Goose, 3, ws2.weights, false);
	//			AlphaBeta ws2AsFox= new AlphaBeta(PawnType.Fox, 3, ws2.weights, false);
	//			PairOfScores pairOfScoresGoose = MatchTwoAi(ws1AsGoose, ws2AsFox, 1);
	//			PairOfScores pairOfScoresFox = MatchTwoAi(ws1AsFox, ws2AsGoose, 1);
	//			if (!scores.ContainsKey(ws1)) {
	//				scores[ws1] = 0;
	//			}
	//			if (!scores.ContainsKey(ws2)) {
	//				scores[ws2] = 0;
	//			}
	//			scores[ws1] += pairOfScoresGoose.first;
	//                  scores[ws1] += pairOfScoresFox.first;
	//                  scores[ws2] += pairOfScoresGoose.second;
	//                  scores[ws2] += pairOfScoresFox.second;
	//		}
	//	}
	//	WeightScore ws = new WeightScore(ws1.weights, scores[ws1]);
	//	newPopulation.Add(ws, scores[ws1]);
	//}
	//return newPopulation;
//}


//fa giocare due ai l'uno contro l'altro per numberOfMatches volte. Se p2 è null, crea un giocatore random.
public PairOfScores MatchTwoAi(AlphaBeta p1, AlphaBeta p2, int numberOfMatches, int indexOfP1, int indexOfP2) {
	//Console.WriteLine("gioco2game");
	int player1Score = 0;
	int player2Score = 0;
	PawnType pawnType1 = p1.aiPlayer;
	PawnType opponentPawnType = pawnType1 == PawnType.Goose ? PawnType.Fox : PawnType.Goose;
	AlphaBeta player2;
	if (p2 == null) {
		player2 = new AlphaBeta(opponentPawnType, 5, null, true);
	}
	else {
		player2 = p2;
	}
	AlphaBeta goosePlayer = pawnType1 == PawnType.Goose ? p1 : player2;
	AlphaBeta foxPlayer = goosePlayer == p1 ? player2 : p1;
	//Console.WriteLine("inizio partite");
	for (int i = 0; i < numberOfMatches; i++) {
		int turnLimit = 50;
		int turnCounter = 0;
		Game game = new Game(15, true);

		while (!game.GameOver() && turnCounter < turnLimit) {
			Move move1 = goosePlayer.RunAlphaBeta(game);
			game.MovePawn(move1);
			//Console.WriteLine("move goose = " + move1);
			if (game.GameOver()) {
				break;
			}
			Move move2 = foxPlayer.RunAlphaBeta(game);
			//Console.WriteLine("move fox = " + move1);
			game.MovePawn(move2);
			turnCounter++;
		}
		//TODO sistemare numeri hardcoded

		if (game.winner == p1.aiPlayer) {
			Console.WriteLine("Qui qualcuno ha vinto, player1 n° = " + indexOfP1 + " contro player2 n° = " + indexOfP2 + "pesi p1 = " + p1.aiPlayer.ToString());
			player1Score += 32000;
		}
		else if (game.winner == player2.aiPlayer) {
			//Console.WriteLine("Qui qualcuno ha vinto" + player2.aiPlayer.ToString());
			player2Score += 32000;
		}
		else {
			if (p1.aiPlayer == PawnType.Fox) {
				player1Score += 15 - game.GetGooseNumber();
				player2Score += game.GetGooseNumber();
			}
			else if (p1.aiPlayer == PawnType.Goose) {
				player1Score += game.GetGooseNumber();
				player2Score += 15 - game.GetGooseNumber();
			}
		}


		//if (game.winner == p1.aiPlayer) {
		//	player1Score++;
		//}
		//if (game.winner == player2.aiPlayer) {
		//	player2Score++;
		//}
		//Console.WriteLine("game numero " + i + " finito vincitore = " + game.winner + " oche rimaste = " + game.GetGooseNumber());
	}
	//Console.WriteLine("player1 ha vinto " + player1Score + " volte contro le " + player2Score + " di player2");
	return new PairOfScores(player1Score, player2Score);
}

}
