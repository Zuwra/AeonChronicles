using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class RaceSelectionManger : MonoBehaviour {

	public Dropdown raceDropdown;

	public GameObject racePacket;
	private List<RaceInfo> raceList = new List<RaceInfo>();

	public Image graph;
	public Text summary;
	public Text Title;
	public Text subtitle;

	private int currentIndex=0;

	public Button firstUnitButton;
	private List<Button> buttonlist = new List<Button>();


	// Use this for initialization
	void Start () {


		foreach (RaceInfo info in racePacket.GetComponents<RaceInfo>()) {
			raceList.Add (info);
		}

		loadRace (0);

	}

	public void initialize()
	{
		loadRace (1);
	}

	// Update is called once per frame
	void Update () {

	}


	public void switchList()
	{
		loadRace(raceDropdown.value);


	}

	private void loadRace(int index)

	{Title.text = raceList [index].race.ToString ();
		subtitle.text = raceList [index].subtitle;
		foreach (Button but in buttonlist) {
			Destroy (but.gameObject);
		}
		buttonlist.Clear ();
		currentIndex = index;


		summary.text = "General\n  " +raceList [index].summary;

		graph.material = raceList [index].PowerGraph;

		bool first = true;
		float currentX = firstUnitButton.transform.position.x;
		firstUnitButton.GetComponentInChildren<Text> ().text = raceList [index].unitList [0].name;
		for (int i = 0; i < raceList [index].unitList.Count; i++) {

			if (first) {
				first = false;
				continue;
			}

			Button tempB = (Button)Instantiate (firstUnitButton, firstUnitButton.transform.position, Quaternion.identity);
			tempB.transform.SetParent(firstUnitButton.transform.parent);
			tempB.GetComponent<RectTransform> ().localScale = new Vector3 (1,1,1);
			tempB.GetComponentInChildren<Text> ().text = raceList [index].unitList [i].name;
			currentX += 80;
			tempB.transform.position = new Vector3(currentX, tempB.transform.position.y, tempB.transform.position.z); 
			buttonlist.Add (tempB);
		}
	}

	public void loadGeneral()
	{summary.text = "General\n  " +raceList [currentIndex].summary;
	}

	public void selectDaexa()
	{
		loadRace (0);
	}

	public void loadTechTree()
	{


	}



	public void loadTips()
	{summary.text = "Playing as: \n" + raceList [currentIndex].playingAs + "\n\nPlaying Against:\n" + raceList [currentIndex].playingAgainst;
	}

	public void selectUrden()
	{
		loadRace (1);
	}


}
