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

	string[] ranks = new string[4]{"Initiate" , "Adjutant", "Mentor", "Executor"};
	 string[] names = new string[21]{"Aelia","Tamra", "Aetius", "Carya", "Emano","Taciton","Cato","Varus", "Domitio", "Bentin", "Silvanus", "Valerian", "Porso", "Agricola", "Metallan", "Timon", "Eton", "Adamus", "Soren", "Mathou", "Jordana" };



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
		}
		else if (n < 5) 
		{
			return ranks [1];
		}
		else if (n < 8) 
		{
			return ranks [2];
		}
		else 
		{
			return ranks [3];
		}
	}
}
