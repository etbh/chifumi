using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//setFingers (new Digit[]{Digit.Index, Digit.Pouce, Digit.Annulaire, Digit.Pouce, Digit.Annulaire, Digit.Index, Digit.Majeur, Digit.Annulaire, Digit.Auriculaire});
		//selectFingers (new Digit[]{Digit.Pouce, Digit.Index, Digit.Pouce});
	}

	public List<GameObject> fings = new List<GameObject>();

	public void setFingers(IList<Digit> tab_fingers){
		const int angle = 160;
		int n = 0;
		int len = tab_fingers.Count - 1;
		int rot = len != 0 ? angle / len : 90;
		fings.ForEach (Destroy);
		foreach(var finger in tab_fingers){
			var fing = new GameObject();
			fing.transform.parent = this.gameObject.transform;
			fing.AddComponent<SpriteRenderer>();
			fing.AddComponent<PolygonCollider2D>();
			fing.name = finger.ToString();
			fing.GetComponent<Transform>().localScale = Vector3.one;
			fing.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hand/" + finger.ToString());
			fing.GetComponent<Transform>().rotation = gameObject.transform.rotation;
			fing.GetComponent<Transform>().Rotate(0, 0, angle - (rot * n));
			if (finger == Digit.Pouce)
				fing.GetComponent<Transform>().Translate (Vector3.right * 12 * gameObject.transform.localScale.x);
			else
				fing.GetComponent<Transform>().Translate (Vector3.right * 14 * gameObject.transform.localScale.x);
			fing.transform.position += gameObject.transform.position;
			fings.Add(fing);
			len++;
			n++;
		}
	}

	public void selectFingers(IList<Digit> tab_finger){
		// Doit afficher en surbrillance les doigt nécéssaire à la figure qui est sur la card sélectionné (si il y a trois pouces dispo mais que la figure n'en requière que 1 afficher 
		// le premier rencontré.
		List<GameObject> cpy_fing = fings.ToList();
		GameObject check;
		foreach (var fing in fings){
			if (fing != null)
				fing.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		}
		foreach (var finger in tab_finger) {
			check = cpy_fing.Find(fing => fing != null && fing.name == finger.ToString());
			if (check != null){
				check.GetComponent<SpriteRenderer>().color = Color.red;
				cpy_fing.Remove(check);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}


