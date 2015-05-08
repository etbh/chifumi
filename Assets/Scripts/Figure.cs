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
				System.Func<Digit, bool> isEqualOrAny = _digit => _digit == Digit.Any || _digit == digit;
				if (hand.Count(isEqualOrAny) < Digits.Count(isEqualOrAny))
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
					Enumerable.Repeat(Digit.Any, 4),
					figure => figure.Name == "Ciseaux"
				),				
				new Figure(
					"Ciseaux",
					Enumerable.Repeat(Digit.Any, 4),
					figure => figure.Name == "Papier"
				)
			};
		}
	}

}
