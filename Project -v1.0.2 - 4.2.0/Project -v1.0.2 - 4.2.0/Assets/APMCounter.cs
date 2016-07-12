﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class APMCounter : MonoBehaviour {


	public Text counter ;



	private Dictionary<float, int> resOneMap = new Dictionary<float, int>();
	List<float> actions = new List<float>();
	private int totalWorkers = 0;

	private float nextActionTime;
	public int totalActions;

	// Use this for initialization
	void Start () {
		
		nextActionTime = Time.time + 3;


	}

	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {
			updateAPM ();
			totalActions++;
		}


		//Debug.Log (Time.time + "    " + nextActionTime);
		if (Time.time > nextActionTime) {
			nextActionTime = Time.time +  2f;
			updateAverage ();
		}


	}



	public void updateAPM()
	{//Debug.Log ("adding money" + resOne);


		if(actions.Count==10){
			actions.RemoveAt(0);
		}
		actions.Add (Time.time);



	}


	public void updateAverage()
	{

	
		float apm = 0;
		int Acounter = actions.Count;

		foreach (float f in actions) {
			
			if ((Time.time - f) < 3.5) {
				apm = Time.time - actions [0];
			
				break;
			} else {
				Acounter--;
			}
		}
	//	Debug.Log ("size " + actions.Count + "  apm  " + apm);

		if (Acounter > 0) {

			counter.text = "Actions Per Minute\n" + (int)((Acounter / apm) * 60) +"\nGame Average\n" + (int)(totalActions/ (Clock.main.getTotalSecond() / 60));
		} else {
			counter.text = "Actions Per Minute\n0" +"\nGame Average\n" + (int)(totalActions/ (Clock.main.getTotalSecond() / 60));
		}
	}







}
