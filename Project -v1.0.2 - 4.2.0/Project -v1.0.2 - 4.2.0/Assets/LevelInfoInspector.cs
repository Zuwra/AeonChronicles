using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCompilation))]
public class LevelInfoInspector : Editor {

	LevelCompilation myTarget;



	public override void OnInspectorGUI()
	{LevelCompilation myTarget = (LevelCompilation)target;
		DrawDefaultInspector ();

	}
}
