using UnityEngine;
using System.Collections;

public class DetailsCard : MonoBehaviour {

	private static DetailsCard instance;
	public static DetailsCard get() { return instance;}

	private SpriteRenderer[] renderers;

	public void Start(){
		renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		instance = this;
		gameObject.SetActive(false);
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
