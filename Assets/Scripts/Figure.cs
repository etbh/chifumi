using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Figure {

		
	public bool CanForm(List<Digit> hand){
		if (hand.Count < Digits.Count)
			return false;
		foreach (Digit digit in System.Enum.GetValues(typeof(Digit))){
			if (digit != Digit.Any){
				if (Digits.Count(_digit => _digit == digit) > hand.Count(_digit => _digit == digit))
					return false;
			}
		}
		return true;
	}


	public string Name;
	public List<Digit> Digits;
	public System.Func<Figure, bool> WinsAgainst;


	public Figure(string name, IEnumerable<Digit> digits, System.Func<Figure, bool> winsAgainst){
		this.Name = name;
		this.Digits = digits.ToList();
		this.WinsAgainst = winsAgainst;
	}

	public static List<Figure> AllFigures {
		get{
			return new List<Figure> () {
				new Figure(
					"Papier",
					Enumerable.Repeat(Digit.Any, 4),
					figure => figure.Name == "Caillou"
					),				
				new Figure(
					"Caillou",
					Enumerable.Repeat(Digit.Any, 5),
					figure => figure.Name == "Ciseaux"
				),				
				new Figure(
					"Ciseaux",
					new Digit[] {Digit.Index, Digit.Majeur},
					figure => figure.Name == "Papier"
				),				
				new Figure(
					"Pincette",
					new Digit[] {Digit.Pouce, Digit.Majeur, Digit.Annulaire},
					figure => figure.Digits.Count < 3
				)
			};
		}
	}

}
