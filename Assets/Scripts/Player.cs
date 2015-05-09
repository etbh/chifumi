using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {


	void Start() {
		hasPlayed = true;
		selecting = false;
	}
	void Update () {
		if (selecting){
			IEnumerable<GameObject> selected = cards.Where(card => card.GetComponent<Card>().isSelected);
			if (selected.Count() == 1){
				Figure = selected.First().GetComponent<Card>().figure;
				hasPlayed = true;
				selecting = false;
				cards.ForEach(Destroy);
			}
		}
		else if (!hasPlayed){
			selecting = true;
			Debug.Log(Hand.Count()+" digits lefts");
			cards = new List<GameObject>();
			var formable = formableFigures();
			for (int i=0; i<formable.Count(); ++i){
				var figure = formable.ElementAt(i);
				var card = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
				card.transform.parent = gameObject.transform;			
				card.GetComponentsInChildren<SpriteRenderer>()[1].sprite = Resources.Load<Sprite>("Figures/"+figure.Name);
				card.GetComponentsInChildren<SpriteRenderer>()[2].sprite = Resources.Load<Sprite>("Figures/"+figure.Name+"_name");
				cards.Add(card);
				card.GetComponent<Card>().figure = figure;
				float totalrange = formable.Count() * card.GetComponent<SpriteRenderer>().bounds.size.x * 1.1f;
				card.transform.localPosition = new Vector3((i - formable.Count()/2) * totalrange / formable.Count(),0,0);

			}
		}

	}

	private IEnumerable<Figure> formableFigures(){
		return Figure.AllFigures.Where(figure => figure.CanForm(Hand));
	}

	public List<Digit> Hand;

	public int Score;

	public Figure Figure;
	private bool hasPlayed;
	private bool selecting;
	private List<GameObject> cards;

	public IEnumerator WaitForPlay(){
		hasPlayed = false;
		while (!hasPlayed){
			yield return null;
		}
		Figure.Digits.ForEach(digit => Hand.Remove(digit));
	}
}
