using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class VeteranStats : IComparable<VeteranStats>{

	public string unitType;
	public string UnitName;
	public float damageDone;
	public float mitigatedDamage;
	public int kills;
	public float energyGained;
	public float healingDone;
	public float damageTaken;
	public float misc;
	public bool isWarrior;
	public string backstory;

	public bool Died = false;
	public float DeathTime;
	public UnitManager myUnit;
	public Sprite mySprite;

	public VeteranStats(bool hasName, string myType, bool isW, string myName, int playerNumber, UnitManager theUnit)
	{
		unitType = myType;
		isWarrior = isW;
		mySprite = theUnit.myStats.Icon;
		if (myName != "") {
			UnitName = myName;
		}

		if (hasName) {

			UnitName = RaceNames.getInstance ().getName (myType);

			if ((playerNumber == 1 || playerNumber == 3) &&!theUnit.GetComponent<UnitStats>().isHero) {
				backstory = getBackstory ();
			} else {
				backstory = "";}
		} else {
			UnitName = "";}


	}



	public float calculateScore()
	{
		float score = 0;
		score += damageDone;
		score += mitigatedDamage * 2;
		score += kills * 100;
		score += energyGained;
		score += healingDone;
		score += damageTaken / 2;
		score += misc;


		return score;
	}


	public int CompareTo(VeteranStats obja)
	{


		VeteranStats a = (VeteranStats)obja;


		if (a.calculateScore () < calculateScore()) {
			return -1;
		} else {
			return 1;
		}


	}
	public void UpdamTaken(float amount)
	{damageTaken += amount;}

	public void UpdamageDone(float amount)
	{damageDone += amount;}

	public void UpHealing(float amount)
	{healingDone += amount;}

	public void UpEnergy(float amount)
	{energyGained += amount;}

	public void UpMitigated(float amount)
	{mitigatedDamage += amount;}

	//Random story generator ===================================================================
	//==========================================================================================


	private List<string> jobs = new List<string> (new string[] {"minority shareholder", "nylon importer", "pottery dealer", "produce displayer", 
		"seam reinforcer", "riverbed raker", "skateboard tester", "shelf organizer", "aeronautic sociologist", "systems analyst",
		"nautical consultant", "melon slicer", "garnish applicator", "sweater mender","security guard", "sand appraiser", "warehouse navigator", 
		"sock darner", "pinwheel tester", "pickling assistant", "salve developer", "glove sizer", "buritto appraiser", "microphone tester",
		"towel monogrammer", "gas station hermit", "giraffe basketball team manager", "book laminator", "wolf herder", "accountant",
		"windshield engineer", "overage charger", "baby swaddler", "syrup stirrer", "spine aligner", "circus promoter", "chess reporter",
		"juice extractor", "hemp weaver", "floor waxer", "compliance officer","professional hobo", "shoe reviewer", "plate stacker", "professional panelist", "cow whisperer"});

	private List<string> jobAdj = new List<string> (new string[] {"well known", "famous", "average", "infamous", "secret", "", "", "popular", "failing", "long time", "", "unpopular"});
	private List<string> pastEmotions = new List<string> (new string[] {"proud", "angry", "afraid", "dissapointed", "excited"});
	private List<string> villians = new List<string> (new string[] {"mutant rabbits", "hoodlums", "newsies", "internet pirates", "thugs","coalition agents", "Assassins", "Rebels", "Terrorist"});

	private List<string> negativeVerbs = new List<string> (new string[] {"cannibilized","Blackmailed","abducted", "hospitalized","kidnapped","killed", "confounded", "tickled", "robbed", "scammed", "murdered", "assassinated"});

	private List<string> office = new List<string> (new string[] {"assistant to the assistant manager", "folding landry", "potato scrubber", "washing dishes", "inspecting machinery", "Quartermaster"
		,"carrying heavy things", "cleaning the commons", "debugging the internet", "creating the best video game there ever was", "loading Ammunition"});

	private List<string> nicknames = new List<string> (new string[] {"the Slayer","the Rock", "the Hulk", "the Florest", "Heisenberg", "Ace, for how often he won in poker", "Hot Shot",
	"The Tenderizer", "Dead Meat" , "Meatloaf", "Momma's Boy", "the Legman", "Hawk Eye", "Rip Jaw", "Buba"});



	private List<string> thingsToDo = new List<string> (new string[] {"play baseball", "climb trees", "take long romantic walks", "hide in the grass", "watch clouds turn into rain",
		"recite the perodic table","compose Renga", "practice the violin", "play catch with eggs", "steal whatever isn't nailed down", "follow rainbows", "read horoscopes", "Read Books"});
	


	private List<string>  spyJob = new List<string> (new string[] {"He was approached with an offer to join the army as a spy for ", "He was formerly an operative for ",
		"Before he came down with a serious case of gout, he had been an agent for "});

	private List<string> factions = new List<string> (new string[] {"the Bunny Conversation Society", "the Coalition", "Alfredo Sauce United", "the Infinite Consortium", "the Cypher Police", 
		"the Dark Brotherhood", "the Brotherhood of Nod", "the Brony Brotherhood", "the Gadgezhan Bruisers", "the Robot Mafia", "the Jintori", "the A'tane"});

	private List<string>  fellowReactions = new List<string> (new string[] {"Most of his fellow soldiers are cool with it. ",
		"Noone suspects a thing. ", "He will one day reveal himself to be old man Jenkins. ", "He found the pay as a soldier was much better so he quit the spy job. ",
		"He has plans to soon become a triple or maybe even a quadruple agent. " });

	private List<string>  bets = new List<string> (new string[] {"Poker", "Competitive Dice Rolling", "Horse Race", "Chess Tournament" });
	private List<string>  bodyParts = new List<string> (new string[] {"arm","ear", "leg", "pancreas", "lung", "finger", "hand", "hand", "brain lobe", "rib", });

	private List<string>  questions = new List<string> (new string[] {"Sometimes he questions his own existance, ",  "He sometimes wonders if there's more out there, " ,
	" He often doubts his potential, "});
	private List<string>  parallel = new List<string> (new string[] {"and wonders if there is some parallel Universe with a different version of himself. ", 
		" and if he could have had more pie.", " and if its all even really worth it."});

	private List<string>  starter = new List<string> (new string[] {"Before joining the army ","Back in his hometown, ", "For a long time, ", 
		"Since he was a child ", "Even though he would never admit it ", "Years before he joined SteelCrest " });

	private List<string>  storyChange = new List<string> (new string[] {"only one step behind. " , "only he never had the privilidge of being an all powerful eye in the sky. " ,
		"except for the part where he joined the army instead of playing video games all the time. ", " except for the part where he was fated to die in an upcomng battle. "
	,"and he means to take your place. ", "only with less cake and more portals. "});


	private List<string>  terrors = new List<string> (new string[] {
		" He's seen things.  Things you couldn't possibly imagine...   Terrible things that would haunt your dreams. Nightmares that wake him in a pool of sweat. ",
		" Every time he closes his eyes at night, abstract horrors and memories are all he sees... Shapes ... Colors.... Sounds... its all too vague to describe. ", 
		" He has done terrible things which he can't write home about. He can't even talk to dog about them. Most people just think he's mute because of how little he talks. Maybe he is. ", 
		" He wonders when the war will end, when his commanders will stop sending him into battle after battle for reasons he doesn't understand. "});

	private List<string> childhood = new List<string> (new string[] { "nor does he care to.", "on account of a serious case of amenesia.","but sometimes he has visions of the future.", 
		"due to a serious alien probing.", "or the orphanage where he grew up." , "due to him having that benjamin button disease.", "or even what he had for breakfast." });

	private List<string> escaped= new List<string> (new string[] { "by faking his own death", "","somehow", "through an air duct", "after being turned into a newt (he got better)", 
		"by tunneling his way out", "by following the East Sea current", "with the help of his future self", "by murdering everyone" });

	private List<string> additions=  new List<string> (new string[] { ", In addition to manning a deadly war machine. ", ". ", ". ", ", as well as managing the arsenal lockers. " 
		," due to military incompetance. ", " in addition to manning heavy machinery. " + " because he didn't know how to do much else. ", " but he hijacked this vehicle for the day. " });

	private List<string> OvercomeFear=  new List<string> (new string[] {", so he joined the army get over his fear. ", ", so he enlisted to grow more as a person. ",
		", but he doesn't think he'll ever get over his fear. ", ", which his peers made fun of him for as a child. ", " even though people said they don't actually exist. "});

	public string getIntro()
	{int rand = UnityEngine.Random.Range (0, 7);
		if(rand == 0)
			return getSentanceA();
		if(rand == 1)
			return getSentanceB();
		if(rand == 2)
			return getSentanceC();
		if(rand == 3)
			return getSentanceD();
		if (rand == 4) {
			return getSentanceE ();
		}
		if (rand == 5) {
			return getSentanceZ ();
				}
		if (rand == 6) {
			return getSentanceY ();
		}
		
		return getReason();

	}



	public string getReason()
	{int rand = UnityEngine.Random.Range (0, 9);
		if(rand == 0)
			return getSentanceF();
		if(rand == 1)
			return getSentanceG();
		if(rand == 2)
			return getSentanceH();
		if(rand == 3)
			return getSentanceI();
		if(rand == 4)
			return getSentanceI();
		if(rand == 5)
			return getSentanceJ();
		if(rand == 6)
			return getSentanceJJ();
		if (rand == 7)
			return getThird();
		
		return getThird();
	}

	public string getThird()
	{int rand = UnityEngine.Random.Range (0, 7);
		if(rand == 0)
			return getSentanceM();
		if(rand == 1)
			return getSentanceN();
		if(rand == 2)
			return getSentanceO();
		if(rand == 3)
			return getSentanceP();
		if(rand == 4)
			return getSentanceQ();
		if(rand == 5)
			return getSentanceAA();
		if (rand == 6)
			return "";
		return "";
	}



	//============================== Intro =========================================================
	private string getSentanceA()
	{
		string s =  starter [UnityEngine.Random.Range (0, starter.Count )] + UnitName + " was a "+ jobAdj[UnityEngine.Random.Range (0, jobAdj.Count )] + " " + jobs [UnityEngine.Random.Range (0, jobs.Count )] + ". ";
		s +=  getReason ();
		return s;
	}

	private string getSentanceB()
	{
		string s = UnitName + " had always been very "+ pastEmotions[UnityEngine.Random.Range (0, pastEmotions.Count )] + " as a "+ jobAdj[UnityEngine.Random.Range (0, jobAdj.Count )] + " " + jobs [UnityEngine.Random.Range (0, jobs.Count)] + ". ";
		s += getReason ();
		return s;
	}

	private string getSentanceC()
	{
		string s = "At age " + UnityEngine.Random.Range(6,16) +" " + UnitName + " took up the family tradition of being a " + jobs [UnityEngine.Random.Range (0, jobs.Count)] + ". ";
		s +=  getReason ();
		return s;
	}

	private string getSentanceD()
	{
		string s = UnitName + "'s mother and father were so "+pastEmotions[UnityEngine.Random.Range (0, pastEmotions.Count )]+" when he left his old job as a " 
			+ jobs [UnityEngine.Random.Range (0, jobs.Count)] + " to become a member of the army. ";
		s += getReason ();
		return s;
	}


	private string getSentanceE()
	{
		string s = UnitName + " has been in the army his whole life." + terrors[UnityEngine.Random.Range (0, terrors.Count )] +" I hope you never have to endure what he has....";
		return s;
	}

	private string getSentanceZ()
	{
		string s = UnitName + "'s story is the same as yours, " + storyChange [UnityEngine.Random.Range (0, storyChange.Count )];
		s += getReason ();
		return s;
	}

	private string getSentanceY()
	{
		string s = UnitName + " doesn't remember much of his childhood, " +   childhood[UnityEngine.Random.Range (0, childhood.Count)];
		s += getReason ();
		return s;
	}
		

	//=================================REASON THEY ARE HERE===========================================

	private string getSentanceF()
	{
		string s = UnitName + "'s entire family was " + negativeVerbs [UnityEngine.Random.Range (0, negativeVerbs.Count)] + " by a gang of " + villians [UnityEngine.Random.Range (0, villians.Count)] + 
			", and he's here for revenge. ";
		s += getThird ();
		return s;
	}

	private string getSentanceG()
	{
		string s ="He was never able to reach his dream of being a " + jobs [UnityEngine.Random.Range (0, jobs.Count )] + ", so he settled for joining the army. ";
		s += getThird ();
		return s;
	}

	private string getSentanceH()
	{
		string s = "He lost a "+ bets [UnityEngine.Random.Range (0, bets.Count )] + " bet and had to enlist in the army. ";
		s += getThird ();
		return s;
	}


	private string getSentanceI()
	{
		string s ="He was always afraid of " + villians [UnityEngine.Random.Range (0, villians.Count )]+ OvercomeFear [UnityEngine.Random.Range (0, OvercomeFear.Count )] ;
		s += getThird ();
		return s;
	}

	private string getSentanceII()
	{
		string s ="When things didn't work out back home, he got mixed up with the " + factions [UnityEngine.Random.Range (0, factions.Count )] +
			". He escaped " + escaped [UnityEngine.Random.Range (0, escaped.Count )]  +" then joined the army. ";
		s += getThird ();
		return s;
	}

	private string getSentanceJ()
	{
		string s = spyJob[UnityEngine.Random.Range (0, spyJob.Count )]  
			+ factions [UnityEngine.Random.Range (0, factions.Count )] + ". " + fellowReactions[UnityEngine.Random.Range (0, fellowReactions.Count )];
		s += getThird ();
		return s;
	}

	private string getSentanceJJ()
	{
		string s =  "He had been on the run for years from " + factions [UnityEngine.Random.Range (0, factions.Count )] + " before he escaped by joining the military. ";
		s += getThird ();
		return s;
	}

	//============================================================ Third ===========================================================
	private string getSentanceM()
	{
		string s = "After joining the army he was quickly given the duty of " + office [UnityEngine.Random.Range (0, office.Count )]+ 
			additions[UnityEngine.Random.Range (0, additions.Count)];
		return s;
	}

	private string getSentanceN()
	{
		string s = "While in the army he gained the nick name of " + nicknames [UnityEngine.Random.Range (0, nicknames.Count)] + ". ";
		return s;
	}

	private string getSentanceO()
	{
		string s = "During his free time " + UnitName + " likes to " + thingsToDo[UnityEngine.Random.Range (0, thingsToDo.Count )] +". ";
		return s;
	}

	private string getSentanceP()
	{
		string s = "After he retires, " + UnitName + " plans to go to grad school to become a " + jobs [UnityEngine.Random.Range (0, jobs.Count )] +". ";
		return s;
	}

	private string getSentanceQ()
	{
		string s = questions [UnityEngine.Random.Range (0, questions.Count)]  + parallel [UnityEngine.Random.Range (0, parallel.Count )] ;
		return s;
	}

	private string getSentanceAA()
	{
		string s = "Even though he lost a " +bodyParts [UnityEngine.Random.Range (0, bodyParts.Count )] + " during combat, " + UnitName +" still serves proudly. ";
		return s;
	}
	private string getSentanceBB()
	{
		string s = UnitName + " got his "+bodyParts [UnityEngine.Random.Range (0, bodyParts.Count)] + " shot off and he has never been the same. ";
		return s;
	}

	//=============================================================================================

	private string getBackstory ()
	{
		return getIntro ();
	
	}
}