using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(VictoryTrigger))]
public class VictoryScriptEditor : Editor {


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();


		List<string> s = GameObject.FindObjectOfType<dialogManager> ().getVoiceTitleList ();
		((VictoryTrigger)target).winLine = EditorGUILayout.Popup("Victory Line: ",((VictoryTrigger)target).winLine,s.ToArray());


		((VictoryTrigger)target).loseLine = EditorGUILayout.Popup("Defeat Line: ",((VictoryTrigger)target).loseLine,s.ToArray());

	}
}
