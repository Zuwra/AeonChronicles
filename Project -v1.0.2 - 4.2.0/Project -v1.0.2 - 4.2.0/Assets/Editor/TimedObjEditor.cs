using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimedObjective))]
	public class TimedObjEditor :Editor{


		public override void OnInspectorGUI()
		{
			DrawDefaultInspector ();
			if (GUILayout.Button ("Start Objective")) {
			((TimedObjective)target).BeginObjective ();

			}

			if (GUILayout.Button ("Finish Objective")) {
			((TimedObjective)target).complete();

			}
		}

	}