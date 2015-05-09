using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public bool isSelected = false;
	public Figure figure;

	public void OnMouseDown(){
		isSelected = true;
	}
}
