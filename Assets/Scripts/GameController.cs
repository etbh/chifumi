using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {

	//Singleton, sort of
	private static GameController instance = null;
	public static GameController Instance{
		get{
			if (instance == null){
				instance = FindObjectOfType<GameController>();
				DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}
	void Awake(){
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
		else{
			if (this != instance)
				Destroy(this.gameObject);
		}

	}

	public void Start(){
		StartGame = false;
	}

	public void Update(){
		if (StartGame){
			StartCoroutine("playGame");
		}
	}

	public GameObject player1;
	public GameObject player2;

	public bool ScoreMode;
	public int Objective;
	public bool IA;
	public bool StartGame;

	private Player[] players;
	private int round;
	private int turn;
	private bool waitingForPlayers;

//	public void StartGame(bool scoreMode, int objective, bool IA){
//		this.scoreMode = scoreMode;
//		this.objective = objective;
//		this.IA = IA;
//		StartCoroutine("startGame");
//	}

	private static List<Digit> genHand(int mutatingFactor){
		var hand = System.Enum.GetValues(typeof(Digit)).Cast<Digit>().ToList<Digit>();
		for (int i=0; i < mutatingFactor; ++i)
			hand[Random.Range(0, 5)] = (Digit) Random.Range(0,5);

		for (int i=0; i < Random.Range(0, 3 * mutatingFactor); ++i)
			hand.Add((Digit) Random.Range(0,5));
		return hand;
	}

	private void playGame(){
		players = new Player[2] {player1.GetComponent<Player>(), player2.GetComponent<Player>()};
		for (round = 1 ; !isGameOver(); ++round)
			playRound ();
	}

	private void playRound(){
		var hand = genHand(round-1);
		players[0].Hand = hand;
		players[1].Hand = hand;
		for(turn=1; !isRoundOver(); ++turn)
			playTurn ();
	}
	
	private void playTurn(){
		players[0].WaitForPlay();
		players[1].WaitForPlay();
		bool p1win = players[0].Figure.WinsAgainst(players[1].Figure);
		bool p2win = players[1].Figure.WinsAgainst(players[0].Figure);
		if (p1win && p2win){
			players[0].Hand.AddRange(players[0].Figure.Digits);
         	players[1].Hand.AddRange(players[1].Figure.Digits);
		}
		if (!p1win && p2win){
			players[1].Score ++;
		}
		if (p1win && !p2win){
			players[0].Score ++;
		}
		if (!p1win && !p2win){

		}
	}	

	private bool isRoundOver(){
		return players.Any(player => player.Hand.Count == 0)
			|| turn > 3;
	}

	private bool isGameOver(){
		return players.Any(player => player.Score >= Objective);
	}

}
