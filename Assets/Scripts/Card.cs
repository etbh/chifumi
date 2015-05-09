using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public bool isSelected = false;
	public Figure figure;

	public void OnMouseEnter(){
		DetailsCard.get().changeFigure(figure);
	}

	public void OnMouseExit(){
		DetailsCard.get ().resetFigure();
	}

	public void OnMouseDown(){
		OnMouseExit();
		isSelected = true;
	}
}
