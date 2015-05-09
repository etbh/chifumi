using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/*
public abstract class BaseFigure<FigureLosing> : Figure{

	public override List<Digit> digits{
		get{
			return Enumerable.Repeat(Digit.Any, 4).ToList();
		}
	}
	
	public override bool WinsAgainst(Figure opponent){
		return opponent.GetType() == typeof(FigureLosing);

	}

}

public class Paper : BaseFigure<Rock> { }
	
public class Rock : BaseFigure<Cissors> { }

public class Cissors : BaseFigure<Paper> { }


public class Tweezers : Figure {

	public override List<Digit> digits{
		get{
			return new List<Digit>(){
				Digit.Pouce,
				Digit.Majeur,
				Digit.Annulaire
			};
		}
	}
	
	public override bool WinsAgainst(Figure opponent){
		return opponent.Digits.Count < 3;
	}
}

public class SpiderMan : Figure {

	public override List<Digit> digits{
		get{
			return new List<Digit>(){
				Digit.Pouce,
				Digit.Index,
				Digit.Auriculaire
			};
		}
	}

	public override bool WinsAgainst(Figure opponent){
		return opponent.Digits.Count >= 3;
	}
}

public class Gun : Figure {
	
	public override List<Digit> digits{
		get{
			return new List<Digit>(){
				Digit.Pouce,
				Digit.Index
			};
		}
	}
	
	public override bool WinsAgainst(Figure opponent){
		return opponent.GetType() == typeof(Gun)
		//	|| opponent.GetType() == typeof(Nabilla)
			|| opponent.GetType() == typeof(SpiderMan);
		//	|| opponent.GetType() == typeof(Spock);
	}
}
*/