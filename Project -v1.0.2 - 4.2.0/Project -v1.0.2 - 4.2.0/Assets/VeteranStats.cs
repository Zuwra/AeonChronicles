using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

	public VeteranStats(bool hasName, string myType, bool isW, string myName)
	{
		unitType = myType;
		isWarrior = isW;

		if (myName != "") {
			UnitName = myName;
		}

		if (hasName) {

			UnitName = RaceNames.getInstance ().getName ();
			backstory = getBackstory ();
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
	private List<string> villians = new List<string> (new string[] {"mutant rabbits", "hoodlums", "newsies", "internet pirates", "garden gnomes", "thugs", "Assasins", "Rebels", "Terrorist"});

	private List<string> negativeVerbs = new List<string> (new string[] {"cannibilized", "kidnapped","killed", "confounded", "tickled", "robbed", "scammed", "murdered", "assasinated"});

	private List<string> office = new List<string> (new string[] {"assistant to the assistant manager", "folding landry", "potato scrubber", "washing dishes", "inspecting machinery", "Quartermaster"
		,"carrying heavy things", "cleaning the commons", "debugging the internet", "creating the best video game there ever was", "loading Ammunition"});

	private List<string> nicknames = new List<string> (new string[] {"the Slayer","the Rock", "the Hulk", "the Florest", "Heisenberg", "Ace, for how often he won in poker", "Hot Shot",
	"The Tenderizer", "Dead Meat" , "Meatloaf", "Momma's Boy", "the Legman", "Hawk Eye"});



	private List<string> thingsToDo = new List<string> (new string[] {"play baseball", "climb trees", "take long romantic walks", "hide in the grass", "watch clouds turn into rain",
		"recite the perodic table","compose Renga", "practice the violin", "play catch with eggs", "steal whatever isn't nailed down", "follow rainbows"});
	
	private List<string> factions = new List<string> (new string[] {"the Bunny Conversation Society", "the Coalition", "Alfredo Sauce United", "the Infinite Consortium", "the Cypher Police", 
		"the Dark Brotherhood", "the Brotherhood of Nod", "the Brony Brotherhood", "the Gadgezhan Bruisers", "the Robot Mafia", "the Jintori", "the A'tane"});

	private List<string>  fellowReactions = new List<string> (new string[] {"Most of his fellow soldiers are cool with it.",
		"Noone suspects a thing", "He will one day reveal himself to be old man Jenkins", "He found the pay was much better so he quit the spy job.",
		"He has plans to soon become a triple or maybe even a quadruple agent." });

	private List<string>  bets = new List<string> (new string[] {"Poker", "Competitive Dice Rolling", "Horse Race", "Chess Tournament" });
	private List<string>  bodyParts = new List<string> (new string[] {"Arm","Ear", "Leg", "Pancreas", "Lung", "Finger", "Hand", "Hand", "brain lobe", "rib", });



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
			int randA = UnityEngine.Random.Range (0, 1);
			if (randA == 0) {
				return getSentanceE ();
			} else {
				return getIntro ();
			}
		}
		if (rand == 5) {
			int randA = UnityEngine.Random.Range (0, 1);
			if (randA == 0) {
				return getSentanceZ ();
			} else {
				return getIntro ();
			}
		}
		if (rand == 6)
			return getReason();
		if (rand == 7)
			return getSentanceY ();
		
		return getReason();

	}



	public string getReason()
	{int rand = UnityEngine.Random.Range (0, 5);
		if(rand == 0)
			return getSentanceF();
		if(rand == 1)
			return getSentanceG();
		if(rand == 2)
			return getSentanceH();
		if(rand == 3)
			return getSentanceI();
		if(rand == 4)
			return getSentanceJ();
		if (rand == 5)
			return getThird();
		return getThird();
	}

	public string getThird()
	{int rand = UnityEngine.Random.Range (0, 6);
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
		string s = "Before joining the corp " + UnitName + " was a "+ jobAdj[UnityEngine.Random.Range (0, jobAdj.Count - 1)] + " " + jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] + ". ";
		s +=  getReason ();
		return s;
	}

	private string getSentanceB()
	{
		string s = "Back in his hometown, "  + UnitName + " was a "+ jobAdj[UnityEngine.Random.Range (0, jobAdj.Count - 1)] + " " + jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] + ". ";
		s += getReason ();
		return s;
	}

	private string getSentanceC()
	{
		string s = "At age 6 " + UnitName + " took up the family tradition of being a " + jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] + ". ";
		s +=  getReason ();
		return s;
	}

	private string getSentanceD()
	{
		string s = UnitName + "'s mother and father were so "+pastEmotions[UnityEngine.Random.Range (0, pastEmotions.Count - 1)]+" when he left his old job as a " 
			+ jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] + " to become a member of the corp. ";
		s += getReason ();
		return s;
	}


	private string getSentanceE()
	{
		string s = UnitName + " has been in the corp his whole life.  He's seen things.  Things you couldn't possibly imagine...   Vague things that would haunt your dreams. " +
			"Every time he closes his eyes at night, abstract horrors and memories are all he sees...  I hope you never have to endure what he has....";
		return s;
	}

	private string getSentanceZ()
	{
		string s = UnitName + "'s story is the same as yours, only one step behind. ";
		s += getReason ();
		return s;
	}

	private string getSentanceY()
	{
		string s = UnitName + " doesn't remember much of his childhood, nor does he care to. ";
		s += getReason ();
		return s;
	}
		

	//=================================REASON THEY ARE HERE===========================================

	private string getSentanceF()
	{
		string s = UnitName + "'s entire family was " + negativeVerbs [UnityEngine.Random.Range (0, negativeVerbs.Count - 1)] + " by a gang of " + villians [UnityEngine.Random.Range (0, villians.Count - 1)] + 
			", and he's here to take their revenge. ";
		s += getThird ();
		return s;
	}

	private string getSentanceG()
	{
		string s ="He was never able to reach his dream of being a " + jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] + ", so he settled for joining the corp. ";
		s += getThird ();
		return s;
	}

	private string getSentanceH()
	{
		string s = "He lost a "+ bets [UnityEngine.Random.Range (0, bets.Count - 1)] + " bet and had to enlist in the corp. ";
		s += getThird ();
		return s;
	}


	private string getSentanceI()
	{
		string s ="He was always afraid of " + villians [UnityEngine.Random.Range (0, villians.Count - 1)]+  ", so he joined the corp get over his fear. ";
		s += getThird ();
		return s;
	}

	private string getSentanceJ()
	{
		string s = "He was approched with an offer to join the corp as a spy for " 
			+ factions [UnityEngine.Random.Range (0, factions.Count - 1)] + " . " + fellowReactions[UnityEngine.Random.Range (0, fellowReactions.Count - 1)];
		s += getThird ();
		return s;
	}

	//============================================================ Third ===========================================================
	private string getSentanceM()
	{
		string s = "After joining the corp he was quikly was given the duty of " + office [UnityEngine.Random.Range (0, office.Count - 1)]+ ", In addition to manning a deadly war machine. ";
		return s;
	}

	private string getSentanceN()
	{
		string s = "While in the corp he gained the nick name of " + nicknames [UnityEngine.Random.Range (0, nicknames.Count - 1)] + ". ";
		return s;
	}

	private string getSentanceO()
	{
		string s = "During his free time " + UnitName + " likes to " + thingsToDo[UnityEngine.Random.Range (0, thingsToDo.Count - 1)] +". ";
		return s;
	}

	private string getSentanceP()
	{
		string s = "After he retires, " + UnitName + " plans to go to grad school to become a " + jobs [UnityEngine.Random.Range (0, jobs.Count - 1)] +". ";
		return s;
	}

	private string getSentanceQ()
	{
		string s = "Sometimes he questions his own existance, and if there is some parallel Universe with a different version of himself. ";
		return s;
	}

	private string getSentanceAA()
	{
		string s = "Even though he lost a " +bodyParts [UnityEngine.Random.Range (0, bodyParts.Count - 1)] + " during combat," + UnitName +" still serves proudly.";
		return s;
	}
	private string getSentanceBB()
	{
		string s = UnitName + " got his "+bodyParts [UnityEngine.Random.Range (0, bodyParts.Count - 1)] + " shot off and he has never been the same. ";
		return s;
	}

	//=============================================================================================

	private string getBackstory ()
	{
		return getIntro ();
	
	}
}