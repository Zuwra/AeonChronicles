using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Objective))]
public class ObjectiveEditor :Editor{


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		if (GUILayout.Button ("Start Objective")) {
			((Objective)target).BeginObjective ();

		}

		if (GUILayout.Button ("Finish Objective")) {
			((Objective)target).complete();

		}
	}

}

