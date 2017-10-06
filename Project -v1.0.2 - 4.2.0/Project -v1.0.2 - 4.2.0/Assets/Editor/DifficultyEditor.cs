using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DifficultyEditor : EditorWindow {

	private DifficultyManager DM;
	private string percentEasy = "25";
	private string percentMedium = "15";

	[MenuItem("Window/Unit Difficulty Editor")]
	public static void showWindow() {
		EditorWindow.GetWindow (typeof(DifficultyEditor));
	}

	void OnGUI(){

	
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Add To Easy"))
		{
			Transform[] transforms = Selection.transforms;
			foreach (Transform t in transforms) {
				Debug.Log (t.gameObject);
				UnitManager um = t.gameObject.GetComponent<UnitManager> ();
				if (um) {
					if (um.PlayerOwner == 2) {
						if (!DM.deleteOnEasy.Contains (t.gameObject) && !DM.deleteOnMedium.Contains(t.gameObject)) {
							DM.deleteOnEasy.Add (t.gameObject);
						}
					}
				}
			}
		}

		if(GUILayout.Button("Add To Medium"))
		{
			Transform[] transforms = Selection.transforms;
			foreach (Transform t in transforms) {
				Debug.Log (t.gameObject);
				UnitManager um = t.gameObject.GetComponent<UnitManager> ();
				if (um) {
					if (um.PlayerOwner == 2) {
						if (!DM.deleteOnMedium.Contains (t.gameObject) && !DM.deleteOnEasy.Contains (t.gameObject)) {
							DM.deleteOnMedium.Add (t.gameObject);
						}
					}
				}
			}
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Remove from Easy"))
		{
			Transform[] transforms = Selection.transforms;
			foreach (Transform t in transforms) {
				Debug.Log (t.gameObject);
				UnitManager um = t.gameObject.GetComponent<UnitManager> ();
				if (um) {
					if (um.PlayerOwner == 2) {
						if (DM.deleteOnEasy.Contains (t.gameObject)) {
							DM.deleteOnEasy.Remove (t.gameObject);
						}
					}
				}
			}
		}

		if(GUILayout.Button("Remove from Medium"))
		{
			Transform[] transforms = Selection.transforms;
			foreach (Transform t in transforms) {
				Debug.Log (t.gameObject);
				UnitManager um = t.gameObject.GetComponent<UnitManager> ();
				if (um) {
					if (um.PlayerOwner == 2) {
						if (DM.deleteOnMedium.Contains (t.gameObject)) {
							DM.deleteOnMedium.Remove (t.gameObject);
						}
					}
				}
			}
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Clear Easy List"))
		{
			DM.deleteOnEasy.Clear ();
		}

		if(GUILayout.Button("Clear Medium List"))
		{
			DM.deleteOnMedium.Clear ();
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Percent to remove: ");
		percentEasy = EditorGUILayout.TextField(percentEasy);
		if(GUILayout.Button("Generate Random Easy  "))
		{
			List<GameObject> newDeleteOnEasy = new List<GameObject>();
			foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner == 2 && !man.GetComponent<UnitStats>().otherTags.Contains(UnitTypes.UnitTypeTag.Structure)) {
					newDeleteOnEasy.Add (man.gameObject);
				}
			}
			int originalCount = newDeleteOnEasy.Count;
			foreach (GameObject obj in DM.deleteOnMedium) {
				newDeleteOnEasy.Remove (obj);
			}
			double percent = Math.Abs((double.Parse (percentEasy)) / 100);
			int numberToKeep = (int) (originalCount * percent);
			Debug.Log (numberToKeep);
			List<GameObject> replaceDeleteOnEasy = new List<GameObject>();
			if (numberToKeep < newDeleteOnEasy.Count) {		
				for (int i = 0; i < numberToKeep; i++) {
					int rand = UnityEngine.Random.Range (0, newDeleteOnEasy.Count - 1);
					replaceDeleteOnEasy.Add (newDeleteOnEasy [rand]);
					newDeleteOnEasy.RemoveAt (rand);
				}
			} else
				replaceDeleteOnEasy = newDeleteOnEasy;
			DM.deleteOnEasy = replaceDeleteOnEasy;
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Percent to remove: ");
		percentMedium = EditorGUILayout.TextField(percentMedium);
		if(GUILayout.Button("Generate Random Medium"))
		{
			List<GameObject> newDeleteOnMedium = new List<GameObject>();
			foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner == 2 && !man.GetComponent<UnitStats>().otherTags.Contains(UnitTypes.UnitTypeTag.Structure)) {
					newDeleteOnMedium.Add (man.gameObject);
				}
			}
			int originalCount = newDeleteOnMedium.Count;
			foreach (GameObject obj in DM.deleteOnEasy) {
				newDeleteOnMedium.Remove (obj);
			}
			double percent = Math.Abs((double.Parse (percentMedium)) / 100);
			int numberToKeep = (int) (originalCount * percent);
			Debug.Log (numberToKeep);
			List<GameObject> replaceDeleteOnMedium = new List<GameObject>();
			if (numberToKeep < newDeleteOnMedium.Count) {		
				for (int i = 0; i < numberToKeep; i++) {
					int rand = UnityEngine.Random.Range (0, newDeleteOnMedium.Count - 1);
					replaceDeleteOnMedium.Add (newDeleteOnMedium [rand]);
					newDeleteOnMedium.RemoveAt (rand);
				}
			} else
				replaceDeleteOnMedium = newDeleteOnMedium;
			DM.deleteOnMedium = replaceDeleteOnMedium;
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Select All Easy"))
		{
			Selection.objects = DM.deleteOnEasy.ToArray();
		}

		if(GUILayout.Button("Select All Medium"))
		{
			Selection.objects = DM.deleteOnMedium.ToArray();
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Make Units Invisible - Easy"))
		{
			foreach (GameObject obj in DM.deleteOnEasy) {
				obj.SetActive (false);
			}
		}

		if(GUILayout.Button("Make Units Invisible - Medium"))
		{
			foreach (GameObject obj in DM.deleteOnMedium) {
				obj.SetActive (false);
			}
		}

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if(GUILayout.Button("Make Units visible - Easy"))
		{
			foreach (GameObject obj in DM.deleteOnEasy) {
				obj.SetActive (true);
			}
		}

		if(GUILayout.Button("Make Units visible - Medium"))
		{
			foreach (GameObject obj in DM.deleteOnMedium) {
				obj.SetActive (true);
			}
		}
		GUILayout.EndHorizontal ();

		if(GUILayout.Button("Matthew Loves Jordan"))
		{
			Debug.Log("yay :D");
		}

		if(GUILayout.Button("<3"))
		{
			Debug.Log ("awwwwwn <3");
		}


		replaceUnit = EditorGUILayout.TextField("Replace Unit Name" , replaceUnit);
		if (GUILayout.Button ("Replace Unit")) {
			if (Selection.objects.Length > 0) {

				GameObject prefab = (GameObject)PrefabUtility.GetPrefabParent (Selection.objects [0]);

				foreach (UnitManager manage in GameObject.FindObjectsOfType<UnitManager>()) {
					if (manage.UnitName == replaceUnit) {

						GameObject obj =  (GameObject)Instantiate (Selection.objects [0], manage.transform.position, manage.transform.rotation, manage.transform.parent);
						obj =  PrefabUtility.ConnectGameObjectToPrefab(obj,  prefab);
						obj.transform.position = manage.transform.position;
						DestroyImmediate (manage.gameObject);
				
					}
				}
			}
		
		}


	}

	string replaceUnit = "";

	void Awake() {
		//when open up window this code runs
		DM = GameObject.FindObjectOfType<DifficultyManager>();
	}

	////add to easy
	////add to medium
	//get refernece to selected gameobjects in the edditor
	//make sure the reference has a game manager and is an enemy. player 2 in unit manager

}
