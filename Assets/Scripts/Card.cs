using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {

	public bool isSelected = false;
	public Figure figure;

	public void OnMouseEnter(){
		DetailsCard.get().changeFigure(figure);
		gameObject.transform.parent.gameObject.GetComponentInChildren<Hand>().selectFingers(figure.Digits);
	}

	public void OnMouseExit(){
		DetailsCard.get ().resetFigure();
		gameObject.transform.parent.gameObject.GetComponentInChildren<Hand>().selectFingers(new List<Digit>());
	}

	public void OnMouseDown(){
		OnMouseExit();
		isSelected = true;
	}
}
