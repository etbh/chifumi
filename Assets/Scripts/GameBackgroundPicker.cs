using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class GameBackgroundPicker : MonoBehaviour {

	public Sprite[] backgrounds;

	// Use this for initialization
	void Start() {
		PickRandom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PickRandom() {
		// Select a random background (we SHOULD have at least 1 background !)
		int backgroundIdx = Random.Range(0, backgrounds.Length);
		SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
		if (myRenderer)
			myRenderer.sprite = backgrounds[backgroundIdx];
	}
}
