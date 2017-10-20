using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TextTrigger))]
public class TextTrigEditor : Editor {


	dialogManager dialog;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		if (dialog == null) {
			dialog = GameObject.FindObjectOfType<dialogManager> ();
			}


		List<string> s = dialog.getVoiceTitleList ();

		if (GUILayout.Button ("Add Line")) {
			if (((TextTrigger)target).VoiceLines.Count == 0) {
				((TextTrigger)target).VoiceLines.Add (0);
			} else {
				
				int n = ((TextTrigger)target).VoiceLines.Count - 1;
				n = ((TextTrigger)target).VoiceLines [n] + 1;
				((TextTrigger)target).VoiceLines.Add ( n);
			}
		}
		if (GUILayout.Button ("Remove Last Line")) {
			((TextTrigger)target).VoiceLines.RemoveAt(((TextTrigger)target).VoiceLines.Count - 1);  
		}

		for (int i = 0; i < ((TextTrigger)target).VoiceLines.Count; i++) {
			((TextTrigger)target).VoiceLines[i] = EditorGUILayout.Popup("Voice Line " + i + ": ",((TextTrigger)target).VoiceLines[i],s.ToArray());
		
		}
	}
}
