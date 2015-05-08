using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {


	void Start() {
		hasPlayed = true;
	}
	void Update () {
		if (!hasPlayed){
			//TODO : wheck if player has selected a figure
			// this.Figure = something has to happen here
			hasPlayed = true;
		}

	}

	private IEnumerable<Figure> formableFigures(){
		return Figure.AllFigures.Where(figure => figure.CanForm(Hand));
	}

	public List<Digit> Hand;

	public int Score;

	public Figure Figure;
	private bool hasPlayed;

	public IEnumerator WaitForPlay(){
		hasPlayed = false;
		while (!hasPlayed){
			yield return null;
		}
		Figure.Digits.ForEach(digit => Hand.Remove(digit));
	}
}
