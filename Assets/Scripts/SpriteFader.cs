using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour {

	public float minimum = 0.0f;
	public float maximum = 1.0f;
	public float fadeInDuration = 5.0f;
	public float fadeOutDuration = 5.0f;
	public Vector3 scaleStep = new Vector3(0.01f, 0.01f, 0.01f);
	private float startTime;
	private SpriteRenderer sprite;
	private float fadeInCompleteTime;
	private bool hasFadedIn = false;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		sprite = GetComponent<SpriteRenderer>(); 
	}
	
	// Update is called once per frame
	void Update () {
		// Use hasFadedIn to prevent matching the condition if we're fading out
		if (sprite.color.a != maximum && !hasFadedIn) { // The sprite hasn't completely faded in yet
			float t = (Time.time - startTime) / fadeInDuration;
			sprite.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(minimum, maximum, t));

			// The sprite is totally faded in
			if (sprite.color.a == maximum) {
				hasFadedIn = true;
				fadeInCompleteTime = Time.time;
			}
		} else { // The sprite is faded in, fade it out
			float t = (Time.time - fadeInCompleteTime) / fadeOutDuration;
			sprite.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(maximum, minimum, t));
		}
		sprite.transform.localScale += scaleStep;
	}
}
