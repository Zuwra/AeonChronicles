using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(VictoryTrigger))]
public class VictoryScriptEditor : Editor {

	dialogManager dialog;
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		if (!dialog) {
			dialog = GameObject.FindObjectOfType<dialogManager> ();
			//Debug.Log ("Found " + dialog.gameObject);
		}

		List<string> s = dialog.getVoiceTitleList ();
	
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Add Win Line")) {
			if (((VictoryTrigger)target).winLine.Count == 0) {
				((VictoryTrigger)target).winLine.Add (0);
			} else {

				int n = ((VictoryTrigger)target).winLine.Count - 1;
				n =((VictoryTrigger)target).winLine[n] + 1;
				((VictoryTrigger)target).winLine.Add ( n);
			}
		}
		if (((VictoryTrigger)target).winLine.Count > 0) {
			if (GUILayout.Button ("Remove Last Win Line")) {
				((VictoryTrigger)target).winLine.RemoveAt (((VictoryTrigger)target).winLine.Count - 1);  
			}
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Add Lose Line")) {
			if (((VictoryTrigger)target).loseLine.Count == 0) {
				((VictoryTrigger)target).loseLine.Add (0);
			} else {

				int n = ((VictoryTrigger)target).loseLine.Count - 1;
				n =((VictoryTrigger)target).loseLine[n] + 1;
				((VictoryTrigger)target).loseLine.Add ( n);
			}
		}
		if (((VictoryTrigger)target).loseLine.Count > 0) {
			if (GUILayout.Button ("Remove Last Lose Line")) {
				((VictoryTrigger)target).loseLine.RemoveAt (((VictoryTrigger)target).loseLine.Count - 1);  
			}
		}
		EditorGUILayout.EndHorizontal ();
		for (int i = 0; i < ((VictoryTrigger)target).winLine.Count; i++) {
			((VictoryTrigger)target).winLine[i] = EditorGUILayout.Popup("Win Line " + i + ": ",((VictoryTrigger)target).winLine[i],s.ToArray());

		}

		for (int i = 0; i < ((VictoryTrigger)target).loseLine.Count; i++) {
			((VictoryTrigger)target).loseLine[i] = EditorGUILayout.Popup("Defeat Line " + i + ": ",((VictoryTrigger)target).loseLine[i],s.ToArray());

		}
			
	}
}
