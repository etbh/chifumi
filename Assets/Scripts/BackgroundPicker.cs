using UnityEngine;
using System.Collections;

public class BackgroundPicker : MonoBehaviour {

	public Sprite[] backgrounds;

	// Use this for initialization
	void Start() {
		PickRandom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PickRandom() {
		// Select a random background (we SHOULD have at least 1 background !)
		int backgroundIdx = Random.Range(0, backgrounds.Length);
		print ("Selectiong background " + backgroundIdx);
		SpriteRenderer rndr = GetComponent<SpriteRenderer>();
		rndr.sprite = backgrounds[backgroundIdx];
	}
}
