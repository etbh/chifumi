using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//setFingers (new Digit[]{Digit.Index, Digit.Pouce, Digit.Annulaire, Digit.Pouce, Digit.Annulaire, Digit.Index, Digit.Majeur, Digit.Annulaire, Digit.Auriculaire});
	//main doit aparaitre avec aucun doigt
		//mais peut etre 
	}

	List<GameObject> fings = new List<GameObject>();

	public void setFingers(IList<Digit> tab_fingers){
		const int angle = 160;
		int n = 0;
		int len = tab_fingers.Count - 1;
		int rot = angle / len;
		fings.ForEach (Destroy);
		foreach(var finger in tab_fingers){
			var fing = new GameObject();
			fing.transform.parent = this.gameObject.transform;
			fing.AddComponent<SpriteRenderer>();
			fing.AddComponent<PolygonCollider2D>();
			fing.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hand/" + finger.ToString());
			fing.GetComponent<Transform>().Rotate(0, 0, angle - (rot * n));
			if (finger == Digit.Pouce)
				fing.GetComponent<Transform>().Translate(Vector3.right * 12);
			else
				fing.GetComponent<Transform>().Translate(Vector3.right * 14);
			fings.Add(fing);
			len++;
			n++;
		}
	}

	public void selectFingers(IList<Digit> tab_finger){
		// Doit afficher en surbrillance les doigt nécéssaire à la figure qui est sur la card sélectionné (si il y a trois pouces dispo mais que la figure n'en requière que 1 afficher 
		// le premier rencontré.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}