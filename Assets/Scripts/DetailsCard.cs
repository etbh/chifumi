using UnityEngine;
using System.Collections;

public class DetailsCard : MonoBehaviour {

	private static DetailsCard instance;
	public static DetailsCard get() { return instance;}

	private SpriteRenderer[] renderers;

	public void Start(){
		renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		instance = this;
	}

	public void resetFigure(){
		renderers[1].sprite = null;
		renderers[2].sprite = null;
	}

	public void changeFigure(Figure figure){
		renderers[1].sprite = Resources.Load<Sprite>("Figures/"+figure.Name);
		renderers[2].sprite = Resources.Load<Sprite>("Figures/"+figure.Name+"_name");
	}

}
