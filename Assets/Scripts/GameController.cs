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

	public void Update(){
		if (StartGame && GameObject.Find("Player 1")){
			StartCoroutine("playGame");
			StartGame = false;
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
		hand.Remove(Digit.Any);
		for (int i=0; i < mutatingFactor; ++i)
			hand[Random.Range(0, 5)] = (Digit) Random.Range(0,5);

		for (int i=0; i < Random.Range(0, 3 * mutatingFactor); ++i)
			hand.Add((Digit) Random.Range(0,5));
		return hand;
	}

	private IEnumerator playGame(){
		players = new Player[2] {
			(player1 ? player1 : GameObject.Find("Player 1")).GetComponent<Player>(),
			(player2 ? player1 : GameObject.Find("Player 2")).GetComponent<Player>()
		};
		for (round = 1 ; !isGameOver(); ++round)
			yield return StartCoroutine(playRound());
	}

	private IEnumerator playRound(){
		var hand = genHand(round-1);
		players[0].Hand = hand.ToList();
		players[1].Hand = hand.ToList();
		for(turn=1; !isRoundOver(); ++turn)
			yield return StartCoroutine((playTurn()));
	}
	
	private IEnumerator playTurn(){
		Debug.Log ("Player 1 :");
		yield return StartCoroutine(players[0].WaitForPlay());
		Debug.Log ("Player 2 :");
		yield return StartCoroutine(players[1].WaitForPlay());
		bool p1win = players[0].Figure.WinsAgainst(players[1].Figure);
		bool p2win = players[1].Figure.WinsAgainst(players[0].Figure);
		string animationName ="";
		if (p1win && p2win){
			players[0].Hand.AddRange(players[0].Figure.Digits);
			players[1].Hand.AddRange(players[1].Figure.Digits);
			animationName = "Egalite";
		}
		if (!p1win && p2win){
			players[1].Score ++;
			animationName = players[1].Figure.Name;
		}
		if (p1win && !p2win){
			players[0].Score ++;
			animationName = players[0].Figure.Name;
		}
		if (!p1win && !p2win){
			animationName = "Null";
		}
		var animation = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Animation"));
		animation.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Animations/"+animationName);

	}

	private bool isRoundOver(){
		return players.Any(player => player.Hand.Count == 0)
			|| turn > 3;
	}

	private bool isGameOver(){
		return players.Any(player => player.Score >= Objective);
	}

}
