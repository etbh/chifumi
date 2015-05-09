using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		setFingers (new Digit[]{Digit.Index, Digit.Pouce, Digit.Annulaire});
	//main doit aparaitre avec aucun doigt
		//mais peut etre 
	}

	List<GameObject> fings = new List<GameObject>();

	public void setFingers(IList<Digit> tab_fingers){
		const int angle = 90;
		float rot = angle / (tab_fingers.Count - 1);
		int n = 0;
		//fings.ForEach (Destroy);
		foreach(var finger in tab_fingers){
			var fing = new GameObject();
			fing.transform.parent = this.gameObject.transform;
			fing.AddComponent<SpriteRenderer>();
			fing.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hand/" + finger.ToString());
			fing.GetComponent<Transform>().Translate(3 * n, 8 - 2 * n, 0);
			fing.GetComponent<Transform>().Rotate(0, 0, rot + n);
			fings.Add(fing);
			n++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}