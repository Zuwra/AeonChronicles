using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TextTrigger))]
public class TextTrigEditor : Editor {



	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();


		List<string> s = GameObject.FindObjectOfType<dialogManager> ().getVoiceTitleList ();
		((TextTrigger)target).VoiceLineIndex = EditorGUILayout.Popup("Voice Line: ",((TextTrigger)target).VoiceLineIndex,s.ToArray());

	}
}
