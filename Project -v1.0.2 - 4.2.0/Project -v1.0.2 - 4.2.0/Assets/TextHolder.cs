using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TextHolder : MonoBehaviour {

	public List<TextType> myTexts;

	[Serializable]
	public class TextType
	{
		public string Description;
		[TextArea(2,10)]
		public List<string> myTexts = new List<string>();
	}



}
