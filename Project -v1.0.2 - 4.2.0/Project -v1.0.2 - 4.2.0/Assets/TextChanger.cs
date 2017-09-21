using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {


	public void loadLevelTip(int tipIndex)
	{
		TextHolder  textHolder = ((GameObject)Resources.Load ("TextHolder")).GetComponent<TextHolder>();
		GetComponent<Text> ().text = textHolder.myTexts [0].myTexts[tipIndex];

	}

	public void setRandomTip()
	{
		TextHolder  textHolder = ((GameObject)Resources.Load ("TextHolder")).GetComponent<TextHolder>();
		GetComponent<Text>().text = textHolder.myTexts [0].myTexts [Random.Range (0, textHolder.myTexts [0].myTexts.Count)];
	}
}
