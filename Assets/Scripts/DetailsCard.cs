using UnityEngine;
using System.Collections;

public class DetailsCard : MonoBehaviour {

	private static DetailsCard instance;
	public static DetailsCard get() { return instance; }
	public GameObject cameraObject;

	private SpriteRenderer[] renderers;
	private SpriteRenderer myRenderer;
	private Camera myCam;

	public void Start(){
		renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		instance = this;
		gameObject.SetActive(false);
		myCam = cameraObject.GetComponent<Camera>();
		myRenderer = GetComponent<SpriteRenderer>();
	}

	public void Update() {
		// Follow the mouse position if the object is active (i.e. if the mouse is on a figure card).
		if (this.isActiveAndEnabled) {
			// Mouse pos is in screen coords. Convert it to position the desc card in the world
			Vector3 mouseWorldPos = myCam.ScreenToWorldPoint(Input.mousePosition);
			// Put the desc card at upper left position relatively to mouse
			Vector3 posOffset = new Vector3(-2.0f, -2.5f, 0.0f);
			Vector3 newPos = mouseWorldPos - posOffset;

			// Ensure Z is 0 to be visible
			newPos.z = 0.0f;
			// Ensure desc card center is at most at half its width before right border
			Vector3 screenWidth = new Vector3(Screen.width, 0.0f, 0.0f);
			Vector3 screenRightBorderWorldPos = myCam.ScreenToWorldPoint(screenWidth);
			if (newPos.x <= (screenRightBorderWorldPos.x - (myRenderer.sprite.bounds.max.x / 2))) {
				transform.position = newPos;
			}
		}
	}

	public void resetFigure(){
		renderers[1].sprite = null;
		renderers[2].sprite = null;
		gameObject.SetActive(false);
	}

	public void changeFigure(Figure figure){
		renderers[1].sprite = Resources.Load<Sprite>("Figures/"+figure.Name);
		renderers[2].sprite = Resources.Load<Sprite>("Figures/"+figure.Name+"_name");
		gameObject.SetActive(true);
	}

}
