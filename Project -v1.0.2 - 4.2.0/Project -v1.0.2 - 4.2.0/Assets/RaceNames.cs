using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceNames  {


	private static RaceNames main;
	public static RaceNames getInstance()
	{if (main == null) {
			main = new RaceNames ();
		}
		return main;
		
	}

	string[] LOneranks = new string[6]{"Initiate", "Trainee", "Cadet", "Recruit", "Newbie", "Private"};
	string[] LTworanks = new string[4]{"Strike Commander", "Captain", "Seargant","Caliph" };
	string[] LThreeranks = new string[4]{"Corporal", "Colonel", "Veteran", "Lieutenant"};
	string[] LFourranks = new string[4]{"Grand General", "Major General",  "Minor general", "Admiral"};
	string[] LFiveranks = new string[4]{"Praetor",  "Primus", "Executor", "Tribune"};
	string[] LSixranks = new string[6]{"Fresh Prince","Altimeter", "Quaestionarius",  "Commander in chief", "High lord of the Universe", "Supreme Commander"};


	 string[] names = new string[35]{"Aelia","Caryn","Jace", "Chauren","Optimus","Tamra", "Aetius", "Carya", "Emano","Taciton",
		"Cato","Varus", "Domitio", "Bentin", "Silvanus", "Valerian", "Porso", "Agricola", "Metallan", "Timon", 
		"Eton", "Adamus", "Soren", "Mathou", "Jordana", "Colton", "Rheinhardt", "Zostro", "Akimus","Comodus",
		"Gisborne","Porsche", "Hanzo", "Torbous", "Talion"};

	string[] bunnyNames = new string[36]{ "bunnykins", "cotton candy", "fluffy", "buffalo", "scamper", "thumper", "Oliver", "lingo", "Easter"
		, "Themistocles-consumer of clovers", "hopper", "nibbler", "Lola", "Harvey", "Roger", "bugs","muncher",  "Judie Hopps", "zappy", "fluffer", 
		"fluffluff", "fuzzy eyes", "Long Ears", "short ears", "mushu", "chomper", "Trixy", "carrot face", "fluff butt", "fuzzy bunny", 
		"cottontail" , "dumb bunny", "clever bunny", "sniffles", "radar", "yoshi"};



	public string getName(string UnitType)
	{//index++;
		//if (index > names.Length-1) {
		
		//	index = 0;}
		//return names [index];

		if (UnitType.Contains ("Bun")) {
			return bunnyNames[Random.Range(0, bunnyNames.Length -1)];
		}

		return names[Random.Range(0, names.Length -1)];

	}

	public string getRank(int n)
	{
		if (n < 2) {
			return LOneranks [Random.Range(0, LOneranks.Length -1 )];
		} else if (n < 4) {
			return LTworanks [Random.Range(0, LTworanks.Length -1)];
		} else if (n < 7) {
			return LThreeranks [Random.Range(0, LThreeranks.Length -1)];
		} else if (n < 12) {
			return LFourranks [Random.Range(0, LFourranks.Length -1)];
		} 
		else if (n < 18) {
			return LFiveranks [Random.Range(0, LFiveranks.Length -1)];
		}
		else {
			return LSixranks [Random.Range(0, LSixranks.Length -1)];
		}
	}
}
