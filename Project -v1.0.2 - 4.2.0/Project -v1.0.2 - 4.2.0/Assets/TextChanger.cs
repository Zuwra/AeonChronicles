using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {


	[Tooltip("This refers to the index of the text type in the TextHodler Prefab")]
	public int TextType;
	public int LevelNumber;
	TextHolder textHolder;


	// Use this for initialization
	void Start () {
		if (LevelData.getHighestLevel() >= LevelNumber) {

			textHolder = ((GameObject)Resources.Load ("TextHolder")).GetComponent<TextHolder>();
			GetComponent<Text>().text = textHolder.myTexts [TextType].myTexts [Random.Range (0, textHolder.myTexts [TextType].myTexts.Count -1)];

		}
	}
	

}
