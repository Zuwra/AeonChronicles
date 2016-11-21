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

	string[] ranks = new string[5]{"Initiate" , "Adjutant", "Mentor","Preator" ,"Executor"};
	 string[] names = new string[35]{"Aelia","Caryn","Jace", "Chauren","Optimus","Tamra", "Aetius", "Carya", "Emano","Taciton",
		"Cato","Varus", "Domitio", "Bentin", "Silvanus", "Valerian", "Porso", "Agricola", "Metallan", "Timon", 
		"Eton", "Adamus", "Soren", "Mathou", "Jordana", "Colton", "Rheinhardt", "Zostro", "Akimus","Comodus",
		"Gisborne","Porsche", "Hanzo", "Torbous", "Talion"};



	public string getName()
	{//index++;
		//if (index > names.Length-1) {
		
		//	index = 0;}
		//return names [index];
		return names[Random.Range(0, names.Length)];

	}

	public string getRank(int n)
	{
		if (n < 2) {
			return ranks [0];
		} else if (n < 4) {
			return ranks [1];
		} else if (n < 7) {
			return ranks [2];
		} else if (n < 11) {
			return ranks [3];
		} else {
			return ranks [4];
		}
	}
}
