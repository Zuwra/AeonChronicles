using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogManager : MonoBehaviour {

	[Tooltip("These images should be in order of Hadrian, Katrina, Jarvis, Ludacrus, Paradox,BackSlash,Timmy,Kara")]
	public List<Sprite> myCharacters;

	public List<DialogLine> VoiceLines;

	dialogManager RootPrafab;
	public static dialogManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		RootPrafab = ((GameObject)Resources.Load ("CharacterHolder")).GetComponent<dialogManager>();
	}

	public void playLine (int index)
	{
		ExpositionDisplayer.instance.displayText (VoiceLines[index].MainLine.myText, VoiceLines[index].MainLine.duration, VoiceLines[index].MainLine.myClip, .93f, 
			getCharacter(VoiceLines[index].CharacterImage, VoiceLines[index]),VoiceLines[index].Priority);
	}

	public List<string>  getVoiceTitleList()
	{
		List<string> toReturn = new List<string> ();
		int i = 0;
		foreach (DialogLine line in VoiceLines) {
			if (!string.IsNullOrEmpty (line.summary)) {
				toReturn.Add (line.summary);
			} else {
				toReturn.Add(""+i);
			}
			i++;
		}
		return toReturn;
	}

	Sprite getCharacter(Character character, DialogLine myLine)
	{

		if (myLine.AlternatePic) {
			return myLine.AlternatePic;
		}

		switch (character) {
		case Character.Hadrian:
			return RootPrafab.myCharacters [0];
		case Character.Katrina:
			return RootPrafab.myCharacters [1];
		case Character.Jarvis:
			return RootPrafab.myCharacters [2];
		case Character.Ludacrus:
			return RootPrafab.myCharacters [3];
		case Character.Paradox:
			return RootPrafab.myCharacters [4];
		case Character.BackSlash:
			return RootPrafab.myCharacters [5];
		case Character.Timmy:
			return RootPrafab.myCharacters [6];
		case Character.Kara:
			return RootPrafab.myCharacters [7];

		}

		return null;

	}


}




[System.Serializable]
public class DialogLine
{
	public string summary;
	public Character CharacterImage;
	[Tooltip(" 4 = Announcer level dialog")]
	public int Priority = 4;
	[Tooltip("Only use this if you don't want one of the main characters as the sprite")]
	public Sprite AlternatePic;

	public VoiceInstance MainLine;

	public List<VoiceInstance> alternateLines;


}

[System.Serializable]
public class VoiceInstance
{
	public AudioClip myClip;
	public float duration;
	[TextArea(3,10)]
	public string myText;


}

public enum Character{ Hadrian, Katrina, Jarvis, Ludacrus, Paradox, BackSlash,Timmy,Kara} //Backslash has a sidekick named tilda
