using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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

	private List<Player> players;
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
		players = new List<Player>() {
			(player1 ? player1 : player1 = GameObject.Find("Player 1")).GetComponent<Player>(),
			(player2 ? player2 : player2 = GameObject.Find("Player 2")).GetComponent<Player>()
		};
		players.ForEach(player => player.Turns = player.Rounds = 0);
		for (round = 1 ; !isGameOver(); ++round){
			yield return StartCoroutine(playRound());
			if (!ScoreMode){
				if (players[0].Turns > players[1].Turns)
					players[0].Rounds ++;
				if (players[0].Turns < players[1].Turns)
					players[1].Rounds ++;
				players[0].Turns = players[1].Turns = 0;
			}
		}
	}

	private IEnumerator playRound(){
		var hand = genHand(round-1);
		players[0].Hand = hand.ToList();
		players[1].Hand = hand.ToList();
		GameObject.Find("GameBackground").GetComponent<GameBackgroundPicker>().PickRandom();
		for(turn=1; !isRoundOver(); ++turn)
			yield return StartCoroutine((playTurn()));
		var animation = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Animation"));
		animation.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Animations/NouvelleHumanite");
		yield return new WaitForSeconds(2);
	}

	private IEnumerator waitEndAnimation(){
		Debug.Log ("waitEndAnimation");
		return null;
//		while (GameObject.Find("Animation)") == null){
//			Debug.Log ("Not Found");
//			yield return null;
//		}
//		while (GameObject.Find("Animation") != null){
//			Debug.Log("Found animation");
//			yield return null;
//		}
	}
	
	private IEnumerator playTurn(){
		players[0].UpdateFingers();
		players[1].UpdateFingers();
		GameObject.Find("J1 Text").GetComponent<Text>().text = "Joueur 1: " + (ScoreMode ? player1.GetComponent<Player>().Turns : player1.GetComponent<Player>().Rounds);
		GameObject.Find("J2 Text").GetComponent<Text>().text = "Joueur 2: " + (ScoreMode ? player2.GetComponent<Player>().Turns : player2	.GetComponent<Player>().Rounds);
		GameObject.Find("Victoire Text").GetComponent<Text>().text = Objective + " " + (ScoreMode ? "Points" : "Manches");
		if (!ScoreMode)
			GameObject.Find("Tour Text").GetComponent<Text>().text = "Tour : " + turn;
		Debug.Log ("Player 1 :");
		yield return StartCoroutine(players[0].WaitForPlay());
		Debug.Log ("Player 2 :");
		yield return StartCoroutine(players[1].WaitForPlay());
		players[1].UpdateFingers();
		bool p1win = players[0].Figure.WinsAgainst(players[1].Figure);
		bool p2win = players[1].Figure.WinsAgainst(players[0].Figure);
		string animationName ="";
		if (p1win && p2win){
			players[0].Hand.AddRange(players[0].Figure.Digits);
			players[1].Hand.AddRange(players[1].Figure.Digits);
			animationName = "Egalite";
		}
		if (!p1win && p2win){
			players[1].Turns ++;
			animationName = players[1].Figure.Name;
		}
		if (p1win && !p2win){
			players[0].Turns ++;
			animationName = players[0].Figure.Name;
		}
		if (!p1win && !p2win){
			animationName = "Null";
		}
		var animation = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Animation"));
		animation.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Animations/"+animationName);
		yield return new WaitForSeconds(2);
	}

	private bool isRoundOver(){
		return players.Any(player => !Figure.AllFigures.Any(fig => fig.CanForm(player.Hand)))
			|| turn > 3;
	}

	private bool isGameOver(){
		return players.Any(player => (ScoreMode? player.Turns: player.Rounds) >= Objective);
	}

}
