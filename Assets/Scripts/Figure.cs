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
				),
				new Figure(
					"Spider",
					new Digit[] {Digit.Pouce, Digit.Index, Digit.Auriculaire},
					figure => figure.Digits.Count >= 3
				),
				new Figure(
					"Flingue",
					new Digit[] {Digit.Pouce, Digit.Index},
					figure => new string[]{"Spock", "Nabilla", "Spider"}.Contains(figure.Name)
				),
				new Figure(
					"Spock",
					new Digit[] {Digit.Pouce, Digit.Index, Digit.Majeur, Digit.Annulaire, Digit.Auriculaire},
					figure => new string[]{"Metal", "Pouce", "Fuck"}.Contains(figure.Name)
				),
				new Figure(
					"Pouce",
					new Digit[] {Digit.Pouce},
					figure => new string[]{"Metal", "Flingue", "Fuck"}.Contains(figure.Name)
				),
				new Figure(
					"Metal",
					new Digit[] {Digit.Index, Digit.Auriculaire},
					figure => new string[]{"Caillou", "Nabilla"}.Contains(figure.Name)
				),
				new Figure(
					"Fuck",
					new Digit[] {Digit.Majeur},
					figure => figure.Digits.Contains(Digit.Majeur)
				),
				new Figure(
					"Nabilla",
					new Digit[] {Digit.Pouce, Digit.Auriculaire},
					figure => figure.Digits.Contains(Digit.Majeur)
				)
			};
		}
	}

}
