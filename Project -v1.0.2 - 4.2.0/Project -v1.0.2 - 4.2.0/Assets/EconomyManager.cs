using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour {

	private RaceManager racer;

	public Text Workers ;
	public Text ResourceOne;
	public Text BarResOneAvg;
	public Text ResourceTwo;


	private Dictionary<float, int> resOneMap = new Dictionary<float, int>();
	private Dictionary<float, int> resTwoMap = new Dictionary<float, int>();
	private int totalWorkers = 0;

	// Use this for initialization
	void Start () {
		racer = GameManager.main.playerList [0];
	

		updateWorker (0);

		if (racer.OneName.Length > 0) {
			
			ResourceOne.text = " ";
			BarResOneAvg.text = " ";
		}
		if (racer.TwoName.Length > 0) {
			ResourceTwo.text =  " ";
		}

		InvokeRepeating ("updateAverage", 1,4);
	}
	

	public void updateAverage()
	{

		if (racer.OneName.Length > 0) {
			List<float> deleteThese = new List<float> ();

			int totalResOne = 0;
			foreach (KeyValuePair<float, int> entry in resOneMap) {
				if (entry.Key + 10 > Time.time) {
					totalResOne += entry.Value;
				} else {
					deleteThese.Add (entry.Key);
				}
			}

			foreach (float f in deleteThese) {
				resOneMap.Remove (f);
			}
			BarResOneAvg.text = "(+"+(totalResOne * 6)+")";
			ResourceOne.text = racer.OneName + ": " + totalResOne * 6 + " per min";

		}
		if (racer.TwoName.Length > 0) {
			List<float> deleteThese2 = new List<float> ();

			int totalResTwo = 0;
			foreach (KeyValuePair<float, int> entry in resTwoMap) {
				if (entry.Key + 10 > Time.time) {
					totalResTwo += entry.Value;
				} else {
					deleteThese2.Add (entry.Key);
				}
			}

			foreach (float f in deleteThese2) {
				resTwoMap.Remove (f);
			}

			ResourceTwo.text = racer.TwoName + ": " + totalResTwo * 6+ " per min";

		}
		}




	public void updateWorker( int input)
	{totalWorkers += input;

		Workers.text = "Workers: " + totalWorkers;
	}


	public void updateMoney(int resOne, int resTwo)
	{//Debug.Log ("adding money" + resOne);
		
		if (resOneMap.ContainsKey (Time.time)) {
			resOneMap [Time.time] += resOne;
		} else {
			resOneMap.Add (Time.time, resOne);
		}


		if (resTwoMap.ContainsKey (Time.time)) {
			resTwoMap [Time.time] += resTwo;
		} else {
			resTwoMap.Add (Time.time, resTwo);
		}


		updateAverage ();
	}


}
