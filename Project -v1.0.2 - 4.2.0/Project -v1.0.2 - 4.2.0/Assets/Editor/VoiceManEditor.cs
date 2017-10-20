using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(dialogManager))]
	public class VoiceManEditor : Editor {


	int n;
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector ();

		n = int.Parse( GUILayout.TextField (n.ToString()));
		if (GUILayout.Button ("Add new Line")) {
			((dialogManager)target).VoiceLines.Insert (n, new DialogLine ());

			foreach (TextTrigger trig in GameObject.FindObjectsOfType<TextTrigger>()) {
	

				for (int i = 0; i < trig.VoiceLines.Count; i++) {
					if (trig.VoiceLines [i] > n) {
						trig.VoiceLines [i]++;
					}
				}
					
			}
		
		}

		if (GUILayout.Button ("Remove Line")) {
			((dialogManager)target).VoiceLines.RemoveAt(n);

			foreach (TextTrigger trig in GameObject.FindObjectsOfType<TextTrigger>()) {
		

				for (int i = 0; i < trig.VoiceLines.Count; i++) {
					if (trig.VoiceLines [i] > n) {
						trig.VoiceLines [i]--;
					}
				}
			}

		}
	
		}
	}
